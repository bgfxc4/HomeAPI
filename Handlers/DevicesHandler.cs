using System.Net.Sockets;

namespace HomeAPI.Handlers;

public static class DevicesHandler {
	static Room[] rooms = {
		new Room("marc", new DeviceGroup[] {
				new DeviceGroup("mainlight",
					new Device[] {
						new Device("1", "192.168.178.83", DeviceType.yeelightBulb),
						new Device("2", "192.168.178.84", DeviceType.yeelightBulb)
					}	
				)
			}
		)
	};

	public static DeviceActionList[] actionList = {
		new DeviceActionList(DeviceType.yeelightBulb, new ActionFunctionPair[] {
			new ActionFunctionPair(ActionNames.turnOn, YeelightHandler.TurnOn),
			new ActionFunctionPair(ActionNames.turnOn, YeelightHandler.TurnOff),
			new ActionFunctionPair(ActionNames.toggle, YeelightHandler.Toggle)
		})
	};

	public static Room[] getRoomList() {
		return rooms;
	}

	public static string sendTCPMessage(string ip, int port, string message) {
		TcpClient client = new TcpClient(ip, port);
		Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
		NetworkStream stream = client.GetStream();
		stream.Write(data, 0, data.Length);
		string responseData = "";
		Int32 bytes = stream.Read(data, 0, data.Length);
		responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

		stream.Close();
		client.Close();
		return responseData;
	}
}

public class Room {
	public string name {get; set;}
	public DeviceGroup[] groups {get; set;}

	public Room(string _name, DeviceGroup[] _groups) {
		name = _name;
		groups = _groups;
	}
}

public class DeviceGroup {
	public string name {get; set;}
	public Device[] devices {get; set;}

	public DeviceGroup(string _name, Device[] _devices) {
		name = _name;
		devices = _devices;
	}
}

public class Device {
	public string name {get; set;}
	public string ip {get; set;}
	public DeviceType type {get; set;}

	public Device(string _name, string _ip, DeviceType _type) {
		name = _name;
		ip = _ip;
		type = _type;
	}

	public string? execute(ActionNames action, string[] args) {
		DeviceActionList? list = Array.Find(DevicesHandler.actionList, el => el.type == type);
		if (list == null || list.actions == null) return null;
		ActionFunctionPair? pair = Array.Find(list.actions, el => el.name == action);
		if (pair == null || pair.action == null) return null;
		return pair.action(this, args);
	}
}

public class DeviceActionList {
	public DeviceType type { get; set; }
	public ActionFunctionPair[]? actions { get; set; }

	public DeviceActionList(DeviceType _type, ActionFunctionPair[] _actions) {
		type = _type;
		actions = _actions;
	}
}

public class ActionFunctionPair {
	public ActionNames name { get; set; }
	public Func<Device, string[], string>? action { get; set; }
	
	public ActionFunctionPair(ActionNames _name, Func<Device, string[], string> _action) {
		name = _name;
		action = _action;
	}
}

public enum ActionNames {
	toggle,
	turnOn,
	turnOff,
}

public enum DeviceType {
	yeelightBulb,
}

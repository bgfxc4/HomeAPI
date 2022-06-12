namespace HomeAPI.Handlers;

public static class YeelightHandler {
	public static string TurnOn(Device device, string[] argsList) {
		return DevicesHandler.sendTCPMessage(device.ip, 55443, "{\"id\": 1, \"method\": \"set_power\", \"params\": [\"on\", \"sudden\"]}\r\n");
	}

	public static string TurnOff(Device device, string[] argsList) {
		return DevicesHandler.sendTCPMessage(device.ip, 55443, "{\"id\": 1, \"method\": \"set_power\", \"params\": [\"off\", \"sudden\"]}\r\n");
	}

	public static string Toggle(Device device, string[] argsList) {
		return DevicesHandler.sendTCPMessage(device.ip, 55443, "{\"id\": 1, \"method\": \"toggle\", \"params\": []}\r\n");
	}
}

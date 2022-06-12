using Microsoft.AspNetCore.Mvc;

using HomeAPI.InputModels;
using HomeAPI.Handlers;

namespace HomeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase {
	[HttpPost]
	[Route("turnOn")]
	public IActionResult TurnOn(InputRoomLogin input) {
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}
		if (input.login_hash == null) return BadRequest("You have to enter a login_hash");
		if (!AuthorizeHandler.isAuthorized(input.login_hash)) {
			return Unauthorized();
		}
		
		Room? room = Array.Find(DevicesHandler.getRoomList(), el => el.name == input.room);
		if (room == null)
			return NotFound("A room with the name you specified does not exist");

		foreach (DeviceGroup gr in room.groups) {
		    foreach (Device d in gr.devices) {
				d.execute(ActionNames.turnOn, new string[0]);
			}
		}

		return Ok();
	}
}

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using HomeAPI.Handlers;

namespace HomeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController : ControllerBase {

	[Route("list")]
	[HttpGet]
    public string List() {
		return JsonSerializer.Serialize(DevicesHandler.getRoomList());
	}
}

using Microsoft.AspNetCore.Mvc;

using HomeAPI.InputModels;
using HomeAPI.Handlers;

namespace HomeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizeController : ControllerBase {

	[HttpPost]
    public IActionResult Authorize(InputLogin input) {
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}
		
		if (input.login_hash == null)
			return Unauthorized("Login hash can not be empty.");

		if (AuthorizeHandler.isAuthorized(input.login_hash))
			return Ok();
		return Unauthorized();
	}
}

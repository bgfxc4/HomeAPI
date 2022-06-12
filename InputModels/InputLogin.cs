using System.ComponentModel.DataAnnotations;

namespace HomeAPI.InputModels;

public class InputLogin {
	[Required]
	public string? login_hash {get; set;}
}

public class InputRoomLogin : InputLogin {
	[Required]
	public string? room {get; set;}
}

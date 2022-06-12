using System.Security.Cryptography;

namespace HomeAPI.Handlers;

public static class AuthorizeHandler {
	private static string login_hash = "17127c0332352e42256849fa04a5f40cc3ca87ac95da107e792f184243240fa9a168aeb21279fda1c5cc66d6f0fbc69e032fed88c0d8d115e08bf00d0f6f65cd";
	
	public static bool isAuthorized(string _login_hash) {
		return HashString(_login_hash) == login_hash;
	}

	static string HashString(string input) {
		using (SHA512 hash = SHA512.Create()) {
			var hashedBytes = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
			var hashedInputStringBuilder = new System.Text.StringBuilder(128);
			foreach (var b in hashedBytes) // convert bytes back to string
				hashedInputStringBuilder.Append(b.ToString("x2"));
			return hashedInputStringBuilder.ToString();
		}
	}
}

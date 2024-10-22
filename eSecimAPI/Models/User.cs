namespace eSecimAPI.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }

		// Şifre saklama için Password yerine PasswordHash ve PasswordSalt kullanacağız.
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }

		public string Role { get; set; }  // "Admin", "Voter"
	}


}

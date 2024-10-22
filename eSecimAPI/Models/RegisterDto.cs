namespace eSecimAPI.Models
{
	public class RegisterDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		// Vatandaş bilgileri
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string TCKimlikNo { get; set; }
	}

}

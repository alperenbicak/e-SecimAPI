namespace eSecimAPI.Models
{
	public class Register
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		// Vatandaş bilgileri
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string TCKimlikNo { get; set; }
	}

}

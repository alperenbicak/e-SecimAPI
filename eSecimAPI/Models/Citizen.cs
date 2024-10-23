
	using Microsoft.EntityFrameworkCore;

	namespace eSecimAPI.Models
	{
		[Index(nameof(TCKimlikNo), IsUnique = true)]
		public class Citizen
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string TCKimlikNo { get; set; }
		}
	}


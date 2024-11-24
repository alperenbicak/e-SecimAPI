using System.Collections.Generic;

namespace eSecimAPI.Models
{
	public class User
	{
		public int Id { get; set; }  // Kullanıcı benzersiz kimliği
		public string UserName { get; set; }  // Kullanıcı adı

		public byte[] PasswordHash { get; set; }  // Şifrelenmiş şifre
		public byte[] PasswordSalt { get; set; }  // Şifreleme için kullanılan salt değeri

		public string Role { get; set; }  // Kullanıcı rolü: "Admin" veya "Voter"

		// Kullandığı seçimlerin ID'lerini saklayan bir liste
		public List<int> VotedElectionIds { get; set; } = new List<int>();
	}
}


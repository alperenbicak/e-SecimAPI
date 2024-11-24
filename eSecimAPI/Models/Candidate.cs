namespace eSecimAPI.Models
{
	public class Candidate
	{
		public int CandidateId { get; set; }  // Adayın benzersiz kimliği
		public string CandidateName { get; set; }  // Adayın adı
		public int VoteCount { get; set; } = 0;  // Oy sayacı, başlangıç değeri 0
		public int ElectionId { get; set; }
		public Election Election { get; set; }
	}
}


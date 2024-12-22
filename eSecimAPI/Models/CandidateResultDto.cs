namespace eSecimAPI.Models
{
	public class CandidateResultDto
	{
		public int CandidateId { get; set; }
		public string CandidateName { get; set; }
		public int CalculatedVoteCount { get; set; } // Şifrelenmiş verilere göre hesaplanan oy sayısı
		public int RecordedVoteCount { get; set; } // Veritabanındaki mevcut oy sayısı
	}

}

namespace eSecimAPI.Models
{
	public class ElectionResultDto
	{
		public int ElectionId { get; set; }
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<CandidateResultDto> CandidateResults { get; set; }
	}

}

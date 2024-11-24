using eSecimAPI.Models;

public class ElectionDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public List<CandidateDto> Candidates { get; set; }
}

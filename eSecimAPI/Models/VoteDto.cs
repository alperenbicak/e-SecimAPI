using System.ComponentModel.DataAnnotations;

namespace eSecimAPI.Models
{
	public class VoteDto
	{
		[Required(ErrorMessage = "Seçim ID gereklidir.")]
		public int ElectionId { get; set; }

		[Required(ErrorMessage = "Aday ID gereklidir.")]
		public int CandidateId { get; set; }
	}
}

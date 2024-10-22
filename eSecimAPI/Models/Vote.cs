namespace eSecimAPI.Models
{
	public class Vote
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }

		public int ElectionId { get; set; }
		public Election Election { get; set; }

		public string Candidate { get; set; }
	}

}

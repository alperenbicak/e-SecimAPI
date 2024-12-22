namespace eSecimAPI.Models
{
	public class Vote
	{
        public int VoteId { get; set; }
		public string EncryptedVote { get; set; }
		public DateTime Timestamp { get; set; }
	}
}

using eSecimAPI.Models;

namespace eSecimAPI.Services
{
	public interface IVoteService
	{
		Task<bool> CastVoteAsync(int userId, VoteDto voteDto);
	}
}

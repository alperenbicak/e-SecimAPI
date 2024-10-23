namespace eSecimAPI.Services
{
	using eSecimAPI.Models;
	using System.Threading.Tasks;

	public interface IVoteService
	{
		Task<string> Vote(VoteDto request, int userId);
	}


}

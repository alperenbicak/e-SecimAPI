namespace eSecimAPI.Services
{
	using eSecimAPI.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IElectionService
	{
		Task<List<Election>> GetElections();
		Task<Election> CreateElection(ElectionDto request);
	}
}

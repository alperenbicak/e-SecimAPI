using eSecimAPI.Models;

namespace eSecimAPI.Services
{
	public interface IElectionService
	{
		Task<Election> CreateElectionAsync(ElectionCreateDto request);
		Task<List<ElectionDto>> GetAllElectionsAsync();
		Task<ElectionDto> GetElectionByIdAsync(int id);
		Task<Dictionary<int, int>> CalculateVoteResultsAsync(int electionId);
		Task<ElectionResultDto> GetElectionResultsAsync(int electionId);

	}
}


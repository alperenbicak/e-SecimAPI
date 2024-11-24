using eSecimAPI.Models;

namespace eSecimAPI.Services
{
	public interface IElectionService
	{
		Task<Election> CreateElectionAsync(ElectionCreateDto request);
		Task<List<ElectionDto>> GetAllElectionsAsync();
		Task<ElectionDto> GetElectionByIdAsync(int id);
	}
}


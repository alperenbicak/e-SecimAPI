namespace eSecimAPI.Services
{
	using eSecimAPI.Data;
	using eSecimAPI.Models;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class ElectionService : IElectionService
	{
		private readonly ESecimDbContext _context;

		public ElectionService(ESecimDbContext context)
		{
			_context = context;
		}

		// Seçimleri listeleme
		public async Task<List<Election>> GetElections()
		{
			return await _context.Elections.ToListAsync();
		}

		// Yeni bir seçim oluşturma
		public async Task<Election> CreateElection(ElectionDto request)
		{
			var election = new Election
			{
				Title = request.Title,
				StartDate = request.StartDate,
				EndDate = request.EndDate
			};

			_context.Elections.Add(election);
			await _context.SaveChangesAsync();

			return election;
		}
	}

}

namespace eSecimAPI.Services
{
	using eSecimAPI.Data;
	using eSecimAPI.Models;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Threading.Tasks;

	public class VoteService : IVoteService
	{
		private readonly ESecimDbContext _context;

		public VoteService(ESecimDbContext context)
		{
			_context = context;
		}

		// Oy verme işlemi
		public async Task<string> Vote(VoteDto request, int userId)
		{
			// Kullanıcının bu seçimde daha önce oy verip vermediğini kontrol et
			var existingVote = await _context.Votes.FirstOrDefaultAsync(v => v.UserId == userId && v.ElectionId == request.ElectionId);
			if (existingVote != null)
			{
				return "Bu seçim için zaten oy kullandınız.";
			}

			// Seçimin mevcut olup olmadığını kontrol et
			var election = await _context.Elections.FirstOrDefaultAsync(e => e.Id == request.ElectionId);
			if (election == null)
			{
				return "Seçim bulunamadı.";
			}

			// Seçim süresi içerisinde olup olmadığını kontrol et
			if (DateTime.Now < election.StartDate || DateTime.Now > election.EndDate)
			{
				return "Seçim şu anda aktif değil.";
			}

			// Oy verme işlemi
			var vote = new Vote
			{
				UserId = userId,
				ElectionId = request.ElectionId,
				Candidate = request.Candidate
			};

			_context.Votes.Add(vote);
			await _context.SaveChangesAsync();

			return "Oy başarıyla kullanıldı.";
		}
	}

}

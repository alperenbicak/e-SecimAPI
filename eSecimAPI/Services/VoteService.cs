using eSecimAPI.Data;
using eSecimAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace eSecimAPI.Services
{
	public class VoteService : IVoteService
	{
		private readonly ESecimDbContext _context;

		public VoteService(ESecimDbContext context)
		{
			_context = context;
		}

		public async Task<bool> CastVoteAsync(int userId, VoteDto voteDto)
		{
			// Kullanıcı ve seçim kontrolü
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null)
				throw new KeyNotFoundException("Kullanıcı bulunamadı.");

			var election = await _context.Elections.Include(e => e.Candidates)
												   .FirstOrDefaultAsync(e => e.Id == voteDto.ElectionId);
			if (election == null)
				throw new KeyNotFoundException("Seçim bulunamadı.");

			// Tarih kontrolü
			if (DateTime.UtcNow < election.StartDate || DateTime.UtcNow > election.EndDate)
				throw new InvalidOperationException("Bu seçim şu anda geçerli değil.");

			// Kullanıcının bu seçimde oy kullanıp kullanmadığını kontrol et
			if (user.VotedElectionIds.Contains(voteDto.ElectionId))
				throw new InvalidOperationException("Bu seçimde zaten oy kullandınız.");

			// Aday kontrolü
			var candidate = election.Candidates.FirstOrDefault(c => c.CandidateId == voteDto.CandidateId);
			if (candidate == null)
				throw new KeyNotFoundException("Aday bulunamadı.");

			// Oy kullanma işlemi
			candidate.VoteCount += 1;
			user.VotedElectionIds.Add(voteDto.ElectionId);

			// Değişiklikleri kaydet
			await _context.SaveChangesAsync();
			return true;
		}
	}
}

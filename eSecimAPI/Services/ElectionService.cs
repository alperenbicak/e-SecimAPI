using eSecimAPI.Data;
using eSecimAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace eSecimAPI.Services
{
	public class ElectionService : IElectionService
	{
		private readonly ESecimDbContext _context;

		public ElectionService(ESecimDbContext context)
		{
			_context = context;
		}

		public async Task<Election> CreateElectionAsync(ElectionCreateDto request)
		{
			// Validasyon: Tarih kontrolü
			if (request.StartDate >= request.EndDate)
				throw new ArgumentException("Başlangıç tarihi, bitiş tarihinden önce olmalıdır.");

			// Validasyon: Benzersiz seçim adı
			if (await _context.Elections.AnyAsync(e => e.Title == request.Title))
				throw new ArgumentException("Bu isimle zaten bir seçim mevcut.");

			// Validasyon: Aday listesi boş olamaz
			if (request.CandidateNames == null || !request.CandidateNames.Any())
				throw new ArgumentException("Seçim için en az bir aday girilmelidir.");

			// Seçim ve adayların oluşturulması
			var election = new Election
			{
				Title = request.Title,
				StartDate = request.StartDate,
				EndDate = request.EndDate,
				Candidates = request.CandidateNames.Select(name => new Candidate
				{
					CandidateName = name,
					VoteCount = 0
				}).ToList()
			};

			// Veritabanına ekleme
			_context.Elections.Add(election);
			await _context.SaveChangesAsync();

			return election;
		}


		public async Task<List<ElectionDto>> GetAllElectionsAsync()
		{
			// Seçimleri ve adayları veritabanından yükle
			var elections = await _context.Elections.Include(e => e.Candidates).ToListAsync();

			// Model -> DTO dönüşümü
			return elections.Select(e => new ElectionDto
			{
				Id = e.Id,
				Title = e.Title,
				StartDate = e.StartDate,
				EndDate = e.EndDate,
				Candidates = e.Candidates.Select(c => new CandidateDto
				{
					CandidateId = c.CandidateId,
					CandidateName = c.CandidateName,
					VoteCount = c.VoteCount
				}).ToList()
			}).ToList();
		}

		public async Task<ElectionDto> GetElectionByIdAsync(int id)
		{
			// Seçim ve adayları veritabanından yükle
			var election = await _context.Elections.Include(e => e.Candidates)
												   .FirstOrDefaultAsync(e => e.Id == id);

			if (election == null)
				throw new KeyNotFoundException("Seçim bulunamadı.");

			// Model -> DTO dönüşümü
			return new ElectionDto
			{
				Id = election.Id,
				Title = election.Title,
				StartDate = election.StartDate,
				EndDate = election.EndDate,
				Candidates = election.Candidates.Select(c => new CandidateDto
				{
					CandidateId = c.CandidateId,
					CandidateName = c.CandidateName,
					VoteCount = c.VoteCount
				}).ToList()
			};
		}
		public async Task<Dictionary<int, int>> CalculateVoteResultsAsync(int electionId)
		{
			// Seçime ait oyları al
			var votes = await _context.Votes.ToListAsync();
			var voteCount = new Dictionary<int, int>();
			foreach (var vote in votes)
			{
				// Şifrelenmiş veriyi çöz
				string decryptedVote = AesEncryptionHelper.Decrypt(vote.EncryptedVote);
				var parts = decryptedVote.Split(':');
				int candidateId = int.Parse(parts[1]);

				if (voteCount.ContainsKey(candidateId))
					voteCount[candidateId]++;
				else
					voteCount[candidateId] = 1;
			}

			return voteCount; // Aday ID ve Oy Sayısı eşleşmesi
		}

		// Seçim sonuçlarını döndürür
		public async Task<ElectionResultDto> GetElectionResultsAsync(int electionId)
		{
			// Seçimi getir
			var election = await _context.Elections.Include(e => e.Candidates)
												   .FirstOrDefaultAsync(e => e.Id == electionId);

			if (election == null)
				throw new KeyNotFoundException("Seçim bulunamadı.");

			// Oyları hesapla
			var calculatedVotes = await CalculateVoteResultsAsync(electionId);

			// Sonuçları DTO'ya dönüştür
			return new ElectionResultDto
			{
				ElectionId = election.Id,
				Title = election.Title,
				StartDate = election.StartDate,
				EndDate = election.EndDate,
				CandidateResults = election.Candidates.Select(c => new CandidateResultDto
				{
					CandidateId = c.CandidateId,
					CandidateName = c.CandidateName,
					CalculatedVoteCount = calculatedVotes.ContainsKey(c.CandidateId) ? calculatedVotes[c.CandidateId] : 0,
					RecordedVoteCount = c.VoteCount
				}).ToList()
			};
		}
	}

}



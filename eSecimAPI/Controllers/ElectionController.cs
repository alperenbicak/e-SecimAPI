

using eSecimAPI.Models;
using eSecimAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSecimAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ElectionController : ControllerBase
	{
		private readonly IElectionService _electionService;

		public ElectionController(IElectionService electionService)
		{
			_electionService = electionService;
		}

		// Seçimleri listeleme
		[HttpGet]
		public async Task<IActionResult> GetElections()
		{
			var elections = await _electionService.GetElections();
			return Ok(elections);
		}

		// Yeni bir seçim ekleme
		[HttpPost]
		[Authorize(Roles = "Admin")]  // Sadece admin seçim ekleyebilir
		public async Task<IActionResult> CreateElection(ElectionDto request)
		{
			var election = await _electionService.CreateElection(request);
			return Ok("Seçim başarıyla oluşturuldu.");
		}
	}


}

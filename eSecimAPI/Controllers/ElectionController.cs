using eSecimAPI.Models;
using eSecimAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSecimAPI.Controllers
{
	 // Sadece admin erişimi
	[Route("api/[controller]")]
	[ApiController]
	public class ElectionController : ControllerBase
	{
		private readonly IElectionService _electionService;

		public ElectionController(IElectionService electionService)
		{
			_electionService = electionService;
		}

		// Seçim oluşturma
		[Authorize(Roles = "Admin")]
		[HttpPost("create")]
		public async Task<IActionResult> CreateElection([FromBody] ElectionCreateDto request)
		{
			try
			{
				var election = await _electionService.CreateElectionAsync(request);
				return Ok(new { Message = "Seçim başarıyla oluşturuldu.", ElectionId = election.Id });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { Error = ex.Message });
			}
		}

		// Tüm seçimleri listeleme
		[HttpGet("all")]
		public async Task<IActionResult> GetAllElections()
		{
			var elections = await _electionService.GetAllElectionsAsync();
			return Ok(elections);
		}

		// Tek bir seçimi alma
		[HttpGet("{id}")]
		public async Task<IActionResult> GetElectionById(int id)
		{
			try
			{
				var election = await _electionService.GetElectionByIdAsync(id);
				return Ok(election);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(new { Error = ex.Message });
			}
		}
	}
}

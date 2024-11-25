	using eSecimAPI.Models;
	using eSecimAPI.Services;
	using global::eSecimAPI.Models;
	using global::eSecimAPI.Services;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	namespace eSecimAPI.Controllers
	{
		[Authorize] // Giriş yapılmış kullanıcılar erişebilir
		[Route("api/[controller]")]
		[ApiController]
		public class VoteController : ControllerBase
		{
			private readonly IVoteService _voteService;

			public VoteController(IVoteService voteService)
			{
				_voteService = voteService;
			}

			[HttpPost("cast")]
			public async Task<IActionResult> CastVote([FromBody] VoteDto voteDto)
			{
				try
				{
				// Kullanıcı ID'sini token'dan alıyoruz
				var userIdClaim = User.FindFirst("id")?.Value;
				if (string.IsNullOrEmpty(userIdClaim))
					return Unauthorized(new { Error = "Kullanıcı doğrulaması yapılamadı." });

				var userId = int.Parse(userIdClaim);


				await _voteService.CastVoteAsync(userId, voteDto);
					return Ok(new { Message = "Oy başarıyla kullanıldı." });
				}
				catch (KeyNotFoundException ex)
				{
					return NotFound(new { Error = ex.Message });
				}
				catch (InvalidOperationException ex)
				{
					return BadRequest(new { Error = ex.Message });
				}
			}
		}
	}


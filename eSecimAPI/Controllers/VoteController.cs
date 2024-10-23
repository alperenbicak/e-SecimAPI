using eSecimAPI.Models;
using eSecimAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eSecimAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VoteController : ControllerBase
	{
		private readonly IVoteService _voteService;

		public VoteController(IVoteService voteService)
		{
			_voteService = voteService;
		}

		// Oy verme işlemi
		[HttpPost]
		[Authorize]  // Giriş yapmış kullanıcılar oy verebilir
		public async Task<IActionResult> Vote(VoteDto request)
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));  // JWT'den kullanıcıyı alıyoruz
			var result = await _voteService.Vote(request, userId);
			if (result != "Oy başarıyla kullanıldı.")
			{
				return BadRequest(result);
			}

			return Ok(result);
		}
	}

}

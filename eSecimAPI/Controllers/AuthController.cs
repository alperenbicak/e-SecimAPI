
namespace eSecimAPI.Controllers
{
	using eSecimAPI.Data;
	using eSecimAPI.Models;
	using eSecimAPI.Services;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;

	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ESecimDbContext _context;
		private readonly AuthenticationService _authService;

		public AuthController(ESecimDbContext context, AuthenticationService authService)
		{
			_context = context;
			_authService = authService;
		}

		// Kullanıcı kayıt API
		[HttpPost("register")]
		public async Task<IActionResult> Register(UserDto request)
		{
			_authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

			var user = new User
			{
				UserName = request.UserName,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				Role = "Voter"  // Varsayılan olarak "Voter" rolünde kayıt yapıyoruz
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok("Kayıt başarılı!");
		}

		// Kullanıcı giriş API
		[HttpPost("login")]
		public async Task<IActionResult> Login(UserDto request)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
			if (user == null)
				return BadRequest("Kullanıcı bulunamadı.");

			if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
				return BadRequest("Hatalı şifre.");

			var token = _authService.CreateToken(user);

			return Ok(new { Token = token });
		}
	}

}

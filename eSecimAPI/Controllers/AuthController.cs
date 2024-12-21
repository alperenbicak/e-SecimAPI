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
		private readonly AuthService _authService;

		public AuthController(ESecimDbContext context, AuthService authService)
		{
			_context = context;
			_authService = authService;
		}

		// Kullanıcı kayıt API
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] Register request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Vatandaş doğrulama
			var citizen = await _context.Citizens.FirstOrDefaultAsync(c =>
				c.TCKimlikNo == request.TCKimlikNo &&
				c.FirstName == request.FirstName &&
				c.LastName == request.LastName);

			if (citizen == null)
			{
				return BadRequest(new { Error = "Vatandaş bilgileri doğrulanamadı. Lütfen geçerli bir TC Kimlik numarası ve isim giriniz." });
			}

			// Kullanıcı adı kontrolü
			if (await _context.Users.AnyAsync(x => x.UserName == request.UserName))
			{
				return BadRequest(new { Error = "Kullanıcı adı zaten mevcut." });
			}

			// Şifre hash'leme işlemi
			_authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

			var user = new User
			{
				UserName = request.UserName,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				Role = "Voter",  // Varsayılan olarak voter
				VotedElectionIds = new List<int>()
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			var message = "Kayıt Başarılı";

			return Ok(new { Message = message });
		}

		// Giriş API (Admin ve Voter için aynı endpoint)
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserDto request)
		{
			// Kullanıcıyı kontrol ediyoruz
			var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
			if (user == null)
				return Unauthorized(new { Error = "Kullanıcı bulunamadı." });

			// Şifre doğrulama
			if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
				return Unauthorized(new { Error = "Hatalı şifre." });

			// JWT token oluşturma
			var token = _authService.CreateToken(user);

			// Giriş türüne göre mesaj
			var message = user.Role == "Admin" ? "Admin giriş başarılı!" : "Kullanıcı girişi başarılı!";

			return Ok(new { Token = token, Role = user.Role, Message = message });
		}
	}
}

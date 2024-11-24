using System.ComponentModel.DataAnnotations;

public class UserDto
{
	[Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
	public string UserName { get; set; }

	[Required(ErrorMessage = "Şifre gereklidir.")]
	[MinLength(8, ErrorMessage = "Şifre en az 8 karakter uzunluğunda olmalıdır.")]
	public string Password { get; set; }
}

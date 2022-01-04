using System.ComponentModel.DataAnnotations;

namespace Profi.Dtos
{
    public class LoginDto
    {
        [Required, MinLength(1)] public string? Login { get; set; }
        [Required, MinLength(1)] public string? Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Profi.Models
{
    public class LoginModel
    {
        [Required] public string? Login { get; set; }
        [Required] public string? Password { get; set; }
    }
}
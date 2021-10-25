using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profi.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace Profi.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Authentification(LoginDto model)
        {
            // Implémentation exemple de la fonction : le mot de passe est tout simplement le login inversé
            // Login = admin, mdp = nimda
            string mdp = new string(model.Login.ToCharArray().Reverse().ToArray());
            SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(mdp));
            if(string.Compare(mdp, Convert.ToBase64String(hash)) == 0)
            {
                return Ok();
            }
            return Unauthorized();
        }
    }
}

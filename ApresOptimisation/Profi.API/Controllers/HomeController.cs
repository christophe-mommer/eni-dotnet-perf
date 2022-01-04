using Microsoft.AspNetCore.Mvc;
using Profi.Dtos;

namespace Profi.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Authentification(LoginDto? model)
        {
            if (model is not null)
            {
                // Implémentation exemple de la fonction : le mot de passe est tout simplement le login inversé
                // Login = admin, mdp = nimda
                string mdp = new string(model.Login!.ToCharArray().Reverse().ToArray());
                bool equals = true;
                for (int i = 0; i < mdp.Length; i++)
                {
                    try
                    {
                        if (mdp[i] != model.Password![i])
                        {
                            equals = false;
                        }
                    }
                    catch
                    {
                        equals = false;
                    }
                }

                if (equals)
                {
                    return Ok();
                }
            }

            return Unauthorized();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profi.Business.Models;

namespace Profi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(Personne.RecupererListe());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(Personne.Recuperer(id));
        }
    }
}

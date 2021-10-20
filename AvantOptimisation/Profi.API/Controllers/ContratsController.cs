using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profi.Business.Models;

namespace Profi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratsController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ContratsController(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("{personneId}")]
        public async Task<IActionResult> Get(string personneId)
        {
            return Ok(Contrat.RecupererListe(personneId));
        }

        [HttpGet("byId/{contratId}")]
        public async Task<IActionResult> GetContrat(string contratId)
        {
            return Ok(Contrat.Recuperer(contratId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string content)
        {
            string fichier = configuration["ResumesDir"] + Path.GetRandomFileName() + ".txt";
            System.IO.File.WriteAllText(fichier, content);
            return Ok();
        }

        [HttpGet("complexe")]
        public async Task<IActionResult> Complexe()
        {
            return Ok(Contrat.RequeteComplexe());
        }
    }
}

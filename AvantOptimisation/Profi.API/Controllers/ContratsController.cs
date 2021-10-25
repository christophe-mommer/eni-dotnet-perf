using Microsoft.AspNetCore.Mvc;
using Profi.Infra;
using Profi.Infra.Messages.Contrats;

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
            var result = await Bus.Current.DispatchMessage(new RecupererListeParPersonne(personneId));
            return Ok(result);
        }

        [HttpGet("byId/{contratId}")]
        public async Task<IActionResult> GetContrat(string contratId)
        {
            var result = await Bus.Current.DispatchMessage(new RecupererContrat(contratId));
            return Ok(result);
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
            var result = await Bus.Current.DispatchMessage(new ExecuterRequeteComplexe());
            return Ok(result);
        }
    }
}

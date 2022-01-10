using Microsoft.AspNetCore.Mvc;
using Profi.Infra;
using Profi.Infra.Messages.Contrats;

namespace Profi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratsController : ControllerBase
    {
        private readonly Bus bus;
        private readonly IConfiguration configuration;

        public ContratsController(
            Bus bus,
            IConfiguration configuration)
        {
            this.bus = bus;
            this.configuration = configuration;
        }

        [HttpGet("{personneId}")]
        public async Task<IActionResult> Get(string personneId)
        {
            var result = await bus.DispatchMessage(new RecupererListeParPersonne(personneId));
            return Ok(result);
        }

        [HttpGet("byId/{contratId}")]
        public async Task<IActionResult> GetContrat(string contratId)
        {
            var result = await bus.DispatchMessage(new RecupererContrat(contratId));
            return Ok(result);
        }

        [HttpGet("download/{contratId}")]
        public async Task<IActionResult> GetFichierContrat(string contratId)
        {
            var result = await bus.DispatchMessage(new FusionnerContrat(contratId));
            if (result is string filePath && System.IO.File.Exists(filePath))
            {
                return File(await System.IO.File.ReadAllBytesAsync(filePath), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "contrat.docx");
            }
            return NotFound();
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
            var result = await bus.DispatchMessage(new ExecuterRequeteComplexe());
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Profi.Infra;
using Profi.Infra.Messages.Personnes;

namespace Profi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        private readonly Bus bus;

        public PersonnesController(
            Bus bus)
        {
            this.bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await bus.DispatchMessage(new RecupererPersonnes());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await bus.DispatchMessage(new RecupererPersonne(id));
            return Ok(result);
        }
    }
}

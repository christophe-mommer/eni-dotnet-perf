using Microsoft.AspNetCore.Mvc;
using Profi.Infra;
using Profi.Infra.Messages.Personnes;

namespace Profi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Bus.Current.DispatchMessage(new RecupererPersonnes());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await Bus.Current.DispatchMessage(new RecupererPersonne(id));
            return Ok(result);
        }
    }
}

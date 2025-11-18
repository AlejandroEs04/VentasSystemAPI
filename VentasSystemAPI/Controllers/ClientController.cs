using Microsoft.AspNetCore.Mvc;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ClientController(IClientService service) : ControllerBase
    {
        private readonly IClientService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _service.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _service.Get(id);
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Dtos.ClientCreateDto clientDto)
        {
            var createdClient = await _service.Add(clientDto);
            return CreatedAtAction(nameof(GetClient), new { id = createdClient.IdCliente }, createdClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Dtos.ClientUpdateDto clientDto)
        {
            var updatedClient = await _service.Update(id, clientDto);
            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}

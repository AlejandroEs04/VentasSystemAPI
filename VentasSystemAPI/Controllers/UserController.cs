using Microsoft.AspNetCore.Mvc;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> users = await _service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _service.Get(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto user)
        {
            User createdUser = await _service.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.IdRol }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto user)
        {
            User updatedUser = await _service.Update(user, id);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}

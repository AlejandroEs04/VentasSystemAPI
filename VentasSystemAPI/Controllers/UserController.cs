using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController(IUserService service, IEmailService emailService) : ControllerBase
    {
        private readonly IUserService _service = service;
        private readonly IEmailService _emailService = emailService;
        
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
            User userExists = await _service.GetByEmail(user.Correo);

            if (userExists != null)
                return BadRequest("El correo ya esta siendo usado por otro usuario");

            User createdUser = await _service.Add(user);

            // TODO: Send confirmation email


            return CreatedAtAction(nameof(GetUser), new { id = createdUser.IdRol }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto user)
        {
            User userExists = await _service.GetByEmail(user.Correo);

            if (userExists != null && userExists.IdUsuario != id)
                return BadRequest("El correo ya esta siendo usado por otro usuario");

            User updatedUser = await _service.Update(user, id);

            // TODO: Send notification email if email was changed
            await _emailService.SendEmailAsync(
                updatedUser.Correo,
                "Your profile has been updated",
                $"Hello {updatedUser.Nombre}, your profile information has been successfully updated."
            );

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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;
using VentasSystemAPI.Utils;

namespace VentasSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController(
        IUserService service, 
        IEmailService emailService, 
        ISecurityService securityService, 
        IConfiguration config
    ) : ControllerBase
    {
        private readonly IUserService _service = service;
        private readonly IEmailService _emailService = emailService;
        private readonly ISecurityService _securityService = securityService;
        private readonly IConfiguration _config = config;

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
            string password = user.Clave;
            User? userExists = await _service.GetByEmail(user.Correo);

            if (userExists != null)
                return BadRequest("El correo ya esta siendo usado por otro usuario");

            if (string.IsNullOrEmpty(user.Clave))
            {
                user.Clave = _securityService.GeneratePass();
                password = user.Clave;
            }

            user.Clave = _securityService.HashPassword(user.Clave);

            User createdUser = await _service.Add(user);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "AccountCreatedSuccessfullyTemplate.html");
            var htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);

            htmlContent = htmlContent.Replace("@ViewData[\"Clave\"]", password);
            
            await _emailService.SendEmailAsync(
                user.Correo,
                "Cuenta creada correctamente",
                htmlContent
            );

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.IdRol }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto user)
        {
            User? userExists = await _service.GetByEmail(user.Correo);

            if (userExists != null && userExists.IdUsuario != id)
                return BadRequest("El correo ya esta siendo usado por otro usuario");

            User updatedUser = await _service.Update(user, id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "CredentialsUpdated.html");
            var htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);

            htmlContent = htmlContent.Replace("@ViewData[\"Url\"]", _config.GetSection("AppSettings:FRONTEND_URL").Value + "/Acceso");

            await _emailService.SendEmailAsync(
                user.Correo,
                "Tu perfil se actualizó con éxito",
                htmlContent
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

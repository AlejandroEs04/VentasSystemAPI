using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;
using VentasSystemAPI.Utils;

namespace VentasSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController(
        IUserService service, 
        ISecurityService securityService, 
        IConfiguration config, 
        IEmailService emailService
    ) : ControllerBase
    {
        private readonly IUserService _service = service;
        private readonly ISecurityService _securityService = securityService;
        private readonly IConfiguration _config = config;
        private readonly IEmailService _emailService = emailService;

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                User user = await _service.GetByCredentials(dto.Correo, dto.Clave);

                if (user == null)
                {
                    return Unauthorized("Credenciales inválidas");
                }

                return Ok(_securityService.CreateToken(user.IdUsuario));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            string password = dto.Clave;
            User? user = await _service.GetByEmail(dto.Correo);

            if (user == null)
            {
                return NotFound("Correo no registrado");
            }

            user.Clave = dto.Clave;
            await _service.Update(new UserUpdateDto
            {
                Nombre = user.Nombre,
                Correo = user.Correo,
                Telefono = user.Telefono,
                IdRol = user.IdRol,
                UrlFoto = user.UrlFoto,
                NombreFoto = user.NombreFoto,
                Clave = dto.Clave,
                EsActivo = user.EsActivo
            }, user.IdUsuario);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "PasswordChanged.html");
            var htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);

            htmlContent = htmlContent.Replace("@ViewData[\"Correo\"]", user.Correo);
            htmlContent = htmlContent.Replace("@ViewData[\"Clave\"]", password);
            htmlContent = htmlContent.Replace("@ViewData[\"Url\"]", _config.GetSection("AppSettings:FRONTEND_URL").Value + "/Acceso");

            await _emailService.SendEmailAsync(
                user.Correo,
                "Contraseña cambio correctamente",
                htmlContent
            );

            return Ok("Clave restablecida con éxito");
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            string userId = User.FindFirst("userId")?.Value + "";

            User user = await _service.Get(int.Parse(userId));

            return Ok(new Dictionary<string, string> {
                {"token", _securityService.CreateToken(user.IdUsuario)}
            });
        }

        [HttpGet("GetAuth")]
        public async Task<IActionResult> GetAuth()
        {
            string userId = User.FindFirst("userId")?.Value + "";
            User user = await _service.Get(int.Parse(userId));
            return Ok(user);
        }
    }
}

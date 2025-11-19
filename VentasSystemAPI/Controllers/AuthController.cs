using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;
using VentasSystemAPI.Utils;

namespace VentasSystemAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController(IUserService service, ISecurityService securityService) : ControllerBase
    {
        private readonly IUserService _service = service;
        private readonly ISecurityService _securityService = securityService;

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

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            User user = await _service.GetByEmail(dto.Correo);
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
    }
}

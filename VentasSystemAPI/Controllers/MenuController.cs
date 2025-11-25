using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MenuController(ApiDbContext context, IUserService userService) : ControllerBase
    {
        private readonly ApiDbContext _context = context;
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            var allMenus = await _context.Menus.ToListAsync();
            string userId = User.FindFirst("userId")?.Value ?? "";
            User user = await _userService.Get(int.Parse(userId));

            var accessibleMenuIds = await _context.RoleMenus
                .Where(m => m.IdRol == user.IdRol && m.EsActivo)
                .Select(m => m.IdMenu)
                .ToListAsync();

            // Crear conjunto de todos los menús accesibles (incluyendo padres)
            var accessibleMenus = new HashSet<int>(accessibleMenuIds);

            // Agregar menús padres de los submenús accesibles
            foreach (var menuId in accessibleMenuIds.ToList())
            {
                var menu = allMenus.First(m => m.IdMenu == menuId);
                if (menu.IdMenuPadre != menu.IdMenu && menu.IdMenuPadre > 0)
                {
                    accessibleMenus.Add(menu.IdMenuPadre);
                }
            }

            // Construir respuesta
            var menusResponse = allMenus
                .Where(m => accessibleMenus.Contains(m.IdMenu) &&
                           (m.IdMenu == m.IdMenuPadre || m.IdMenuPadre == m.IdMenu))
                .Select(parentMenu => new MenuResponseDto
                {
                    IdMenu = parentMenu.IdMenu,
                    Descripcion = parentMenu.Descripcion,
                    Icono = parentMenu.Icono ?? "",
                    Controlador = parentMenu.Controlador ?? "",
                    PaginaAccion = parentMenu.PaginaAccion ?? "",
                    SubMenus = allMenus.Where(subMenu =>
                        subMenu.IdMenuPadre == parentMenu.IdMenu &&
                        subMenu.IdMenu != parentMenu.IdMenu &&
                        accessibleMenus.Contains(subMenu.IdMenu))
                })
                .ToList();

            return Ok(menusResponse);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;

namespace VentasSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class RolController(ApiDbContext context) : ControllerBase
    {
        private readonly ApiDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await  _context.Roles.ToListAsync());
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;

namespace VentasSystemAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("Api/[controller]")]
    public class SaleDocumentController(ApiDbContext context) : ControllerBase
    {
        private readonly ApiDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            return Ok(await _context.DocumentTypes.ToListAsync());
        }
    }
}

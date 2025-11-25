using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;

namespace VentasSystemAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("Api/[controller]")]
    public class SatController(ApiDbContext context) : ControllerBase
    {
        private readonly ApiDbContext _context = context;

        [HttpGet("ClaveProdServ")]
        public async Task<IActionResult> GetCProdServ()
        {
            return Ok(await _context.ClaveProdServ_SAT.ToListAsync());
        }

        [HttpGet("ClaveUnidad")]
        public async Task<IActionResult> GetCUnidad()
        {
            return Ok(await _context.ClaveUnidad_SAT.ToListAsync());
        }

        [HttpGet("ClaveImpuesto")]
        public async Task<IActionResult> GetCImpuesto()
        {
            return Ok(await _context.Impuesto_SAT.ToListAsync());
        }

        [HttpGet("ObjetoImpuesto")]
        public async Task<IActionResult> GetCObjetoImp()
        {
            return Ok(await _context.ObjetoImp_SAT.ToListAsync());
        }

        [HttpGet("RegimenFiscal")]
        public async Task<IActionResult> GetRegimenFiscal()
        {
            return Ok(await _context.RegimenFiscal.ToListAsync());
        }
    }
}

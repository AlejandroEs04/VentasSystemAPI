using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class BusinessController(IBusinessService businessService) : ControllerBase
    {
        private readonly IBusinessService _businessService = businessService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBusiness()
        {
            var businessData = await _businessService.GetBusiness();
            return Ok(businessData);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBusiness(Business business)
        {
            var businessUpdated = await _businessService.Update(business);
            return Ok(businessUpdated);
        }
    }
}

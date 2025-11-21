using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductController(IProductService service) : ControllerBase
    {
        private readonly IProductService _service = service;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            IEnumerable<Product> products = await _service.GetAll();
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                Product product = await _service.Get(id);
                return Ok(product);
            }
            catch (ArgumentException)
            {
                return NotFound("Producto no fue encontrado");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto model)
        {
            Product createdProduct = await _service.Add(model);
            return Ok(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductDto model, int id)
        {
            try
            {
                Product updatedProduct = await _service.Update(model, id);
                return Ok(updatedProduct);
            }
            catch (ArgumentException)
            {
                return NotFound("Producto no fue encontrado");
            }
            catch (Exception)
            {
                return BadRequest("Hubo un error el servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch(ArgumentException)
            {
                return NotFound("Producto no fue encontrado");
            }
            catch(Exception)
            {
                return BadRequest("Hubo un error el servidor");
            }
        }
    }
}

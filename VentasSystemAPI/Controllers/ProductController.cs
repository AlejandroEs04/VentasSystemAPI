using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductController(IProductService service) : ControllerBase
    {
        private readonly IProductService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            IEnumerable<Product> products = await _service.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            Product product = await _service.Get(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductFormDto form)
        {
            var dto = JsonSerializer.Deserialize<ProductDto>(form.dto)
                ?? throw new Exception("Error al recibir producto");

            string urlImage = "";
            string fileName = "";

            if (form.File != null && form.File.Length > 0)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string extension = Path.GetExtension(form.File.FileName);
                fileName = $"{Guid.NewGuid()}{extension}";
                string fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await form.File.CopyToAsync(stream);
                }

                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                urlImage = $"{baseUrl}/images/products/{fileName}";
            }

            Product createdProduct = await _service.Add(dto, urlImage, fileName);
            return Ok(createdProduct);
        }
    }
}

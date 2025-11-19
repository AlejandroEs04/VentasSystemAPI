using Microsoft.AspNetCore.Mvc;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Services;

namespace VentasSystemAPI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CategoryController(ICategoryService service) : ControllerBase
    {
        private readonly ICategoryService _service = service;

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                Category category = await _service.Get(id);
                return Ok(category);
            }
            catch (ArgumentException)
            {
                return NotFound("Categoria no existe");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            Category createdCategory = await _service.Add(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.IdCategoria }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto category)
        {
            try
            {
                Category updatedCategory = await _service.Update(category, id);
                return Ok(updatedCategory);
            }
            catch (ArgumentException)
            {
                return NotFound("Categoria no existe");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound("Categoria no existe");
            }
        }
    }
}

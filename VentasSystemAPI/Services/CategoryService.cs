using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;

namespace VentasSystemAPI.Services
{
    public class CategoryService(ICategoryRepository repository) : ICategoryService
    {
        private readonly ICategoryRepository _repository = repository;

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Category> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<Category> Add(CategoryDto dto)
        {
            var entity = new Category
            {
                Descripcion = dto.Descripcion,
                EsActivo = dto.EsActivo,
                FechaRegistro = DateTime.Now
            };

            return await _repository.Add(entity);
        }

        public async Task<Category> Update(CategoryDto dto, int id)
        {
            var entity = new Category
            {
                IdCategoria = id, 
                Descripcion = dto.Descripcion, 
                EsActivo = dto.EsActivo
            };
            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}

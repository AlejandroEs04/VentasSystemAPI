using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> Get(int id);
        Task<Category> Add(CategoryDto category);
        Task<Category> Update(CategoryDto category, int id);
        Task<bool> Delete(int id);
    }
}

using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(int id);
        Task<Product> Add(ProductDto product, string urlImage = "", string nameImage = "");
        Task<Product> Update(ProductDto product, int id, string urlImage = "", string nameImage = "");
        Task<bool> Delete(int id);
    }
}

using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetByBarCode(string barCode);
    }
}

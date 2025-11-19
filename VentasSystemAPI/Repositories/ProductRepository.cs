using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public class ProductRepository(ApiDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<Product?> GetByBarCode(string barCode)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.CodigoBarra == barCode);
        }
    }
}

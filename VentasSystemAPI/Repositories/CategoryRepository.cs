using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public class CategoryRepository(ApiDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
    }
}

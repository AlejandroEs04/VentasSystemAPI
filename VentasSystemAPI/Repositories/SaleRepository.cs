using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public class SaleRepository(ApiDbContext context) : GenericRepository<Sale>(context), ISaleRepository
    {
    }
}

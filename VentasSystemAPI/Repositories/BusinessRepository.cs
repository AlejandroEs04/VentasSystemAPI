using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public class BusinessRepository(ApiDbContext context) : GenericRepository<Business>(context), IBusinessRepository
    {
    }
}

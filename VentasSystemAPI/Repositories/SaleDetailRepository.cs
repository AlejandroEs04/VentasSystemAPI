using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public class SaleDetailRepository(ApiDbContext context) : GenericRepository<SaleDetails>(context), ISaleDetailRepository
    {
        private readonly ApiDbContext _context = context;
        public IEnumerable<SaleDetails> GetBySale(int saleId)
        {
            return _context.SaleDetails.Where(sd => sd.IdVenta == saleId);
        }
    }
}

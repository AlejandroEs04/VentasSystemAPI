using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public interface ISaleDetailRepository : IGenericRepository<SaleDetails>
    {
        IEnumerable<SaleDetails> GetBySale(int saleId);
    }
}

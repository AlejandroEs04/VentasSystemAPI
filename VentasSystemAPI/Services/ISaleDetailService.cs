using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Services
{
    public interface ISaleDetailService
    {
        IEnumerable<SaleDetails> GetBySale(int saleId);
        Task<SaleDetails> Get(int id);
        Task<SaleDetails> Add(SaleDetails saleDetail);
        Task<bool> Delete(int id);
    }
}

using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAll();
        Task<Sale> Get(int id);
        Task<Sale> Add(Sale sale);
        Task<bool> Delete(int id);
    }
}

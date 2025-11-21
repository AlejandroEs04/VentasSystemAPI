using VentasSystemAPI.Models;

namespace VentasSystemAPI.Services
{
    public interface IBusinessService
    {
        Task<Business> GetBusiness();
        Task<Business> Update(Business business);
    }
}

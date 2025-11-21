using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;

namespace VentasSystemAPI.Services
{
    public class BusinessService(IBusinessRepository businessRepository) : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository = businessRepository;

        public async Task<Business> GetBusiness()
        {
            var businesses = await _businessRepository.GetAll();
            return businesses.FirstOrDefault() ?? new Business();
        }
        public async Task<Business> Update(Business business)
        {
            var updatedBusiness = await _businessRepository.Update(business);
            return updatedBusiness;
        }
    }
}

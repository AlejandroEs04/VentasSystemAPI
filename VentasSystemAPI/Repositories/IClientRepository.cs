using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> Get(int id);
        Task<Client> Add(Client client);
        Task<Client> Update(Client client);
        Task<bool> Delete(int id);
    }
}

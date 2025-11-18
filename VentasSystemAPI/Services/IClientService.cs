using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> Get(int id);
        Task<Client> Add(ClientCreateDto client);
        Task<Client> Update(int id, ClientUpdateDto client);
        Task<bool> Delete(int id);
    }
}

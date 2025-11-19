using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
        Task<User> Add(UserCreateDto user);
        Task<User> Update(UserUpdateDto user, int id);
        Task<bool> Delete(int id);
        Task<User> GetByCredentials(string correo, string clave);
        Task<User> GetByEmail(string correo);
    }
}

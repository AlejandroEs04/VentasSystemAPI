using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByCredentials(string correo, string clave);
        Task<User> GetByEmail(string correo);
    }
}

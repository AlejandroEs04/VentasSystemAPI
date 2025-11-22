using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public class UserRepository(ApiDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User> GetByCredentials(string correo, string clave)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave) ?? throw new UnauthorizedAccessException("Credenciales inválidas");
        }

        public async Task<User?> GetByEmail(string correo)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}

using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;
using VentasSystemAPI.Utils;

namespace VentasSystemAPI.Services
{
    public class UserService(IUserRepository repository, ISecurityService securityService) : IUserService
    {
        private readonly IUserRepository _repository = repository;
        private readonly ISecurityService _securityService = securityService;

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<User> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<User> Add(UserCreateDto dto)
        {
            var entity = new User
            {
                Nombre = dto.Nombre, 
                Correo = dto.Correo, 
                Telefono = dto.Telefono, 
                IdRol = dto.IdRol, 
                UrlFoto = dto.UrlFoto, 
                NombreFoto = dto.NombreFoto,
                Clave = dto.Clave,
                EsActivo = true, 
                FechaRegistro = DateTime.Now
            };
            return await _repository.Add(entity);
        }

        public async Task<User> Update(UserUpdateDto dto, int id)
        {
            var currentUser = await _repository.Get(id);

            if (string.IsNullOrEmpty(dto.Clave))
                dto.Clave = currentUser.Clave;
            else
                dto.Clave = _securityService.HashPassword(dto.Clave);

            currentUser.Nombre = dto.Nombre;
            currentUser.Correo = dto.Correo;
            currentUser.Telefono = dto.Telefono;
            currentUser.IdRol = dto.IdRol;
            currentUser.UrlFoto = dto.UrlFoto;
            currentUser.NombreFoto = dto.NombreFoto;
            currentUser.Clave = dto.Clave;
            currentUser.EsActivo = dto.EsActivo;

            return await _repository.Update(currentUser);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<User> GetByCredentials(string correo, string clave)
        {
            return await _repository.GetByCredentials(correo, _securityService.HashPassword(clave));
        }

        public async Task<User?> GetByEmail(string correo)
        {
            return await _repository.GetByEmail(correo);
        }
    }
}

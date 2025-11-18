using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;

namespace VentasSystemAPI.Services
{
    public class ClientService(IClientRepository repository) : IClientService
    {
        private readonly IClientRepository _repository = repository;

        public async Task<Client> Add(ClientCreateDto dto)
        {
            var entity = new Client
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Rfc = dto.Rfc,
                FechaRegistro = DateTime.Now,
                DomicilioFiscalReceptor = dto.DomicilioFiscalReceptor, 
                RegimenFiscalReceptor = dto.RegimenFiscalReceptor
            };

            return await _repository.Add(entity);
        }

        public async Task<Client> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Client> Update(int id, ClientUpdateDto dto)
        {
            var entity = new Client
            {
                IdCliente = id,
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Rfc = dto.Rfc,
                EsActivo = dto.EsActivo,
                DomicilioFiscalReceptor = dto.DomicilioFiscalReceptor,
                RegimenFiscalReceptor = dto.RegimenFiscalReceptor,
            };
            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}

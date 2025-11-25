using Microsoft.VisualBasic;
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
                RegimenFiscalReceptor = dto.RegimenFiscalReceptor,
                EsActivo = true
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
            var clientExists = await Get(id);

            clientExists.Nombre = dto.Nombre;
            clientExists.Correo = dto.Correo;
            clientExists.Rfc = dto.Rfc;
            clientExists.EsActivo = dto.EsActivo;
            clientExists.DomicilioFiscalReceptor = dto.DomicilioFiscalReceptor;
            clientExists.RegimenFiscalReceptor = dto.RegimenFiscalReceptor;

            return await _repository.Update(clientExists);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Repositories
{
    public class ClientRepository(ApiDbContext context) : IClientRepository
    {
        private readonly ApiDbContext _context = context;

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> Get(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.IdCliente == id) ?? throw new Exception("Cliente no existe");
        }

        public async Task<Client> Add(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Update(Client client)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.IdCliente == client.IdCliente) ?? throw new Exception("Cliente no existe");
            
            existingClient.Nombre = client.Nombre;
            existingClient.Correo = client.Correo;
            existingClient.Rfc = client.Rfc;
            existingClient.DomicilioFiscalReceptor = client.DomicilioFiscalReceptor;
            existingClient.RegimenFiscalReceptor = client.RegimenFiscalReceptor;
            existingClient.EsActivo = client.EsActivo;

            _context.Clients.Update(existingClient);
            await _context.SaveChangesAsync();
            return existingClient;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdCliente == id) ?? throw new Exception("Cliente no existe");
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

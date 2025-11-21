using VentasSystemAPI.Data;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;

namespace VentasSystemAPI.Services
{
    public class SaleService(ISaleRepository repository) : ISaleService
    {
        private readonly ISaleRepository _repository = repository;

        public async Task<IEnumerable<Sale>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Sale> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<Sale> Add(Sale dto)
        {
            Sale sale = new()
            {
                NumeroVenta = dto.NumeroVenta,
                IdTipoDocumentoVenta = dto.IdTipoDocumentoVenta,
                IdUsuario = dto.IdUsuario,
                SubTotal = dto.SubTotal,
                ImpuestoTotal = dto.ImpuestoTotal,
                Total = dto.Total,
                FechaRegistro = DateTime.Now,
                IdCliente = dto.IdCliente,
                Descuento = dto.Descuento
            };
            return await _repository.Add(sale);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}

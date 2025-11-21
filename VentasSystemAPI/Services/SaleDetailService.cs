using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;

namespace VentasSystemAPI.Services
{
    public class SaleDetailService(ISaleDetailRepository repository) : ISaleDetailService
    {
        private readonly ISaleDetailRepository _repository = repository;

        public IEnumerable<SaleDetails> GetBySale(int saleId)
        {
            return _repository.GetBySale(saleId);
        }   

        public async Task<SaleDetails> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<SaleDetails> Add(SaleDetails dto)
        {
            SaleDetails detail = new()
            {
                IdVenta = dto.IdVenta,
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad,
                MarcaProducto = dto.MarcaProducto,
                DescripcionProducto = dto.DescripcionProducto,
                CategoriaProducto = dto.CategoriaProducto,
                Precio = dto.Precio,
                Total = dto.Total
            };
            return await _repository.Add(detail);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}

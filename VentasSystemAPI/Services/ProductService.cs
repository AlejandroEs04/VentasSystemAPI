using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;

namespace VentasSystemAPI.Services
{
    public class ProductService(IProductRepository repository) : IProductService
    {
        private readonly IProductRepository _repository = repository;

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Product> Get(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<Product> Add(ProductDto dto)
        {

            var productExists = await _repository.GetByBarCode(dto.CodigoBarra);

            if (productExists != null)
                throw new InvalidOperationException("Ya existe un producto registrado con el mismo código de barras");

            var entity = new Product
            {
                CodigoBarra = dto.CodigoBarra, 
                Marca = dto.Marca,
                Descripcion = dto.Descripcion,
                IdCategoria = dto.IdCategoria,
                Stock = dto.Stock,
                Precio = dto.Precio,
                EsActivo = dto.EsActivo,
                FechaRegistro = DateTime.Now,
                UnidadMedida = dto.UnidadMedida,
                UnidadMedidaSat = dto.UnidadMedidaSat,
                ClaveProductoSat = dto.ClaveProductoSat,
                ObjetoImpuesto = dto.ObjetoImpuesto,
                FactorImpuesto = dto.FactorImpuesto,
                Impuesto = dto.Impuesto,
                ValorImpuesto = dto.ValorImpuesto,
                TipoImpuesto = dto.TipoImpuesto,
                Descuento = dto.Descuento,
                NombreImagen = dto.NombreImagen, 
                UrlImagen = dto.UrlImagen
            };
            return await _repository.Add(entity);
        }

        public async Task<Product> Update(ProductDto dto, int id)
        {
            var entity = new Product
            {
                IdProducto = id,
                CodigoBarra = dto.CodigoBarra,
                Marca = dto.Marca,
                Descripcion = dto.Descripcion,
                IdCategoria = dto.IdCategoria,
                Stock = dto.Stock,
                Precio = dto.Precio,
                EsActivo = dto.EsActivo,
                FechaRegistro = DateTime.Now,
                UnidadMedida = dto.UnidadMedida,
                UnidadMedidaSat = dto.UnidadMedida,
                ClaveProductoSat = dto.ClaveProductoSat,
                ObjetoImpuesto = dto.ObjetoImpuesto,
                FactorImpuesto = dto.FactorImpuesto,
                Impuesto = dto.Impuesto,
                ValorImpuesto = dto.ValorImpuesto,
                TipoImpuesto = dto.TipoImpuesto,
                Descuento = dto.Descuento
            };

            if (!string.IsNullOrEmpty(dto.UrlImagen) && !string.IsNullOrEmpty(dto.NombreImagen))
            {
                entity.UrlImagen = dto.UrlImagen;
                entity.NombreImagen = dto.NombreImagen;
            }

            return await _repository.Update(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasSystemAPI.Data;
using VentasSystemAPI.Documents;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;
using VentasSystemAPI.Services;
using VentasSystemAPI.Utils;
using QuestPDF.Fluent;

namespace VentasSystemAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("Api/[controller]")]
    public class SaleController(
        ISaleService saleRepository, 
        ISaleDetailService saleDetailRepository, 
        IProductService product, 
        ICategoryService categoryRepository,
        IBusinessService businessRepository,
        ApiDbContext context
    ) : ControllerBase
    {
        private readonly ISaleService _saleRepository = saleRepository;
        private readonly ISaleDetailService _saleDetailRepository = saleDetailRepository;
        private readonly IProductService _productRepository = product;
        private readonly ICategoryService _categoryRepository = categoryRepository;
        private readonly IBusinessService _businessRepository = businessRepository;
        private readonly ApiDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            IEnumerable<Sale> sales = await _saleRepository.GetAll();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(int id)
        {
            Sale sale = await _saleRepository.Get(id);
            IEnumerable<SaleDetails> saleDetails = _saleDetailRepository.GetBySale(id);

            SaleResponseDto saleResponse = new()
            {
                Productos = [.. saleDetails],
                IdVenta = sale.IdVenta,
                NumeroVenta = sale.NumeroVenta,
                IdTipoDocumentoVenta = sale.IdTipoDocumentoVenta,
                IdUsuario = sale.IdUsuario,
                SubTotal = sale.SubTotal,
                ImpuestoTotal = sale.ImpuestoTotal,
                Total = sale.Total,
                FechaRegistro = sale.FechaRegistro,
                IdCliente = sale.IdCliente,
                Descuento = sale.Descuento
            };

            return Ok(saleResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(SaleCreateDto saleDto)
        {
            List<SaleDetails> saleDetails = [];
            decimal discount = 0;
            decimal tax = 0;
            decimal subtotal = 0;

            string userId = User.FindFirst("userId")?.Value + "";

            foreach (var detail in saleDto.Detalles)
            {
                Product currentProduct = await _productRepository.Get(detail.IdProducto);
                discount += currentProduct.Descuento * detail.Cantidad;
                subtotal += currentProduct.Precio * detail.Cantidad;
                tax += (currentProduct.Precio * decimal.Parse(currentProduct.Impuesto) / 100) * detail.Cantidad;
            }

            SerialNumber serialNumber = await _context.SerialNumbers.FirstAsync();

            Sale newSale = new()
            {
                NumeroVenta = (serialNumber.UltimoNumero + 1).ToString(),
                IdTipoDocumentoVenta = saleDto.IdTipoDocumentoVenta,
                IdUsuario = int.Parse(userId),
                SubTotal = subtotal,
                ImpuestoTotal = tax,
                Total = subtotal + tax - discount,
                FechaRegistro = DateTime.Now,
                IdCliente = saleDto.IdCliente,
                Descuento = discount
            };

            Sale createdSale = await _saleRepository.Add(newSale);

            foreach (var detail in saleDto.Detalles)
            {
                Product currentProduct = await _productRepository.Get(detail.IdProducto);
                Category category = await _categoryRepository.Get(currentProduct.IdCategoria);

                SaleDetails newDetail = new()
                {
                    IdVenta = createdSale.IdVenta,
                    IdProducto = detail.IdProducto,
                    MarcaProducto = currentProduct.Marca,
                    DescripcionProducto = currentProduct.Descripcion,
                    CategoriaProducto = category.Descripcion,
                    Cantidad = detail.Cantidad,
                    Precio = currentProduct.Precio,
                    Total = currentProduct.Precio * detail.Cantidad
                };

                var saleDetailCreated = await _saleDetailRepository.Add(newDetail);
                saleDetails.Add(saleDetailCreated);
            }

            SaleResponseDto response = new()
            {
                Productos = saleDetails,
                IdVenta = createdSale.IdVenta,
                NumeroVenta = createdSale.NumeroVenta,
                IdTipoDocumentoVenta = createdSale.IdTipoDocumentoVenta,
                IdUsuario = createdSale.IdUsuario,
                SubTotal = createdSale.SubTotal,
                ImpuestoTotal = createdSale.ImpuestoTotal,
                Total = createdSale.Total,
                FechaRegistro = createdSale.FechaRegistro,
                IdCliente = createdSale.IdCliente,
                Descuento = createdSale.Descuento
            };

            return Ok(response);
        }

        [HttpGet("Report/Download/{id}")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            Sale sale = await _saleRepository.Get(id);
            IEnumerable<SaleDetails> saleDetails = _saleDetailRepository.GetBySale(id);
            Business business = await _businessRepository.GetBusiness();

            SaleReportDto saleReport = new()
            {
                Sale = sale,
                SaleDetails = saleDetails,
                Business = business
            };

            var document = new SalePdf(saleReport);
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"venta_{id}.pdf");
        }
    }
}

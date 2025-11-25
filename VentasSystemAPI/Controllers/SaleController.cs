using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using VentasSystemAPI.Data;
using VentasSystemAPI.Documents;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;
using VentasSystemAPI.Repositories;
using VentasSystemAPI.Services;
using VentasSystemAPI.Utils;
using static VentasSystemAPI.Services.TimbradoService.TimbradoService;

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
        IClientService clientService,
        TimbradoClient _timbradoClient,
        ApiDbContext context,
        TimpradoApiClient timbradoApiClient
    ) : ControllerBase
    {
        private readonly ISaleService _saleRepository = saleRepository;
        private readonly ISaleDetailService _saleDetailRepository = saleDetailRepository;
        private readonly IProductService _productRepository = product;
        private readonly ICategoryService _categoryRepository = categoryRepository;
        private readonly IBusinessService _businessRepository = businessRepository;
        private readonly IClientService _clientService = clientService;
        private readonly ApiDbContext _context = context;
        private readonly TimbradoClient _timbradoClient = _timbradoClient;
        private readonly TimpradoApiClient _timbradoApiClient = timbradoApiClient;

        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            IEnumerable<Sale> sales = await _saleRepository.GetAll();
            return Ok(sales);
        }

        [HttpGet("Report")]
        public async Task<IActionResult> GetReport(int? month, DateTime? startTime, DateTime? endTime)
        {
            IEnumerable<Sale> sales = await _saleRepository.GetAll();

            decimal totalAmount = 0;
            decimal totalQuantity = 0;

            if(month.HasValue) sales = sales.Where(s => s.FechaRegistro.Month == month);

            if(startTime.HasValue) sales = sales.Where(s => s.FechaRegistro > startTime);

            if(endTime.HasValue) sales = sales.Where(s => s.FechaRegistro < endTime);

            foreach (Sale sale in sales)
            {
                IEnumerable<SaleDetails> saleDetails = _saleDetailRepository.GetBySale(sale.IdVenta);

                foreach (SaleDetails detail in saleDetails)
                {
                    totalQuantity += detail.Cantidad;
                }

                totalAmount += sale.Total;
            }

            int salesQuantity = sales.Count();

            var productSales = await _context.ProductsSellers.ToListAsync();

            var response = new
            {
                TotalAmount = totalAmount,
                TotalQuantity = totalQuantity,
                SalesQuantity = salesQuantity,
                productSales,
                sales
            };

            return Ok(response);
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
                tax += ((currentProduct.Precio - currentProduct.Descuento) * (currentProduct.ValorImpuesto / 100)) * detail.Cantidad;
            }

            SerialNumber serialNumber = await _context.SerialNumbers.FirstAsync();

            Sale newSale = new()
            {
                NumeroVenta = (serialNumber.UltimoNumero + 1).ToString("D6"),
                IdTipoDocumentoVenta = saleDto.IdTipoDocumentoVenta,
                IdUsuario = int.Parse(userId),
                SubTotal = subtotal,
                ImpuestoTotal = tax,
                Total = subtotal + tax - discount,
                FechaRegistro = DateTime.Now,
                IdCliente = saleDto.IdCliente,
                Descuento = discount
            };

            serialNumber.UltimoNumero += 1;

            _context.SerialNumbers.Update(serialNumber);
            await _context.SaveChangesAsync();

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

        [HttpGet("GenerateXml/{id}")]
        public async Task<IActionResult> GenerateXml(int id)
        {
            Sale sale = await _saleRepository.Get(id);
            IEnumerable<SaleDetails> saleDetails = _saleDetailRepository.GetBySale(id);
            Client client = await _clientService.Get(sale.IdCliente);

            string xml = new FactureHelper(_context).GenerarXmlVenta(sale, client, [.. saleDetails]);

            string zipBase64 = await _timbradoApiClient.TimbrarAsync(xml);

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "facturas");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = $"Factura_{id}.zip";
            string fullPath = Path.Combine(folderPath, fileName);

            var bytes = Convert.FromBase64String(zipBase64);
            System.IO.File.WriteAllBytes(fullPath, bytes);

            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            string urlDescarga = $"{baseUrl}/facturas/{fileName}";

            return Ok(new
            {
                fileName,
                url = urlDescarga
            });
        }

    }
}

using VentasSystemAPI.Models;

namespace VentasSystemAPI.Dtos
{
    public class SaleResponseDto
    {
        public int IdVenta { get; set; }
        public string NumeroVenta { get; set; } = "";
        public int? IdTipoDocumentoVenta { get; set; }
        public int IdUsuario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ImpuestoTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdCliente { get; set; }
        public decimal Descuento { get; set; }
        public IEnumerable<SaleDetails> Productos { get; set; } = [];
    }
}

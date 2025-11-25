using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Dtos
{
    [Table("vw_Venta")]
    public class SaleReportResponseDto
    {
        [Key]
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
        public string? DocumentoVenta { get; set; } = "";
        public int CantidadProductos { get; set; }
        public int Cantidad { get; set; }
        public string ClienteNombre { get; set; } = "";
        public string ClienteCorreo { get; set; } = "";
        public string ClienteRfc { get; set; } = "";
        public string UsuarioNombre { get; set; } = "";
    }
}

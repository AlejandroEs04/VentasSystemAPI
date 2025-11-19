using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("Venta")]
    public class Sale
    {
        [Key]
        public int IdVenta { get; set; }
        public string NumeroVenta { get; set; } = "";
        public int IdTipoDocumentoVenta { get; set; }
        public int IdUsuario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ImpuestoTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdCliente { get; set; }
        public decimal Descuento { get; set; }
    }
}

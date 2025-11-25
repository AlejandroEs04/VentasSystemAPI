using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("vw_ProductosMasVendidos")]
    public class VW_ProductsSellers
    {
        [Key] 
        public int IdProducto { get; set; }

        public string MarcaProducto { get; set; } = "";
        public string DescripcionProducto { get; set; } = "";
        public string CategoriaProducto { get; set; } = "";
        public int TotalCantidadVendida { get; set; }
        public decimal TotalGenerado { get; set; }
    }
}

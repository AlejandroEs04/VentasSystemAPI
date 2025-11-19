using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("TipoDocumentoVenta")]
    public class SaleDocumentType
    {
        [Key]
        public int IdTipoDocumentoVenta { get; set; }
        public string Descripcion { get; set; } = "";
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

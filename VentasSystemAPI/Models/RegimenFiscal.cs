using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("c_RegimenFiscal")]
    public class RegimenFiscal
    {
        [Key]
        public int IdRegimenFiscal { get; set; }
        public string C_RegimenFiscal { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}

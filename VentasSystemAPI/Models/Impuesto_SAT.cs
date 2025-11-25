using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("c_Impuesto_SAT")]
    public class Impuesto_SAT
    {
        [Key]
        public int IdImpuesto { get; set; }
        public string C_Impuesto_SAT { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}

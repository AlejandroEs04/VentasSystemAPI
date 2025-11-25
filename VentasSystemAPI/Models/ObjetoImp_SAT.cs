using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("c_ObjetoImp_SAT")]
    public class ObjetoImp_SAT
    {
        [Key]
        public int IdObjetoImp_SAT { get; set; }
        public string C_ObjetoImp_SAT { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}

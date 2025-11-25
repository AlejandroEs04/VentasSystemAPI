using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("c_ClaveUnidad_SAT")]
    public class ClaveUnidad_SAT
    {
        [Key]
        public int IdClaveUnidad_SAT { get; set; }
        public string C_ClaveUnidad_SAT { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}

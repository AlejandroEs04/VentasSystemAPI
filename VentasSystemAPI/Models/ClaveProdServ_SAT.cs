using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("c_ClaveProdServ_SAT")]
    public class ClaveProdServ_SAT
    {
        [Key]
        public int IdClaveProdServ_SAT { get; set; }
        public string C_ClaveProdServ_SAT { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}

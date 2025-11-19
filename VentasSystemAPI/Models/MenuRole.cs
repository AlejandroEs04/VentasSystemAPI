using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("RolMenu")]
    public class MenuRole
    {
        [Key]
        public int IdRolMenu { get; set; }
        public int IdRol { get; set; }
        public int IdMenu { get; set; }
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

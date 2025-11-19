using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("Rol")]
    public class Role
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; } = "";
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

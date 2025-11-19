using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("Usuario")]
    public class User
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Telefono { get; set; } = "";
        public int IdRol { get; set; }
        public string UrlFoto { get; set; } = "";
        public string NombreFoto { get; set; } = "";
        public string Clave { get; set; } = "";
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

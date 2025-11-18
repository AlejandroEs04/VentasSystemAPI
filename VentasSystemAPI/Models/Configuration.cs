using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("Configuracion")]
    public class Configuration
    {
        public string Recurso { get; set; } = "";
        public string Propiedad { get; set; } = "";
        public string Valor { get; set; } = "";
    }
}

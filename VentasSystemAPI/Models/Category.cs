using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("Categoria")]
    public class Category
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; } = "";
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

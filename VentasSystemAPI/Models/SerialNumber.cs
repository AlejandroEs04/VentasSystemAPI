using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("NumeroCorrelativo")]
    public class SerialNumber
    {
        [Key]
        public int IdNumeroCorrelativo { get; set; }
        public int UltimoNumero { get; set; }
        public int CantidadDigitos { get; set; }
        public string Gestion { get; set; } = "";
        public DateTime FechaActualizacion { get; set; }
    }
}

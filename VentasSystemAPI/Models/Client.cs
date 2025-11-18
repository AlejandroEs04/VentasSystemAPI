using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasSystemAPI.Models
{
    [Table("Cliente")]
    public class Client
    {
        [Key]
        public int? IdCliente { get; set; }
        public string Nombre { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Rfc { get; set; } = "";
        public string DomicilioFiscalReceptor { get; set; } = "";
        public string RegimenFiscalReceptor { get; set; } = "";
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

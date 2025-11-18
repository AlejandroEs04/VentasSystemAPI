using System.ComponentModel.DataAnnotations;

namespace VentasSystemAPI.Dtos
{
    public class ClientUpdateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = "";
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es correcto")]
        public string Correo { get; set; } = "";
        public string Rfc { get; set; } = "";
        public string DomicilioFiscalReceptor { get; set; } = "";
        public string RegimenFiscalReceptor { get; set; } = "";
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

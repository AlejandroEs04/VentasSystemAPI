using System.ComponentModel.DataAnnotations;

namespace VentasSystemAPI.Dtos
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = "";
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es correcto")]
        public string Correo { get; set; } = "";
        [Required(ErrorMessage = "El telefono es obligatorio.")]
        public string Telefono { get; set; } = "";
        [Required(ErrorMessage = "El rol es obligatorio.")]
        public int IdRol { get; set; }
        public string UrlFoto { get; set; } = "";
        public string NombreFoto { get; set; } = "";
        public string Clave { get; set; } = "";
        public bool EsActivo { get; set; }
    }
}

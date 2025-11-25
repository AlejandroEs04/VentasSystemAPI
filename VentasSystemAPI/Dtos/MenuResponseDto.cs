using VentasSystemAPI.Models;

namespace VentasSystemAPI.Dtos
{
    public class MenuResponseDto
    {
        public int IdMenu { get; set; }
        public string Descripcion { get; set; } = "";
        public string Icono { get; set; } = "";
        public string Controlador { get; set; } = "";
        public string PaginaAccion { get; set; } = "";
        public IEnumerable<Menu> SubMenus { get; set; } = [];
    }
}

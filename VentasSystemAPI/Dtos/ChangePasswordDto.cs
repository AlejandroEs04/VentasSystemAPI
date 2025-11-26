namespace VentasSystemAPI.Dtos
{
    public class ChangePasswordDto
    {
        public string ClaveActual { get; set; } = "";
        public string NuevaClave { get; set; } = "";
        public string NuevaClaveRepetida { get; set; } = "";
    }
}

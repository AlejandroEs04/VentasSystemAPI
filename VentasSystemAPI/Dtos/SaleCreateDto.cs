namespace VentasSystemAPI.Dtos
{
    public class SaleCreateDto
    {
        public int IdCliente { get; set; }
        public int? IdTipoDocumentoVenta { get; set; }
        public IEnumerable<SaleDetailDto> Detalles { get; set; } = [];
    }
}

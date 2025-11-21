using VentasSystemAPI.Models;

namespace VentasSystemAPI.Dtos
{
    public class SaleReportDto
    {
        public Sale Sale { get; set; } = new Sale();
        public IEnumerable<SaleDetails> SaleDetails { get; set; } = [];
        public Business Business { get; set; } = new Business();
    }
}

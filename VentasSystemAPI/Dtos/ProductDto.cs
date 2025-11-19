namespace VentasSystemAPI.Dtos
{
    public class ProductDto
    {
        public string CodigoBarra { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public int IdCategoria { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public bool EsActivo { get; set; }
        public string UnidadMedida { get; set; } = "";
        public string UnidadMedidaSat { get; set; } = "";
        public string ClaveProductoSat { get; set; } = "";
        public string ObjetoImpuesto { get; set; } = "";
        public string FactorImpuesto { get; set; } = "";
        public string Impuesto { get; set; } = "";
        public decimal ValorImpuesto { get; set; }
        public string TipoImpuesto { get; set; } = "";
        public decimal Descuento { get; set; }
        public string UrlImagen { get; set; } = "";
        public string NombreImagen { get; set; } = "";
    }
}

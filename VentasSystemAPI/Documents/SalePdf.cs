using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using VentasSystemAPI.Dtos;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Documents
{
    public class SalePdf : IDocument
    {
        private readonly SaleReportDto _model;

        public SalePdf(SaleReportDto model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => new DocumentMetadata
        {
            Title = $"Venta {_model.Sale.NumeroVenta}"
        };

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(20);

                // ENCABEZADO
                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text(_model.Business.Nombre.ToUpper()).Bold();
                        col.Item().Text($"Dirección: {_model.Business.Direccion}");
                        col.Item().Text($"Correo: {_model.Business.Correo}");
                    });

                    row.RelativeItem().AlignRight().Column(col =>
                    {
                        col.Item().Text("NÚMERO VENTA").Bold().FontSize(14);
                        col.Item().Text(_model.Sale.NumeroVenta.ToString());
                    });
                });

                page.Content().Column(col =>
                {
                    col.Spacing(10);

                    // TABLA DE PRODUCTOS
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(column =>
                        {
                            column.RelativeColumn();
                            column.ConstantColumn(80);
                            column.ConstantColumn(80);
                            column.ConstantColumn(80);
                        });

                        // HEADER
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Producto");
                            header.Cell().Element(CellStyle).Text("Cantidad");
                            header.Cell().Element(CellStyle).Text("Precio");
                            header.Cell().Element(CellStyle).Text("Total");
                        });

                        // FILAS DINÁMICAS
                        foreach (var item in _model.SaleDetails)
                        {
                            table.Cell().Text(item.DescripcionProducto);
                            table.Cell().Text(item.Cantidad.ToString());
                            table.Cell().Text($"{_model.Business.SimboloMoneda} {item.Precio}");
                            table.Cell().Text($"{_model.Business.SimboloMoneda} {item.Total}");
                        }
                    });

                    // TOTALES
                    col.Item().AlignRight().Column(totals =>
                    {
                        totals.Item().Text($"Sub Total: {_model.Business.SimboloMoneda} {_model.Sale.SubTotal}").Bold();
                        totals.Item().Text($"IGV: {_model.Business.SimboloMoneda} {_model.Sale.ImpuestoTotal}").Bold();
                        totals.Item().Text($"Total: {_model.Business.SimboloMoneda} {_model.Sale.Total}").Bold().FontSize(14);
                    });
                });
            });
        }

        private static IContainer CellStyle(IContainer container) =>
            container.Background("#03A99F").Padding(5).DefaultTextStyle(x => x.FontColor("#FFFFFF"));
    }
}

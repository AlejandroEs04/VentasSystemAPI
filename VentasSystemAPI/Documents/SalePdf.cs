using QuestPDF.Fluent;
using QuestPDF.Helpers;
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
                page.Size(PageSizes.A4);

                // ====== HEADER ======
                page.Header().PaddingBottom(10).Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        // Logo
                        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "uploaded", _model.Business.NombreLogo);
                        if (File.Exists(logoPath))
                        {
                            row.RelativeItem().Width(120).Height(120)
                                .Image(logoPath, ImageScaling.FitArea);
                        }

                        // Número Venta
                        row.RelativeItem().AlignRight().Column(rightCol =>
                        {
                            rightCol.Item().Text("NÚMERO VENTA")
                                .Bold().FontColor("#03A99F").FontSize(18);

                            rightCol.Item().Text(_model.Sale.NumeroVenta.ToString())
                                .Bold().FontSize(14);
                        });
                    });
                });

                page.Content().Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        // Negocio
                        row.RelativeItem().Column(business =>
                        {
                            business.Item().Text(_model.Business.Nombre.ToUpper()).Bold().FontSize(14);
                            business.Item().Text($"Dirección: {_model.Business.Direccion}")
                                    .FontColor("#858585").FontSize(12);
                            business.Item().Text($"Correo: {_model.Business.Correo}")
                                    .FontColor("#858585").FontSize(12);
                        });

                        // Cliente
                        row.RelativeItem().AlignRight().Column(client =>
                        {
                            client.Item().Text("CLIENTE").Bold().FontSize(13);
                            client.Item().Text(_model.Client.Nombre).FontColor("#858585");
                            client.Item().Text(_model.Client.Rfc).FontColor("#858585");
                        });
                    });

                    col.Spacing(200);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(column =>
                        {
                            column.RelativeColumn();
                            column.ConstantColumn(100);
                            column.ConstantColumn(100);
                            column.ConstantColumn(100);
                        });

                        // Header
                        table.Header(header =>
                        {
                            header.Cell().Element(ThStyle).Text("Producto");
                            header.Cell().Element(ThStyle).Text("Cantidad");
                            header.Cell().Element(ThStyle).Text("Precio");
                            header.Cell().Element(ThStyle).Text("Total");
                        });

                        // Filas
                        foreach (var item in _model.SaleDetails)
                        {
                            table.Cell().Element(TdStyleNormal).Text(item.DescripcionProducto);
                            table.Cell().Element(TdStyleNormal).Text(item.Cantidad.ToString());
                            table.Cell().Element(TdStyleNormal).Text($"{_model.Business.SimboloMoneda} {item.Precio}");
                            table.Cell().Element(TdStyleTotal).Text($"{_model.Business.SimboloMoneda} {item.Total}");
                        }
                    });

                    col.Spacing(10);

                    // ====== TOTALES ======
                    col.Item().AlignRight().Column(totals =>
                    {
                        totals.Item().Text($"Sub Total: {_model.Business.SimboloMoneda} {_model.Sale.SubTotal}")
                                .Bold().FontSize(13);
                        totals.Item().Text($"IGV: {_model.Business.SimboloMoneda} {_model.Sale.ImpuestoTotal}")
                                .Bold().FontSize(13);
                        totals.Item().Text($"Total: {_model.Business.SimboloMoneda} {_model.Sale.Total}")
                                .Bold().FontSize(16).FontColor("#03A99F");
                    });
                });
            });
        }

        // ====== ESTILOS ======
        private static IContainer ThStyle(IContainer container) =>
            container.Background("#03A99F").Padding(10)
                    .DefaultTextStyle(x => x.FontColor("#FFFFFF").FontSize(14).Bold());

        private static IContainer TdStyleNormal(IContainer container) =>
            container.BorderBottom(1).BorderColor("#E8E8E8")
                    .Padding(8).DefaultTextStyle(x => x.FontColor("#757575"));

        private static IContainer TdStyleTotal(IContainer container) =>
            container.Background("#EDF6F9").Padding(8)
                    .DefaultTextStyle(x => x.FontColor("#757575"));
    }
}

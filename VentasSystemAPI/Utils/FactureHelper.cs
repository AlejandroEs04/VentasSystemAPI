using Microsoft.EntityFrameworkCore;
using System.Security;
using System.Text;
using VentasSystemAPI.Data;
using VentasSystemAPI.Models;

namespace VentasSystemAPI.Utils
{
    public class FactureHelper(ApiDbContext context)
    {
        private readonly ApiDbContext _context = context;

        public string GenerarXmlVenta(Sale venta, Client cliente, List<SaleDetails> detalles)
        {
            string idLocal = "11" + venta.NumeroVenta;

            decimal totalImpuestosTrasladados = 0;

            var sb = new StringBuilder();
            sb.Append("<Comprobante>");
            sb.Append($"<idLocal>{idLocal}</idLocal>");
            sb.Append("<version>4.0</version>");
            sb.Append("<serie></serie>");
            sb.Append($"<folio>{venta.NumeroVenta}</folio>");
            sb.Append("<formaPago>01</formaPago>");
            sb.Append("<condicionesDePago></condicionesDePago>");
            sb.Append($"<subTotal>{venta.SubTotal}</subTotal>");
            sb.Append($"<descuento>{venta.Descuento}</descuento>");
            sb.Append("<moneda>MXN</moneda>");
            sb.Append("<tipoCambio>1.0</tipoCambio>");
            sb.Append($"<total>{venta.Total}</total>");
            sb.Append("<tipoDeComprobante>I</tipoDeComprobante>");
            sb.Append("<exportacion>01</exportacion>");
            sb.Append("<metodoPago>PUE</metodoPago>");
            sb.Append("<lugarExpedicion>66400</lugarExpedicion>");
            sb.Append("<confirmacion></confirmacion>");
            sb.Append("<periodicidad></periodicidad>");
            sb.Append("<meses></meses>");
            sb.Append("<ano></ano>");

            sb.Append("<Relacionado>");
            sb.Append("<tipoRelacion></tipoRelacion>");
            sb.Append("<uUID></uUID>");
            sb.Append("</Relacionado>");

            // Datos del receptor (cliente)
            sb.Append($"<regimenFiscal>601</regimenFiscal>");
            sb.Append($"<facAtrAdquirente></facAtrAdquirente>");
            sb.Append($"<rfc>{EscapeXml(cliente.Rfc)}</rfc>");
            sb.Append($"<nombre>{EscapeXml(cliente.Nombre)}</nombre>");
            sb.Append($"<domicilioFiscalReceptor>{EscapeXml(cliente.DomicilioFiscalReceptor)}</domicilioFiscalReceptor>");
            sb.Append("<residenciaFiscal></residenciaFiscal>");
            sb.Append("<numRegIdTrib></numRegIdTrib>");
            sb.Append("<regimenFiscalReceptor>616</regimenFiscalReceptor>");
            sb.Append("<usoCFDI>S01</usoCFDI>");

            sb.Append("<Concepto>");
            foreach (var d in detalles)
            {
                var prod = _context.Products.FirstOrDefault(p => p.IdProducto == d.IdProducto);
                if (prod == null) continue; // Evita error si no existe

                // Cálculo impuesto
                decimal baseImpuesto = ((d.Precio - prod.Descuento) * d.Cantidad);
                decimal impuesto = baseImpuesto * (prod.ValorImpuesto / 100);
                totalImpuestosTrasladados += impuesto;

                sb.Append($"<claveProdServ>{prod.ClaveProductoSat}</claveProdServ>");
                sb.Append($"<noIdentificacion></noIdentificacion>\r\n");
                sb.Append($"<cantidad>{d.Cantidad}</cantidad>");
                sb.Append($"<claveUnidad>{prod.UnidadMedidaSat}</claveUnidad>");
                sb.Append($"<unidad>{EscapeXml(prod.UnidadMedida)}</unidad>");
                sb.Append($"<descripcion>{EscapeXml(d.DescripcionProducto)}</descripcion>");
                sb.Append($"<valorUnitario>{d.Precio:F2}</valorUnitario>");
                sb.Append($"<importe>{d.Total:F2}</importe>");
                sb.Append($"<descuento>{prod.Descuento:F2}</descuento>");
                sb.Append($"<objetoImp>{prod.ObjetoImpuesto}</objetoImp>");

                sb.Append("<Traslado>");
                sb.Append($"<base>{baseImpuesto:F2}</base>");
                sb.Append($"<impuesto>{prod.Impuesto}</impuesto>");
                sb.Append($"<tipoFactor>{prod.FactorImpuesto}</tipoFactor>");
                sb.Append($"<tasaOCuota>{prod.ValorImpuesto / 100}</tasaOCuota>");
                sb.Append($"<importe>{impuesto:F2}</importe>");
                sb.Append("</Traslado>");

                sb.Append("<rfcACuentaTerceros></rfcACuentaTerceros>");
                sb.Append("<nombreACuentaTerceros></nombreACuentaTerceros>");
                sb.Append("<regimenFiscalACuentaTerceros></regimenFiscalACuentaTerceros>");
                sb.Append("<domicilioFiscalACuentaTerceros></domicilioFiscalACuentaTerceros>");
                sb.Append("<numeroPedimento></numeroPedimento>");
                sb.Append("<cuentaPredial></cuentaPredial>");
            }
            sb.Append("</Concepto>");

            sb.Append("<totalImpuestosRetenidos></totalImpuestosRetenidos>");
            sb.Append($"<totalImpuestosTrasladados>{(venta.SubTotal - venta.Descuento) * (16/100)}</totalImpuestosTrasladados>");

            sb.Append("<Traslados>");
            sb.Append($"<base>{venta.SubTotal - venta.Descuento}</base>");
            sb.Append($"<impuesto>002</impuesto>");
            sb.Append($"<tipoFactor>Tasa</tipoFactor>");
            sb.Append($"<tasaOCuota>0.160000</tasaOCuota>");
            sb.Append($"<importe>{(venta.SubTotal - venta.Descuento) * (16/100)}</importe>");
            sb.Append("</Traslados>");
            sb.Append("</Comprobante>");

            return sb.ToString();
        }

        private string EscapeXml(string text)
        {
            return SecurityElement.Escape(text ?? "");
        }

    }


}

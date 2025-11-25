using System.Net;
using System.Text;

namespace VentasSystemAPI.Services.TimbradoService
{
    public class TimbradoService
    {
        public class TimbradoClient(HttpClient httpClient)
        {
            private readonly HttpClient _httpClient = httpClient;
            private const string URL = "https://ws.urbansa.com/app/timbrado.asmx";
            private const string SOAP_ACTION = "http://ws.urbansa.com/TimbrarF";
            private const string USER = "FIME";
            private const string PASS = "s9%4ns7q#eGq";

            public async Task<byte[]> TimbrarVentaAsync(string xmlGenerado)
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                string xml = xmlGenerado
                            .Replace("\r", "")
                            .Replace("\n", "")
                            .Trim();

                var soapEnvelope = $@"
                    <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:urn='http://ws.urbansa.com/'>
                      <soapenv:Body>
                        <urn:TimbrarF>
                          <Usuario>FIME</Usuario>
                          <Password>s9%4ns7q#eGq</Password>
                          <StrXml>{xml}</StrXml>
                        </urn:TimbrarF>
                      </soapenv:Body>
                    </soapenv:Envelope>";

                var request = new HttpRequestMessage(HttpMethod.Post, URL);
                request.Headers.Add("SOAPAction", "\"http://ws.urbansa.com/TimbrarF\""); 
                request.Headers.Add("User-Agent", "FIME-WINFORMS"); // <-- igual que la app del inge
                request.Content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");

                var response = await _httpClient.SendAsync(request);
                var respuestaXml = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error SOAP: {respuestaXml}");

                string base64 = ExtractBase64(respuestaXml);
                return Convert.FromBase64String(base64);
            }

            // Método para extraer base64 del XML de respuesta
            private static string ExtractBase64(string xmlResponse)
            {
                int start = xmlResponse.IndexOf("<TimbrarFResult>") + "<TimbrarFResult>".Length;
                int end = xmlResponse.IndexOf("</TimbrarFResult>");
                if (start < 0 || end < 0)
                    throw new Exception("No se encontró el resultado dentro del XML SOAP");

                return xmlResponse[start..end].Trim();
            }
        }
    }
}

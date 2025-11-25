using Newtonsoft.Json;
using System.Text;

namespace VentasSystemAPI.Data
{
    public class TimpradoApiClient
    {
        private readonly HttpClient _http;

        public TimpradoApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> TimbrarAsync(string xml)
        {
            var content = new StringContent($"\"{xml}\"", Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(
                "https://localhost:44332/api/timbrado/timbrar",
                content
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            var data = JsonConvert.DeserializeObject<dynamic>(
                await response.Content.ReadAsStringAsync()
            );

            return (string)data;
        }
    }
}

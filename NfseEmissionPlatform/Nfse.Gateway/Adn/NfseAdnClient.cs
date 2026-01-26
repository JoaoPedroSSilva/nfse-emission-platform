using System.Net.Http.Json;

namespace Nfse.Gateway.Adn
{
    public sealed class NfseAdnClient
    {
        private readonly HttpClient _http;

        public NfseAdnClient(HttpClient http) => _http = http;

        public async Task<RecepcaoResponseLote> SendAsync(List<string> dpsXmlDocuments, CancellationToken ct)
        {
            RecepcaoRequest req = new RecepcaoRequest
            {
                LoteXmlGZipB64 = dpsXmlDocuments.Select(GzipBase64.FromUtf8String).ToList()
            };

            HttpResponseMessage resp = await _http.PostAsJsonAsync("/adn/DFe", req, ct);
            resp.EnsureSuccessStatusCode();

            RecepcaoResponseLote? body = await resp.Content.ReadFromJsonAsync<RecepcaoResponseLote>(cancellationToken: ct);
            return body ?? throw new InvalidOperationException("Empty ADN response body.");
        }
    }
}

using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;


namespace Nfse.Application.Services
{
    public sealed class NfseGatewayClient
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public NfseGatewayClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<string> SubmitAsync(
            List<string> xmlDocuments,
            CancellationToken ct = default)
        {
            if (xmlDocuments == null || xmlDocuments.Count == 0)
                throw new ArgumentException("xmlDocuments is required.");

            using HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Post,
                "/api/adn/submit"
            );

            request.Headers.Add(
                "X-Api-Key",
                _config["Gateway:ApiKey"]
            );

            request.Content = JsonContent.Create(new
            {
                xmlDocuments
            });

            using HttpResponseMessage response = await _http.SendAsync(request, ct);

            string body = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"Gateway error {(int)response.StatusCode}: {body}"
                );
            }

            return body;
        }
    }
}

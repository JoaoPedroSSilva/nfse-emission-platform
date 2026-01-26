namespace Nfse.Gateway
{
    public sealed class GatewayOptions
    {
        public string ApiKey { get; set; } = "";
        public string AdnBaseUrl { get; set; } = "";
        public string ClientCertificateThumbprint { get; set; } = "";
        public int MaxBatchSize { get; set; } = 50;
    }
}

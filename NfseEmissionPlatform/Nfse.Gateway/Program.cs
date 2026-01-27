
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using Nfse.Gateway;
using Nfse.Gateway.Adn;
using Nfse.Gateway.Queue;
using Nfse.Gateway.Security;
using Nfse.Gateway.Workers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GatewayOptions>(builder.Configuration.GetSection("Gateway"));

builder.Services.AddSingleton<IEmissionQueue, ChannelEmissionQueue>();
builder.Services.AddSingleton<IJobStore, InMemoryJobStore>();

builder.Services.AddTransient<ApiKeyMiddleware>();

builder.Services.AddHttpClient<NfseAdnClient>((sp, http) =>
{
    GatewayOptions opt = sp.GetRequiredService<IOptions<GatewayOptions>>().Value;
    http.BaseAddress = new Uri(opt.AdnBaseUrl);
    http.Timeout = TimeSpan.FromSeconds(60);
})
.ConfigurePrimaryHttpMessageHandler(sp =>
{
    GatewayOptions opt = sp.GetRequiredService<IOptions<GatewayOptions>>().Value;
    X509Certificate2 cert = LoadCertFromStoreByThumbprint(opt.ClientCertificateThumbprint);

    HttpClientHandler handler = new HttpClientHandler();
    handler.ClientCertificates.Add(cert);
    handler.ClientCertificateOptions = ClientCertificateOption.Manual;

    return handler;
});

// Worker
builder.Services.AddHostedService<EmissionWorker>();

WebApplication app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Nfse.Gateway is running.");

// MVP endpoint: receive XML batch and send to ADN immediately (but still async via queue in next step)
app.MapPost("/api/adn/submit", async (
    SubmitXmlBatchRequest request,
    NfseAdnClient adn,
    IOptions<GatewayOptions> opt,
    IJobStore jobs) =>
{
    if (request.XmlDocuments is null || request.XmlDocuments.Count == 0)
        return Results.BadRequest("XmlDocuments is required.");

    if (request.XmlDocuments.Count > opt.Value.MaxBatchSize)
        return Results.BadRequest($"Max batch size is {opt.Value.MaxBatchSize}.");

    // MVP: call ADN now (synchronous). Next step: enqueue and process in background.
    var result = await adn.SendAsync(request.XmlDocuments, CancellationToken.None);

    return Results.Ok(result);
});

app.Run();

static X509Certificate2 LoadCertFromStoreByThumbprint(string thumbprint)
{
    if (string.IsNullOrWhiteSpace(thumbprint))
        throw new InvalidOperationException("ClientCertificateThumbprint is required.");

    thumbprint = thumbprint.Replace(" ", "", StringComparison.OrdinalIgnoreCase);

    using X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
    store.Open(OpenFlags.ReadOnly);

    X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false);
    if (certs.Count == 0)
        throw new InvalidOperationException("Client certificate not found in LocalMachine\\My.");

    return certs[0];
}

public sealed class SubmitXmlBatchRequest
{
    public List<string> XmlDocuments { get; set; } = new();
}
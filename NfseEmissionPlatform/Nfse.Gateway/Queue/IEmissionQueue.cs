namespace Nfse.Gateway.Queue
{
    public interface IEmissionQueue
    {
        ValueTask EnqueueAsync(EmissionJob job, CancellationToken ct);
        ValueTask<EmissionJob> DequeueAsync(CancellationToken ct);
    }
}

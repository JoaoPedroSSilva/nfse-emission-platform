namespace Nfse.Gateway.Queue
{
    public sealed record EmissionJob(
        Guid JobId,
        Guid IssuerId,
        List<Guid> DraftIds,
        DateTime CreatedAtUtc);
}

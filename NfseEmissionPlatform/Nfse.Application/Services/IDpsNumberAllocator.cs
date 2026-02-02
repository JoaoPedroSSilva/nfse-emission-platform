
namespace Nfse.Application.Services
{
    public interface IDpsNumberAllocator
    {
        Task<long> AllocateAsync(Guid issuerId, CancellationToken ct);
    }
}

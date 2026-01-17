using Nfse.Domain.Entities;

namespace Nfse.Application.Interfaces
{
    public interface IIssuerRepository
    {
        Task AddAsync(Issuer issuer, CancellationToken cancellationToken = default);
        Task<Issuer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

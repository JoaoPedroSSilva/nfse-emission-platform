using Nfse.Domain.Entities;

namespace Nfse.Application.Interfaces
{
    public interface IInvoiceDraftRepository
    {
        Task AddAsync(InvoiceDraft draft, CancellationToken cancellationToken = default);
        Task<IEnumerable<InvoiceDraft>> GetByIssuerAsync(Guid issuerId, CancellationToken cancellationToken = default);
    }
}

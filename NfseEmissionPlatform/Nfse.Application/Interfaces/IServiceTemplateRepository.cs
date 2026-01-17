using Nfse.Domain.Entities;

namespace Nfse.Application.Interfaces
{
    public interface IServiceTemplateRepository
    {
        Task AddAsync(ServiceTemplate service, CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceTemplate>> GetByIssuerAsync(Guid issuerId, CancellationToken cancellationToken = default);
    }
}

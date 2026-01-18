using Microsoft.EntityFrameworkCore;
using Nfse.Application.Interfaces;
using Nfse.Domain.Entities;
using Nfse.Infrastructure.Persistence;

namespace Nfse.Infrastructure.Repositories
{
    public class EfServiceTemplateRepository : IServiceTemplateRepository
    {
        private readonly AppDbContext _db;

        public EfServiceTemplateRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(ServiceTemplate service, CancellationToken cancellationToken = default)
        {
            _db.ServiceTemplates.Add(service);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ServiceTemplate>> GetByIssuerAsync(Guid issuerId, CancellationToken cancellationToken = default)
        {
            return await _db.ServiceTemplates
                .Where(x => x.IssuerId == issuerId && x.IsActive)
                .ToListAsync(cancellationToken);
        }
    }
}

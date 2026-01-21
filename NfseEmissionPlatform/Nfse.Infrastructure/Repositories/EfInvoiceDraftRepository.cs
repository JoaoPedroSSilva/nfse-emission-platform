using Microsoft.EntityFrameworkCore;
using Nfse.Application.Interfaces;
using Nfse.Domain.Entities;
using Nfse.Infrastructure.Persistence;

namespace Nfse.Infrastructure.Repositories
{
    public class EfInvoiceDraftRepository : IInvoiceDraftRepository
    {
        private readonly AppDbContext _db;

        public EfInvoiceDraftRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(InvoiceDraft draft, CancellationToken cancellationToken = default)
        {
            _db.InvoiceDrafts.Add(draft);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<InvoiceDraft>> GetByIssuerAsync(Guid issuerId, CancellationToken cancellationToken = default)
        {
            return await _db.InvoiceDrafts
                .AsNoTracking()
                .Where(x => x.IssuerId == issuerId)
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }
    }
}

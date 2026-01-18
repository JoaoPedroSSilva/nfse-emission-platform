using Microsoft.EntityFrameworkCore;
using Nfse.Application.Interfaces;
using Nfse.Domain.Entities;
using Nfse.Infrastructure.Persistence;

namespace Nfse.Infrastructure.Repositories
{
    public class EfIssuerRepository : IIssuerRepository
    {
        private readonly AppDbContext _db;

        public EfIssuerRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Issuer issuer, CancellationToken cancellationToken = default)
        {
            _db.Issuers.Add(issuer);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public Task<Issuer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _db.Issuers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Nfse.Application.Services;
using Nfse.Domain.Entities;
using Nfse.Infrastructure.Persistence;

namespace Nfse.Infrastructure.Services
{
    public sealed class EfDpsNumberAllocator : IDpsNumberAllocator
    {
        private readonly AppDbContext _db;

        public EfDpsNumberAllocator(AppDbContext db) => _db = db;

        public async Task<long> AllocateAsync(Guid issuerId, CancellationToken ct)
        {
           await using var tx = await _db.Database.BeginTransactionAsync(ct);

            var seq = await _db.Set<IssuerSequence>()
                .SingleOrDefaultAsync(x => x.IssuerId == issuerId, ct);

            if (seq is null)
            {
                seq = new IssuerSequence(issuerId);
                _db.Add(seq);
            }

            long number = seq.AllocateNext();

            await _db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            return number;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Nfse.Domain.Entities;

namespace Nfse.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Issuer> Issuers => Set<Issuer>();
        public DbSet<ServiceTemplate> ServiceTemplates => Set<ServiceTemplate>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Issuer>(b =>
            {
                b.ToTable("Issuers");
                b.HasKey(x => x.Id);

                b.Property(x => x.TenantId).IsRequired();

                b.OwnsOne(x => x.Cnpj, c =>
                {
                    c.Property(p => p.Value)
                        .HasColumnName("Cnpj")
                        .HasMaxLength(14)
                        .IsRequired();
                });

                b.Property(x => x.LegalName).HasMaxLength(200).IsRequired();
                b.Property(x => x.TradeName).HasMaxLength(200);
                b.Property(x => x.IsActive).IsRequired();
            });

            modelBuilder.Entity<ServiceTemplate>(b =>
            {
                b.ToTable("ServiceTemplates");
                b.HasKey(x => x.Id);

                b.Property(x => x.IssuerId).IsRequired();
                b.Property(x => x.NationalServiceCode).HasMaxLength(6).IsRequired();
                b.Property(x => x.Lc116Subitem).HasMaxLength(10);
                b.Property(x => x.Description).HasMaxLength(2000).IsRequired();
                b.Property(x => x.DefaultTaxRate).HasPrecision(9, 4);
                b.Property(x => x.IsIssWithheld).IsRequired();
                b.Property(x => x.IsActive).IsRequired();

                b.HasIndex(x => new { x.IssuerId, x.NationalServiceCode });
            });
        }
    }
}

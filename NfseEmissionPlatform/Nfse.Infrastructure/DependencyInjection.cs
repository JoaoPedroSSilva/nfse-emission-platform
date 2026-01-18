using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nfse.Application.Interfaces;
using Nfse.Infrastructure.Persistence;
using Nfse.Infrastructure.Repositories;

namespace Nfse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IIssuerRepository, EfIssuerRepository>();
            services.AddScoped<IServiceTemplateRepository, EfServiceTemplateRepository>();

            return services;
        }
    }
}

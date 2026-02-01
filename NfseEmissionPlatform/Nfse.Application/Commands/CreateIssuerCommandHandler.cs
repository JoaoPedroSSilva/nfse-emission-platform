using Nfse.Application.Interfaces;
using Nfse.Domain.Entities;
using Nfse.Domain.ValueObjects;

namespace Nfse.Application.Commands
{
    public class CreateIssuerCommandHandler
    {
        private readonly IIssuerRepository _issuerRepository;

        public CreateIssuerCommandHandler(IIssuerRepository issuerRepository)
        {
            _issuerRepository = issuerRepository;
        }

        public async Task<Guid> Handle(CreateIssuerCommand command, CancellationToken cancellationToken)
        {
            Cnpj cnpj = Cnpj.Create(command.Cnpj);

            Issuer issuer = new Issuer(
                command.TenantId,
                cnpj,
                command.LegalName,
                command.TradeName,
                command.IbgeCityCode7,
                command.MunicipalRegistration,
                command.SimplesNationalOption,
                command.SimplesNationalRegime,
                command.SpecialTaxRegime);

            await _issuerRepository.AddAsync(issuer, cancellationToken);

            return issuer.Id;
        }
    }
}

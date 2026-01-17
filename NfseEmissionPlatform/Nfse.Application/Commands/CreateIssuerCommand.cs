namespace Nfse.Application.Commands
{
    public record CreateIssuerCommand(
        Guid TenantId,
        string Cnpj,
        string LegalName,
        string TradeName);
}

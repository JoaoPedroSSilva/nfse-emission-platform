namespace Nfse.Application.Commands
{
    public record CreateIssuerCommand(
        Guid TenantId,
        string Cnpj,
        string LegalName,
        string? TradeName,
        string IbgeCityCode7,
        string? MunicipalRegistration,
        int SimplesNationalOption,
        int? SimplesNationalRegime,
        int SpecialTaxRegime);
}

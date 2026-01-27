namespace Nfse.Application.Services
{
    public sealed record DpsBuildModel(
        // Root/infDPS
        string LayoutVersion,          // "1.01"
        string DpsId,                  // "DPS" + digits (45 chars)
        string EnvironmentCode,        // "1" or "2"
        DateTime IssueDateUtc,         // UTC
        string AppVersion,             // e.g. "NfseEmissionPlatform/0.1"
        string Series,                 // 1..5 digits (string, will be zero-padded outside if needed)
        string DpsNumber,              // 1..15 digits, no leading zero unless padded explicitly
        DateTime ServiceDate,          // competence date (yyyyMMdd)
        string EmitterTypeCode,        // "1" (provider)
        string IssuerIbgeCityCode7,    // cLocEmi (7-digit IBGE)

        // Provider (prest)
        string ProviderCnpj,           // 14 digits
        string? ProviderMunicipalRegistration, // IM (optional)
        int SimplesNationalOption,     // opSimpNac (1,2,3)
        int? SimplesNationalRegime,    // regApTribSN (opcional)
        int SpecialTaxRegime,          // regEspTrib (0..9)

        // Recipient (toma)
        string RecipientDocument,      // CPF (11) or CNPJ (14)
        string RecipientName,
        string? RecipientEmail,

        // Service (serv)
        string ServiceIbgeCityCode7,   // locPrest (usually same as cLocEmi)
        string NationalServiceCode,    // code from ServiceTemplate (6 digits)
        string ServiceDescription,     // free text 
        string? MunicipalTaxCode,
        string? NbsCode,
        string? ContributorInternalCode,

        // Values (valores)
        decimal ServiceAmount,         // vServPrest 

        // Trib
        string IssTaxationCode,     // tribISSQN: "1","2","3","4"
        string IssWithholdingType,  // tpRetISSQN: "1","2","3"
        decimal? IssRatePercent    // pAliq (optional) e.g. 2.00, 5.00

    );
}

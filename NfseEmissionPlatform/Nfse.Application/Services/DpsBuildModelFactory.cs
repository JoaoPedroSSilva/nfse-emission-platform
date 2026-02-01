using Nfse.Domain.Entities;

namespace Nfse.Application.Services
{
    public sealed class DpsBuildModelFactory
    {
        public DpsBuildModel Create(
            InvoiceDraft draft,
            Issuer issuer,
            string appVersion)
        {
            return new DpsBuildModel(
                LayoutVersion: "1.01",
                DpsId: BuildDpsId(draft, issuer),
                EnvironmentCode: "2",
                IssueDateUtc: DateTime.UtcNow,
                AppVersion: appVersion,
                Series: "1",
                DpsNumber: draft.Id.ToString("N")[..15],
                ServiceDate: DateTime.UtcNow.Date,
                EmitterTypeCode: "1",
                IssuerIbgeCityCode7: issuer.IbgeCityCode7,

                ProviderCnpj: issuer.Cnpj.Value,
                ProviderMunicipalRegistration: issuer.MunicipalRegistration,
                SimplesNationalOption: issuer.SimplesNationalOption,
                SimplesNationalRegime: issuer.SimplesNationalRegime,
                SpecialTaxRegime: issuer.SpecialTaxRegime,


                RecipientDocument: draft.RecipientDocument,
                RecipientName: draft.RecipientName,
                RecipientEmail: null,

                ServiceIbgeCityCode7: issuer.IbgeCityCode7,
                NationalServiceCode: draft.NationalServiceCode,
                ServiceDescription: draft.ServiceDescription,
                MunicipalTaxCode: null,
                NbsCode: null,
                ContributorInternalCode: null,

                ServiceAmount: draft.Amount,

                IssTaxationCode: "1",
                IssWithholdingType: draft.IsIssWithheld ? "2" : "1",
                IssRatePercent: draft.TaxRate,
                IrrfWithheldAmount: draft.IrrfWithheldAmount
            );
        }

        private static string BuildDpsId(InvoiceDraft draft, Issuer issuer)
        {
            return $"DPS{issuer.IbgeCityCode7}{issuer.Cnpj.Value}{draft.Id:N}"[..45];
        }
    }
}

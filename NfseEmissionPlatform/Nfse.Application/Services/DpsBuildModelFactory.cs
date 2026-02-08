using Nfse.Domain.Entities;
using System.Threading.Tasks;

namespace Nfse.Application.Services
{
    public sealed class DpsBuildModelFactory
    {
        
        private readonly IDpsNumberAllocator _allocator;

        public DpsBuildModelFactory(IDpsNumberAllocator allocator)
        {
            _allocator = allocator;
        }
        public async Task<DpsBuildModel> CreateAsync(
            InvoiceDraft draft,
            Issuer issuer,
            string appVersion,
            CancellationToken ct)
        {
            const string seriesXml = "900";
            const string tpEmit = "1";
            const string tpAmb = "2";

            long nDps = await _allocator.AllocateAsync(issuer.Id, ct);
            string nDpsXml = nDps.ToString();
            string nDps15 = nDps.ToString().PadLeft(15, '0');
            string serie5 = seriesXml.PadLeft(5, '0');
            string cMun7 = OnlyDigits(issuer.IbgeCityCode7).PadLeft(7, '0');

            const string tpInsFed = "1";
            string insFed14 = OnlyDigits(issuer.Cnpj.Value).PadLeft(14, '0');

            string dpsId = BuildDpsId(cMun7, tpInsFed, insFed14, serie5, nDps15);

            
            return new DpsBuildModel(
                LayoutVersion: "1.01",
                DpsId: dpsId,
                EnvironmentCode: tpAmb,
                IssueDateUtc: DateTime.UtcNow,
                AppVersion: appVersion,
                Series: seriesXml,
                DpsNumber: nDpsXml,
                ServiceDate: DateTime.UtcNow.Date,
                EmitterTypeCode: tpEmit,
                IssuerIbgeCityCode7: cMun7,

                ProviderCnpj: insFed14,
                ProviderMunicipalRegistration: issuer.MunicipalRegistration,
                SimplesNationalOption: issuer.SimplesNationalOption,
                SimplesNationalRegime: issuer.SimplesNationalRegime,
                SpecialTaxRegime: issuer.SpecialTaxRegime,


                RecipientDocument: OnlyDigits(draft.RecipientDocument),
                RecipientName: draft.RecipientName,
                RecipientEmail: null,

                ServiceIbgeCityCode7: cMun7,
                NationalServiceCode: OnlyDigits(draft.NationalServiceCode).PadLeft(6, '0'),
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

        private static string BuildDpsId(
            string cMun7,
            string tpInsFed1,
            string insFed14,
            string serie5,
            string nDps15)
        {
            return $"DPS{cMun7}{tpInsFed1}{insFed14}{serie5}{nDps15}";
        }

        private static string OnlyDigits(string s)
            => new string((s ?? "").Where(char.IsDigit).ToArray());
    }
}

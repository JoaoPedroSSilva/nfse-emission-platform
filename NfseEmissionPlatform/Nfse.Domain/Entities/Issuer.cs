using Nfse.Domain.Common;
using Nfse.Domain.ValueObjects;

namespace Nfse.Domain.Entities
{
    public class Issuer : Entity
    {
        public Guid TenantId { get; private set; }
        public Cnpj Cnpj { get; private set; }

        public string LegalName { get; private set; }
        public string? TradeName { get; private set; }
        public bool IsActive { get; private set; }

        public string IbgeCityCode7 { get; private set; } = ""; // 7 digits
        public string? MunicipalRegistration { get; private set; } // IM 

        public int SimplesNationalOption { get; private set; } // 1,2,3
        public int? SimplesNationalRegime { get; private set; } // 1..3 
        public int SpecialTaxRegime { get; private set; } // 0..9

        private Issuer() { }

        public Issuer(
            Guid tenantId,
            Cnpj cnpj,
            string legalName,
            string? tradeName,
            string ibgeCityCode7,
            string? municipalRegistration,
            int simplesNationalOption,
            int? simplesNationalRegime,
            int specialTaxRegime)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException("TenantId is required.");

            if (cnpj is  null) throw new ArgumentNullException(nameof(cnpj));

            if (string.IsNullOrWhiteSpace(legalName))
                throw new ArgumentException("Legal name is required.");

            ibgeCityCode7 = OnlyDigits(ibgeCityCode7);
            if (ibgeCityCode7.Length != 7) throw new ArgumentException("IbgeCityCode7 must have 7 digits.");

            if (simplesNationalOption is < 1 or > 3)
                throw new ArgumentException("SimplesNationalOption must be 1, 2 or 3.");

            if (simplesNationalOption == 3)
            {
                if (simplesNationalRegime is null)
                    throw new ArgumentException("SimplesNationalRegime is required when SimplesNationalOption is 3.");

                if (simplesNationalRegime is < 1 or > 3)
                    throw new ArgumentException("SimplesNationalRegime must be 1, 2 or 3 when provided.");
            }
            else
            {
                // For option 1 or 2, regime should be null
                simplesNationalRegime = null;
            }

            if (specialTaxRegime is < 0 or > 9)
                throw new ArgumentException("SpecialTaxRegime must be between 0 and 9.");

            TenantId = tenantId;
            Cnpj = cnpj;
            LegalName = legalName.Trim();
            TradeName = string.IsNullOrWhiteSpace(tradeName) ? null : tradeName.Trim();

            IbgeCityCode7 = ibgeCityCode7;
            MunicipalRegistration = string.IsNullOrWhiteSpace(municipalRegistration)
                ? null
                : OnlyDigits(municipalRegistration);

            SimplesNationalOption = simplesNationalOption;
            SimplesNationalRegime = simplesNationalRegime;
            SpecialTaxRegime = specialTaxRegime;

            IsActive = true;
        }

        public void UpdateMunicipalProfile(string ibgeCityCode7, string? municipalRegistration)
        {
            ibgeCityCode7 = OnlyDigits(ibgeCityCode7);

            IbgeCityCode7 = ibgeCityCode7;
            MunicipalRegistration = string.IsNullOrWhiteSpace(municipalRegistration)
                ? null
                : OnlyDigits(municipalRegistration);
        }

        public void UpdateTaxRegime(int simplesNationalOption, int? simplesNationalRegime, int specialTaxRegime)
        {
            if (simplesNationalOption is < 1 or > 3)
                throw new ArgumentException("SimplesNationalOption must be 1, 2 or 3.");

            if (simplesNationalOption == 3)
            {
                if (simplesNationalRegime is null)
                    throw new ArgumentException("SimplesNationalRegime is required when SimplesNationalOption is 3.");

                if (simplesNationalRegime is < 1 or > 3)
                    throw new ArgumentException("SimplesNationalRegime must be 1, 2 or 3 when provided.");
            }
            else
            {
                simplesNationalRegime = null;
            }

            if (specialTaxRegime is < 0 or > 9)
                throw new ArgumentException("SpecialTaxRegime must be between 0 and 9.");

            SimplesNationalOption = simplesNationalOption;
            SimplesNationalRegime = simplesNationalRegime;
            SpecialTaxRegime = specialTaxRegime;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        private static string OnlyDigits(string s)
        => new string((s ?? "").Where(char.IsDigit).ToArray());
    }
}

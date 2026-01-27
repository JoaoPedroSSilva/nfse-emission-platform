using System.Globalization;
using System.Xml.Linq;

namespace Nfse.Application.Services
{
    public sealed class DpsXmlBuilder
    {
        private static readonly XNamespace Ns = "http://www.sped.fazenda.gov.br/nfse";
        private static readonly XNamespace Ds = "http://www.w3.org/2000/09/xmldsig#";

        public string Build(DpsBuildModel m)
        {
            var dps = new XElement(Ns + "DPS",
                new XAttribute(XNamespace.Xmlns + "ds", Ds),
                new XAttribute("versao", m.LayoutVersion),
                new XElement(Ns + "infDPS",
                    new XAttribute("Id", m.DpsId),

                    new XElement(Ns + "tpAmb", m.EnvironmentCode),
                    new XElement(Ns + "dhEmi", FormatUtc(m.IssueDateUtc)),
                    new XElement(Ns + "verAplic", m.AppVersion),
                    new XElement(Ns + "serie", m.Series),
                    new XElement(Ns + "nDPS", m.DpsNumber),
                    new XElement(Ns + "dCompet", m.ServiceDate.ToString("yyyyMMdd")),
                    new XElement(Ns + "tpEmit", m.EmitterTypeCode),
                    new XElement(Ns + "cLocEmi", m.IssuerIbgeCityCode7),

                    BuildPrest(m),
                    BuildToma(m),
                    BuildServ(m),
                    BuildValores(m)
                )
            );

            return new XDocument(new XDeclaration("1.0", "utf-8", null), dps)
                .ToString(SaveOptions.DisableFormatting);
        }

        private static XElement BuildPrest(DpsBuildModel m)
        {
            XElement prest = new XElement(Ns + "prest",
                new XElement(Ns + "CNPJ", OnlyDigits(m.ProviderCnpj))
            );

            if (!string.IsNullOrWhiteSpace(m.ProviderMunicipalRegistration))
                prest.Add(new XElement(Ns + "IM", OnlyDigits(m.ProviderMunicipalRegistration)));

            prest.Add(BuildRegTrib(m));

            return prest;
        }

        private static XElement BuildRegTrib(DpsBuildModel m)
        {
            XElement regTrib = new XElement(Ns + "regTrib",
                new XElement(Ns + "opSimpNac", m.SimplesNationalOption),
                new XElement(Ns + "regEspTrib", m.SpecialTaxRegime)
            );

            if (m.SimplesNationalOption == 3 && m.SimplesNationalRegime.HasValue)
            {
                regTrib.Add(new XElement(Ns + "regApTribSN", m.SimplesNationalRegime.Value));
            }

            return regTrib;
        }

        private static XElement BuildToma(DpsBuildModel m)
        {
            XElement toma = new XElement(Ns + "toma");

            string docDigits = OnlyDigits(m.RecipientDocument);

            if (docDigits.Length == 11)
                toma.Add(new XElement(Ns + "CPF", docDigits));
            else if (docDigits.Length == 14)
                toma.Add(new XElement(Ns + "CNPJ", docDigits));
            else
                throw new ArgumentException("RecipientDocument must be CPF (11) or CNPJ (14).");

            toma.Add(new XElement(Ns + "xNome", m.RecipientName));

            if (!string.IsNullOrWhiteSpace(m.RecipientEmail))
                toma.Add(new XElement(Ns + "email", m.RecipientEmail));

            return toma;
        }

        private static XElement BuildServ(DpsBuildModel m)
        {
            return new XElement(Ns + "serv",
                BuildLocPrest(m),
                BuildCServ(m)
            );
        }

        private static XElement BuildLocPrest(DpsBuildModel m)
        {
            string codMun = OnlyDigits(m.ServiceIbgeCityCode7).PadLeft(7, '0');

            return new XElement(Ns + "locPrest",
                new XElement(Ns + "cLocPrestacao", codMun)
            );
        }

        private static XElement BuildCServ(DpsBuildModel m)
        {
            string cTribNac = OnlyDigits(m.NationalServiceCode).PadLeft(6, '0');

            XElement cServ = new XElement(Ns + "cServ",
                new XElement(Ns + "cTribNac", cTribNac),
                new XElement(Ns + "xDescServ", m.ServiceDescription)
            );

            if (!string.IsNullOrWhiteSpace(m.MunicipalTaxCode))
                cServ.Add(new XElement(Ns + "cTribMun", m.MunicipalTaxCode.Trim()));

            if (!string.IsNullOrWhiteSpace(m.NbsCode))
                cServ.Add(new XElement(Ns + "cNBS", m.NbsCode.Trim()));

            if (!string.IsNullOrWhiteSpace(m.ContributorInternalCode))
                cServ.Add(new XElement(Ns + "cIntContrib", m.ContributorInternalCode.Trim()));

            return cServ;
        }

        private static XElement BuildValores(DpsBuildModel m)
        {
            return new XElement(Ns + "valores",
                BuildVServPrest(m),
                BuildTrib(m)
            );
        }

        private static XElement BuildVServPrest(DpsBuildModel m)
        {
            return new XElement(Ns + "vServPrest",
                new XElement(Ns + "vServ", FormatDecimal(m.ServiceAmount))
            );
        }

        private static XElement BuildTrib(DpsBuildModel m)
        {
            return new XElement(Ns + "trib",
                new XElement(Ns + "TODO", "0")
            );
        }

        private static string FormatUtc(DateTime utc)
        {
            DateTime u = utc.Kind == DateTimeKind.Utc ? utc : utc.ToUniversalTime();
            return u.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        }

        private static string FormatDecimal(decimal value)
            => value.ToString("0.00", CultureInfo.InvariantCulture);

        private static string OnlyDigits(string s)
            => new string((s ?? "").Where(char.IsDigit).ToArray());
    }
}

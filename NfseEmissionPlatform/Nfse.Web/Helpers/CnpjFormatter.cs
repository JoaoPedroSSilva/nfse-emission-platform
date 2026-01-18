namespace Nfse.Web.Helpers
{
    public static class CnpjFormatter
    {
        public static string Format(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
                return cnpj;

            return $"{cnpj[..2]}.{cnpj.Substring(2, 3)}.{cnpj.Substring(5, 3)}/{cnpj.Substring(8, 4)}-{cnpj.Substring(12, 2)}";
        }
    }
}

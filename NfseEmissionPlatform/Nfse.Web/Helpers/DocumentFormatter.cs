namespace Nfse.Web.Helpers
{
    public static class DocumentFormatter
    {
        public static string FormatCpfOrCnpj(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                return document;

            string digits = new string(document.Where(char.IsDigit).ToArray());

            return digits.Length switch
            {
                11 => $"{digits[..3]}.{digits.Substring(3, 3)}.{digits.Substring(6, 3)}-{digits.Substring(9, 2)}",
                14 => $"{digits[..2]}.{digits.Substring(2, 3)}.{digits.Substring(5, 3)}/{digits.Substring(8, 4)}-{digits.Substring(12, 2)}",
                _ => document
            };
        }
    }
}

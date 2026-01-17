using System.Text.RegularExpressions;

namespace Nfse.Domain.ValueObjects
{
    public sealed class Cnpj
    {
        public string Value { get; }

        private Cnpj(string value) 
        { 
            Value = value; 
        }

        public static Cnpj Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CNPJ cannot be empty.");

            string digits = Regex.Replace(value, @"\D", "");

            if (digits.Length != 14)
                throw new ArgumentException("CNPJ must contain 14 digits.");

            return new Cnpj(digits);
        }

        public override string ToString() => Value; 
    }
}

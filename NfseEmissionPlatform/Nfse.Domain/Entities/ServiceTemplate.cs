using Nfse.Domain.Common;

namespace Nfse.Domain.Entities
{
    public class ServiceTemplate : Entity
    {
        public Guid IssuerId { get; private set; }

        public string NationalServiceCode { get; private set; }

        public string Lc116Subitem { get; private set; }

        public string Description { get; private set; }

        public decimal? DefaultTaxRate { get; private set; }

        public bool IsIssWithheld { get; private set; }

        public bool IsActive { get; private set; }

        private ServiceTemplate() { }

        public ServiceTemplate(
            Guid issuerId,
            string nationalServiceCode,
            string lc116Subitem,
            string description,
            decimal? defaultTaxRate,
            bool isIssWithheld)
        {
            if (issuerId == Guid.Empty)
                throw new ArgumentException("IssuerId is required.");

            if (string.IsNullOrWhiteSpace(nationalServiceCode))
                throw new ArgumentException("Service description is required.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Service description is required.");

            IssuerId = issuerId;
            NationalServiceCode = nationalServiceCode.Trim();
            Lc116Subitem = lc116Subitem?.Trim() ?? string.Empty;
            Description = description.Trim();
            DefaultTaxRate = defaultTaxRate;
            IsIssWithheld = isIssWithheld;
            IsActive = true;
        }

        public void Update(
            string description,
            decimal? defaultTaxRate,
            bool isIssWithheld)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Service description is required.");

            Description = description.Trim();
            DefaultTaxRate = defaultTaxRate;
            IsIssWithheld = isIssWithheld;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}

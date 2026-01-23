using Nfse.Domain.Common;

namespace Nfse.Domain.Entities
{
    public class InvoiceDraft : Entity
    {
        public Guid IssuerId { get; private set; }
        public Guid? ServiceTemplateId { get; private set; }
        public string NationalServiceCode { get; private set; }

        // Recipient (Tomador) - MVP: store as plain strings
        public string RecipientName { get; private set; }
        public string RecipientDocument { get; private set; } // CPF/CNPJ digits only

        // Values
        public string ServiceDescription { get; private set; }
        public decimal Amount { get; private set; }
        public decimal? TaxRate { get; private set; }
        public bool IsIssWithheld { get; private set; }

        // Control
        public InvoiceDraftStatus Status { get; private set; }
        public string? ErrorMessage { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? SubmittedAtUtc { get; private set; }

        private InvoiceDraft() { } // For ORM

        public InvoiceDraft(
            Guid issuerId,
            Guid? serviceTemplateId,
            string nationalServiceCode,
            string recipientName,
            string recipientDocument,
            string serviceDescription,
            decimal amount,
            decimal? taxRate,
            bool isIssWithheld)
        {
            if (issuerId == Guid.Empty) throw new ArgumentException("IssuerId is required.");
            if (string.IsNullOrWhiteSpace(nationalServiceCode) || nationalServiceCode.Trim().Length != 6)
                throw new ArgumentException("NationalServiceCode must have 6 digits.");
            if (string.IsNullOrWhiteSpace(recipientName)) throw new ArgumentException("Recipient name is required.");
            if (string.IsNullOrWhiteSpace(recipientDocument)) throw new ArgumentException("Recipient document is required.");
            if (string.IsNullOrWhiteSpace(serviceDescription)) throw new ArgumentException("Service description is required.");
            if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

            IssuerId = issuerId;
            ServiceTemplateId = serviceTemplateId;
            NationalServiceCode = nationalServiceCode.Trim();

            RecipientName = recipientName.Trim();
            RecipientDocument = recipientDocument.Trim();
            ServiceDescription = serviceDescription.Trim();

            Amount = amount;
            TaxRate = taxRate;
            IsIssWithheld = isIssWithheld;

            Status = InvoiceDraftStatus.Draft;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void MarkReady()
        {
            Status = InvoiceDraftStatus.Ready;
            ErrorMessage = null;
        }

        public void MarkError(string message)
        {
            Status = InvoiceDraftStatus.Error;
            ErrorMessage = string.IsNullOrWhiteSpace(message) ? "Unknown error." : message.Trim();
        }

        public void MarkSubmitted()
        {
            Status = InvoiceDraftStatus.Submitted;
            SubmittedAtUtc = DateTime.UtcNow;
            ErrorMessage = null;
        }

        public void AssignServiceTemplate(Guid serviceTemplateId, decimal? defaultTaxRate, bool defaultIsIssWithheld)
        {
            if (serviceTemplateId == Guid.Empty)
                throw new ArgumentException("ServiceTemplateId is required.");

            ServiceTemplateId = serviceTemplateId;

            TaxRate ??= defaultTaxRate;
            IsIssWithheld = IsIssWithheld || defaultIsIssWithheld;
        }
    }
}

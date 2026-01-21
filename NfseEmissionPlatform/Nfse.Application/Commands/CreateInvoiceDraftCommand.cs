namespace Nfse.Application.Commands
{
    public record CreateInvoiceDraftCommand(
        Guid IssuerId,
        Guid ServiceTemplateId,
        string RecipientName,
        string RecipientDocument,
        decimal Amount,
        decimal? TaxRate,
        bool IsIssWithheld);
}

namespace Nfse.Application.Commands
{
    public record CreateServiceTemplateCommand(
        Guid IssuerId,
        string NationalServiceCode,
        string Lc116Subitem,
        string Description,
        decimal? DefaultTaxRate,
        bool IsIssWithheld);
}

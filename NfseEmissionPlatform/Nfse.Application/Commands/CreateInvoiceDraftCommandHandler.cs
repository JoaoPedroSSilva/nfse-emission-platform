using Nfse.Application.Interfaces;
using Nfse.Domain.Entities;

namespace Nfse.Application.Commands
{
    public class CreateInvoiceDraftCommandHandler
    {
        private readonly IInvoiceDraftRepository _repo;

        public CreateInvoiceDraftCommandHandler(IInvoiceDraftRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateInvoiceDraftCommand command, CancellationToken cancellationToken)
        {
            InvoiceDraft draft = new InvoiceDraft(
                command.IssuerId,
                command.ServiceTemplateId,
                command.NationalServiceCode,
                command.RecipientName,
                command.RecipientDocument,
                command.ServiceDescription,
                command.Amount,
                command.TaxRate,
                command.IsIssWithheld);

            await _repo.AddAsync(draft, cancellationToken);
            return draft.Id;
        }
    }
}

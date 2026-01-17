using Nfse.Application.Interfaces;
using Nfse.Domain.Entities;

namespace Nfse.Application.Commands
{
    public class CreateServiceTemplateCommandHandler
    {
        private readonly IServiceTemplateRepository _serviceTemplateRepository;

        public CreateServiceTemplateCommandHandler(IServiceTemplateRepository serviceRepository)
        {
            _serviceTemplateRepository = serviceRepository;
        }

        public async Task<Guid> Handle(CreateServiceTemplateCommand command, CancellationToken cancellationToken)
        {
            ServiceTemplate service = new ServiceTemplate(
                command.IssuerId,
                command.NationalServiceCode,
                command.Lc116Subitem,
                command.Description,
                command.DefaultTaxRate,
                command.IsIssWithheld);

            await _serviceTemplateRepository.AddAsync(service, cancellationToken);

            return service.Id;
        }
    }
}

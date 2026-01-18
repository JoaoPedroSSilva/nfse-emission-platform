using Nfse.Domain.Common;
using Nfse.Domain.ValueObjects;
using System.Diagnostics;

namespace Nfse.Domain.Entities
{
    public class Issuer : Entity
    {
        public Guid TenantId { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public string LegalName { get; private set; }

        public string TradeName { get; private set; }

        public bool IsActive { get; private set; }

        private Issuer() { }

        public Issuer(
            Guid tenantId,
            Cnpj cnpj,
            string legalName,
            string tradeName)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException("TenantId is required.");

            if (string.IsNullOrWhiteSpace(legalName))
                throw new ArgumentException("Legal name is required.");

            TenantId = tenantId;
            Cnpj = cnpj;
            LegalName = legalName;
            TradeName = tradeName;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}

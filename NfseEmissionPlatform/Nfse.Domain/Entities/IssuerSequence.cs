using Nfse.Domain.Common;

namespace Nfse.Domain.Entities
{
    public sealed class IssuerSequence : Entity
    {
        public Guid IssuerId {  get; private set; }
        public long NextDpsNumber { get; private set; } = 1;

        private IssuerSequence() { }

        public IssuerSequence(Guid issuerId)
        {
            if (issuerId == Guid.Empty) throw new ArgumentException("IssuerId is required.");
            IssuerId = issuerId;
            NextDpsNumber = 1;
        }

        public long AllocateNext()
        {
            long current = NextDpsNumber;
            NextDpsNumber++;
            return current;
        }
    }
}

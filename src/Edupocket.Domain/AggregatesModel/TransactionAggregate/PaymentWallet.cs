using Edupocket.Domain.Enums;
using Edupocket.Domain.SeedWork;

namespace Edupocket.Domain.AggregatesModel.TransactionAggregate
{
    public class PaymentWallet: BaseEntity<Guid>
    {
        public string WalletNumber { get; private set; }
        public decimal Balance { get; private set; }
        public Status Status { get; private set; }
        public string CheckSum { get; private set; }
        public bool IsPndActive { get; private set; }


    }
}

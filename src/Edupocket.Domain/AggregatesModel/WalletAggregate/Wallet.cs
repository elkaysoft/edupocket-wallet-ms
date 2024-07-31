using BCrypt.Net;
using Edupocket.Domain.Enums;
using Edupocket.Domain.SeedWork;

namespace Edupocket.Domain.AggregatesModel.WalletAggregate
{
    public class Wallet: BaseEntity<Guid>
    {
        public string WalletNumber { get; private set; }
        public decimal Balance { get; private set; }
        public Guid ProfileId { get; private set; }
        public Status Status { get; private set; }
        public string CheckSum { get; private set; }
        public bool IsPndActive { get; private set; }        

        public Wallet(Guid id): base(id)
        {            
        }

        private Wallet(): base(Guid.NewGuid())  // required by EF
        { }

        public static Wallet Create(Guid profileId, string walletNumber)
        {
            Wallet wallet = new Wallet()
            {
                ProfileId = profileId,                
                IsPndActive = true,
                Status = Status.Active,
                Balance = 0
            };

            return wallet;
        }

        public void BalanceUpdate(decimal amount)
        {
            if (!this.ValidateWalletCheckSum())
                return;

            Balance += amount;
            this.GetCheckSum();
        }



    }
}

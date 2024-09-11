using Edupocket.Domain.AggregatesModel.WalletAggregate;
using Edupocket.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.DAL.Contracts
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        IRepository<Wallet> WalletRepository { get; }
        IRepository<Profile> ProfileRepository { get; }
        IRepository<Beneficiary> BeneficiaryRepository { get; }

    }
}

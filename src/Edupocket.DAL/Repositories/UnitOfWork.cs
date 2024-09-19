using Edupocket.DAL.Contracts;
using Edupocket.Domain.AggregatesModel.WalletAggregate;
using Edupocket.Infrastructure;
using Edupocket.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalletDbContext _dbContext;
        private Repository<Wallet> walletRepository;
        private Repository<Profile> profileRepository;
        private Repository<Beneficiary> beneficiaryRepository;

        private IDbContextTransaction _transaction;

        public UnitOfWork(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      

        public IRepository<Wallet> WalletRepository => walletRepository ?? new Repository<Wallet>(_dbContext);

        public IRepository<Profile> ProfileRepository => profileRepository ?? new Repository<Profile>(_dbContext);

        public IRepository<Beneficiary> BeneficiaryRepository => beneficiaryRepository ?? new Repository<Beneficiary>(_dbContext);

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _dbContext.SaveChanges();
                _transaction.Commit();
            }
            catch(Exception ex)
            {
                ex.ToString();
                _transaction.Rollback();
            }
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing) { _dbContext.Dispose(); }
        }


    }
}

using Edupocket.Infrastructure;
using Edupocket.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Edupocket.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly WalletDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }                

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);  
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}

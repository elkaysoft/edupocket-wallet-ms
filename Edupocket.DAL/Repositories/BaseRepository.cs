using Edupocket.Domain.SeedWork;
using Edupocket.Infrastructure;
using Edupocket.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Edupocket.DAL.Repositories
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private readonly WalletDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>(); 
        }        


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
             await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false)
        {
            var query = _dbSet.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();
            }

            return await query.CountAsync(predicate);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var query = _dbSet.AsQueryable();

            foreach(var expression  in includeExpressions)
            {
                query = query.Include(expression);
            }

            if (includeDeleted) 
            {
                query = query.IgnoreQueryFilters();
            }

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var query = _dbSet.AsQueryable();

            foreach(var entity in includeExpressions)
            {
                query = query.Include(entity);
            }
            if (includeDeleted) 
                query = query.IgnoreQueryFilters();

            if(predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<PagedListModel<TEntity>> GetPagedFilteredAsync(Expression<Func<TEntity, bool>> filter, int page, int pageSize, string sortColumn, string sortOrder, bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var query = _dbSet.AsQueryable();

            foreach(var entity in includeExpressions)
                 query = query.Include(entity); 

            if(includeDeleted)
                query = query.IgnoreQueryFilters();

            if(filter != null)
                query = query.Where(filter);

            query = ApplySorting(query, sortColumn, sortOrder);

            int totalRecords = await query.CountAsync();
            var items = await query.Skip(( page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedListModel<TEntity> { Items = items, TotalPages = totalRecords, CurrentPage = page };
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var query = _dbSet.AsQueryable();
            
            foreach(var entity in includeExpressions)
                query = query.Include(entity);

            if(includeDeleted) query = query.IgnoreQueryFilters();

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, decimal>> selector, bool includeDeleted = false)
        {
            var query = _dbSet.AsQueryable();

            if(includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query.Where(predicate).SumAsync(selector);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string sortColumn, string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, sortColumn);
            var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);

            if (sortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(lambda);
            }
            else
            {
                query = query.OrderBy(lambda);
            }

            return query;
        }

    }
}

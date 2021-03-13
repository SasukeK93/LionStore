using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LionStore.Core.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        // Find
        ValueTask<TEntity> FindByIdAsync(int id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        // List
        Task<List<TEntity>> ListAllAsync();
        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter);

        // CRUD
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity);
        ValueTask InsertRangeAsync(IEnumerable<TEntity> entity);
        EntityEntry<TEntity> Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entity);
        EntityEntry<TEntity> Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entity);

        // Aggregate
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}

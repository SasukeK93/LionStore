using LionStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LionStore.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;
        private readonly CancellationToken _cancellationToken;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            _cancellationToken = new CancellationToken();
        }
        public BaseRepository(DbContext context, CancellationToken cancellationToken)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            _cancellationToken = cancellationToken;
        }

        #region Find
        public ValueTask<TEntity> FindByIdAsync(int id)
        {
            return _entities.FindAsync(id);
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            /*
            TEntity value = await _entities.FirstOrDefaultAsync(filter, cancellationToken: _cancellationToken);
            if (value == null)
                value = default;
            return value ??= default;
            */
            return await _entities.FirstOrDefaultAsync(filter, cancellationToken: _cancellationToken);
        }
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.SingleOrDefaultAsync(filter, _cancellationToken);
        }
        #endregion

        #region List
        public async Task<List<TEntity>> ListAllAsync()
        {
            return await _entities.ToListAsync(cancellationToken: _cancellationToken);
        }
        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.Where(filter).ToListAsync(_cancellationToken);
        }
        #endregion

        #region CRUD
        public async ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return await _entities.AddAsync(entity);
        }
        public async ValueTask InsertRangeAsync(IEnumerable<TEntity> entity)
        {
            await _entities.AddRangeAsync(entity);
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            return _entities.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entity)
        {
            _entities.UpdateRange(entity);
        }

        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            return _entities.Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            _entities.RemoveRange(entity);
        }
        #endregion

        #region Aggregate
        public async Task<int> CountAsync()
        {
            return await _entities.CountAsync(_cancellationToken);
        }
        public async Task<int> CountAsync(ISpecification<TEntity> spec)
        {
            return await _entities.CountAsync(_cancellationToken);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.CountAsync(filter, _cancellationToken);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.AnyAsync(filter, _cancellationToken);
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        #endregion
    }
}

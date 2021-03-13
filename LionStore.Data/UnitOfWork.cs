using LionStore.Core;
using LionStore.Core.Repositories;
using LionStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LionStore.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LionStoreDbContext _context;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;

        public UnitOfWork(LionStoreDbContext context)
        {
            this._context = context;
        }

        #region Interface Implementation
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
        public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
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

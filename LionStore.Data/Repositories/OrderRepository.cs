using LionStore.Core.Models;
using LionStore.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionStore.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(LionStoreDbContext context) : base(context) { }

        public async Task<Order> GetOrderByIdWithProduct(int orderId)
        {
            return await LionStoreDbContext.Orders
                .Include(m => m.Product)
                .SingleOrDefaultAsync(m => m.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersWithProducts()
        {
            return await LionStoreDbContext.Orders
                .Include(m => m.Product)
                .ToListAsync();
        }

        private LionStoreDbContext LionStoreDbContext
        {
            get { return _context as LionStoreDbContext; }
        }
    }
}

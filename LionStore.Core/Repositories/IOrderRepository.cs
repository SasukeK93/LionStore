using LionStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LionStore.Core.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> GetOrderByIdWithProduct(int id);
        Task<IEnumerable<Order>> GetOrdersWithProducts();
    }
}

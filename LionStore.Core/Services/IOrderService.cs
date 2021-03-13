using LionStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LionStore.Core.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdWithProduct(int id);
        Task<IEnumerable<Order>> GetOrdersWithProducts();
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> CreateOrder(Order newOrder);
        Task UpdateOrder(Order orderToBeUpdated, Order order);
        Task DeleteOrder(Order order);
    }
}
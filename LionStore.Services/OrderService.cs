using LionStore.Core;
using LionStore.Core.Models;
using LionStore.Core.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LionStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrder(Order newOrder)
        {
            EntityEntry<Order> product = await _unitOfWork.Orders.InsertAsync(newOrder);
            // Should i reduce the quantity of the product according to the purchase ?
            await _unitOfWork.CommitAsync();
            return product.Entity;
        }

        public async Task DeleteOrder(Order order)
        {
            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _unitOfWork.Orders.FindByIdAsync(id);
        }

        public async Task<Order> GetOrderByIdWithProduct(int id)
        {
            return await _unitOfWork.Orders.GetOrderByIdWithProduct(id);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _unitOfWork.Orders.GetOrdersWithProducts();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithProducts()
        {
            return await _unitOfWork.Orders.GetOrdersWithProducts();
        }

        public async Task UpdateOrder(Order orderToBeUpdated, Order order)
        {
            orderToBeUpdated.Status = order.Status;

            await _unitOfWork.CommitAsync();
        }
    }
}

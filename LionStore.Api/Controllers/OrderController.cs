using AutoMapper;
using LionStore.Api.Resources;
using LionStore.Api.Resources.Order;
using LionStore.Api.Resources.Product;
using LionStore.Api.Validators;
using LionStore.Core.Models;
using LionStore.Core.Models.Identity;
using LionStore.Core.Services;
using LionStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LionStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderResource>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersWithProducts();
            var orderResources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);

            return Ok(orderResources);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OrderResource>> Put(int id, SaveOrderResource saveOrderResource)
        {
            var validator = new SaveOrderResourceValidator();
            var validationResult = await validator.ValidateAsync(saveOrderResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var orderToBeUpdated = await _orderService.GetOrderById(id);

            if (orderToBeUpdated == null)
            {
                return NotFound();
            }

            var order = _mapper.Map<SaveOrderResource, Order>(saveOrderResource);

            await _orderService.UpdateOrder(orderToBeUpdated, order); // Should update timestamp ?

            var updatedOrder = await _orderService.GetOrderByIdWithProduct(id);

            var updatedOrderResource = _mapper.Map<Order, OrderResource>(updatedOrder);

            return Ok(updatedOrderResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OrderResource>> Delete(int id)
        {
            var orderToBeRemoved = await _orderService.GetOrderByIdWithProduct(id);

            if (orderToBeRemoved == null)
            {
                return NotFound();
            }

            if (orderToBeRemoved.Status == EOrderStatus.Confirmed)
            {
                return Unauthorized();
            }

            await _orderService.DeleteOrder(orderToBeRemoved);

            var removedOrderResource = _mapper.Map<Order, OrderResource>(orderToBeRemoved);

            return Ok(removedOrderResource);
        }
    }    
}

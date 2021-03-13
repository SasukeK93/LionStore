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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IOrderService orderService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProductResource>>> GetAllProducts()
        {
            var products = await _productService.GetProducts();
            var productResources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return Ok(productResources);
        }

        /*
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult<ProductResource>> Get(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var productResource = _mapper.Map<Product, ProductResource>(product);

            return Ok(productResource);
        }
        */

        [HttpPost("Buy")]
        [Authorize]
        public async Task<ActionResult<ProductResource>> BuyProduct([FromBody] BuyProductResource buyProductResource)
        {
            var validator = new BuyProductResourceValidator();
            var validationResult = await validator.ValidateAsync(buyProductResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var productToBuy = await _productService.GetProductById(buyProductResource.ProductId);

            if (productToBuy == null || buyProductResource.Quantity >= productToBuy.Quantity)
            {
                return NotFound();
            }

            var orderToCreate = _mapper.Map<BuyProductResource, Order>(buyProductResource);
            orderToCreate.LionUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            orderToCreate.Timestamp = DateTime.Now;
            orderToCreate.Status = EOrderStatus.Created;

            var newOrder = await _orderService.CreateOrder(orderToCreate); // See OrderService.cs Line 22

            var order = await _orderService.GetOrderByIdWithProduct(newOrder.Id);

            var orderResource = _mapper.Map<Order, OrderResource>(order);

            return Ok(orderResource);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult<ProductResource>> CreateProduct([FromBody] SaveProductResource saveProductResource)
        {
            var validator = new SaveProductResourceValidator();
            var validationResult = await validator.ValidateAsync(saveProductResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var productToCreate = _mapper.Map<SaveProductResource, Product>(saveProductResource);

            productToCreate.LionUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var newProduct = await _productService.CreateProduct(productToCreate);

            var product = await _productService.GetProductById(newProduct.Id);

            var productResource = _mapper.Map<Product, ProductResource>(product);

            return Ok(productResource);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult<ProductResource>> Put(int id, SaveProductResource saveProductResource)
        {
            var validator = new SaveProductResourceValidator();
            var validationResult = await validator.ValidateAsync(saveProductResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var productToBeUpdated = await _productService.GetProductById(id);

            if (productToBeUpdated == null)
            {
                return NotFound();
            }

            if (productToBeUpdated.LionUserId != _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            var product = _mapper.Map<SaveProductResource, Product>(saveProductResource);

            await _productService.UpdateProduct(productToBeUpdated, product);

            var updatedProduct = await _productService.GetProductById(id);

            var updatedProductResource = _mapper.Map<Product, ProductResource>(updatedProduct);

            return Ok(updatedProductResource);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult<ProductResource>> Delete(int id)
        {
            var productToBeRemoved = await _productService.GetProductById(id);

            if (productToBeRemoved == null)
            {
                return NotFound();
            }

            if (productToBeRemoved.LionUserId != _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            await _productService.DeleteProduct(productToBeRemoved); // What to do if there are orders for the product?

            var removedProductResource = _mapper.Map<Product, ProductResource>(productToBeRemoved);

            return Ok(removedProductResource);
        }

        /*
         * var username = HttpContext.User.Identity.Name;
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
         */
    }
}
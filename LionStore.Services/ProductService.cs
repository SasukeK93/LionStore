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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Product> CreateProduct(Product newProduct)
        {
            EntityEntry<Product> product = await _unitOfWork.Products.InsertAsync(newProduct);
            await _unitOfWork.CommitAsync();
            return product.Entity;
        }

        public async Task DeleteProduct(Product product)
        {
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.Products.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _unitOfWork.Products.ListAllAsync();
        }

        public async Task UpdateProduct(Product productToBeUpdated, Product product)
        {
            productToBeUpdated.Name = product.Name;
            productToBeUpdated.Description = product.Description;
            productToBeUpdated.Quantity = product.Quantity;
            productToBeUpdated.Slug = product.Slug;
            productToBeUpdated.Price = product.Price;

            await _unitOfWork.CommitAsync();
        }
    }
}

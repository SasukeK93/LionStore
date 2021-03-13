using LionStore.Core.Models;
using LionStore.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LionStore.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(LionStoreDbContext context) : base(context) { }
    }
}

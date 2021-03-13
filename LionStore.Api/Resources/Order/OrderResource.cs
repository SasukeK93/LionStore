using LionStore.Api.Resources.Product;
using LionStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Resources.Order
{
    public class OrderResource
    {
        public DateTime Timestamp { get; set; }
        public EOrderStatus Status { get; set; }
        public int Quantity { get; set; }
        public ProductResource Product { get; set; }
    }
}

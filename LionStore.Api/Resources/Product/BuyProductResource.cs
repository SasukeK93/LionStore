using LionStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Resources.Product
{
    public class BuyProductResource
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

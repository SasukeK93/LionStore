using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Resources.Product
{
    public class SaveProductResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Slug { get; set; }
        public float Price { get; set; }
    }
}

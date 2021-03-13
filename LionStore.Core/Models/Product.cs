using LionStore.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LionStore.Core.Models
{
    public class Product : BaseEntity
    {
        public LionUser Owner { get; set; }
        public string LionUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Slug { get; set; }
        public float Price { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Product()
        {
            Orders = new Collection<Order>();
        }
    }
}

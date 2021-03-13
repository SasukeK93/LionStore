using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LionStore.Core.Models.Identity
{
    public class LionUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }

        public LionUser()
        {
            Products = new Collection<Product>();
            Orders = new Collection<Order>();
        }
    }
}
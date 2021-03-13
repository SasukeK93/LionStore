using LionStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Resources.Order
{
    public class SaveOrderResource
    {
        //public int OrderId { get; set; }
        public EOrderStatus Status { get; set; }
    }
}

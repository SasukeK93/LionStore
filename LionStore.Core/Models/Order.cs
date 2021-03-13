using LionStore.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LionStore.Core.Models
{
    public enum EOrderStatus
    {
        Created,
        Confirmed,
        Canceled
    }
    public class Order : BaseEntity
    {
        public Product Product { get; set; }
        public LionUser User { get; set; }
        public int ProductId { get; set; }
        public string LionUserId { get; set; }
        public DateTime Timestamp { get; set; }
        public EOrderStatus Status { get; set; }
        public int Quantity { get; set; }
    }
}

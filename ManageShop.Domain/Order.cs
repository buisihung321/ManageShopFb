using System;
using System.Collections.Generic;

namespace ManageShop.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public float Total { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
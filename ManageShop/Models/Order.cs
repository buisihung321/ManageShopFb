﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public float Total { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
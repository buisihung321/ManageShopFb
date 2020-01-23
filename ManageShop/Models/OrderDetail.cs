using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float Amount { get; set; }
        public Order Order { get; set; }
    }
}
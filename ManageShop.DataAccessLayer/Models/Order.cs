using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageShop.DataAccessLayer.Models;

namespace ManageShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public float Total { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models.ViewModel
{
    public class OrderViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Album> Albums { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }

        public OrderDetail OrderDetail { get; set; }
        public Album Album { get; set; }
    }
}
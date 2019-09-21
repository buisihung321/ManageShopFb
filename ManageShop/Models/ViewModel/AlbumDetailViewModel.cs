using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models.ViewModel
{
    public class AlbumDetailViewModel
    {
        public string AlbumId{ get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
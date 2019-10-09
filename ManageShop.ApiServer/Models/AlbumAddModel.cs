using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageShop.DataAccessLayer.Models;

namespace ManageShop.ApiServer.Models
{
    public class AlbumAddModel
    {
        public Album album { get; set; }  
        public IEnumerable<Product> products{ get; set; }  
    }
}
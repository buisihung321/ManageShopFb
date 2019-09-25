using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class Category
    {

        public Category()
        {
            Albums = new List<Album>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
using System.Data.Entity;
using ManageShop.DataAccessLayer.Models;
using ManageShop.Models;

namespace ManageShop.DataAccessLayer.DAL
{
    public class ManageShopContext : DbContext
    {
        
        public DbSet<Album> Albums { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Category> Categories{ get; set; }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }



    }
}
using System.Data.Entity;
using ManageShop.Models;
using ManageShop.Server.Repository.DbModel;

namespace ManageShop.Server.Repository.DAL
{
    public class ManageShopContext : DbContext
    {
        public ManageShopContext() : base("ManageShopContext")
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }



    }
}
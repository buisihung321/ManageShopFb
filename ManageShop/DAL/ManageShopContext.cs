﻿using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManageShop.DAL
{
    public class ManageShopContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
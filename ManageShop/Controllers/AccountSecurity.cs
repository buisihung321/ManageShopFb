using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageShop.DAL;

namespace ManageShop.Controllers
{
    public class AccountSecurity
    {
        public static bool Login(string username, string password)
        {
            using (ManageShopContext _context = new ManageShopContext())
            {
                return _context.Customers.Any(c => c.Username.Equals(username) && c.Password.Equals(password));
            }
        }

    }
}
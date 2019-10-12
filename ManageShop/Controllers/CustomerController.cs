using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using ManageShop.DAL;

namespace ManageShop.Controllers
{
    public class CustomerController : Controller
    {
        private ManageShopContext _context;

        public CustomerController()
        {
            _context = new ManageShopContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (AccountSecurity.Login(username, password))
                return Content("Hello " + username);
            else
                return Content("Invalid username or password");
        }

        [BasicAuthentication]
        public ActionResult GetProduct()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            if(username == "admin")
            {
                return Content("Data for admin account");
            }
            else
            {
                return Content("Data for guest account ");
            }

        }

    }
}
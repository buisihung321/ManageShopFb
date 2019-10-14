using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using ManageShop.DAL;
using ManageShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

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
            if (User.Identity.IsAuthenticated)
                return Content("You has logged in. Hello " + User.Identity.GetUserName());
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register(Customer customer)
        {


            return null;


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
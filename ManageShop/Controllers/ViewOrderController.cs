using ManageShop.DAL;
using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageShop.Controllers
{
    public class ViewOrderController : Controller
    {
        private ManageShopContext _context;
        public ViewOrderController()
        {
            _context = new ManageShopContext();
        }
        // GET: ViewOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList()
        {
            var orderList = _context.Orders.ToList();
            return Json(new { data=orderList}, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Details(int id)
        {
            var DetailsList = _context.OrderDetails.Where(o => o.OrderId == id).ToList();
            return View(DetailsList);
        }

    }
}
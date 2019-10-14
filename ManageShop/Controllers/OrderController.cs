using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ManageShop.DAL;
using ManageShop.Models.ViewModel;
using ManageShop.Models;
using System.ComponentModel;

namespace ManageShop.Controllers
{
    public class OrderController : Controller
    {
        private ManageShopContext _context;
        public OrderController()
        {
            _context = new ManageShopContext();
        }
        // GET: Order
        public ActionResult Create()
        {
            var albums = _context.Albums.ToList();
            var products = _context.Products.ToList();
            var viewModel = new OrderViewModel
            {
                Albums = albums,
                Products = products

            };
            return View("Create", viewModel);
        }
        //Save
       public ActionResult Save(IEnumerable<OrderDetail> orderDetails,Order order)
       {
            //update product quantity
            foreach (var orderDetail in orderDetails)
            {
                var prod = _context.Products.SingleOrDefault(p => p.Id == orderDetail.ProductId);
                if(prod != null)
                {
                    if (prod.Quantity - orderDetail.Quantity < 0)
                        return Content("Khong du quantity");
                    prod.Quantity -= orderDetail.Quantity;
                }
                else
                {
                    //Thong bao khong tim thay product
                }
            }

            foreach(OrderDetail orderDetail in orderDetails)
            {
                order.OrderDetails.Add(orderDetail);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Json(new { newUrl = "/ViewOrder" });
        }

       

    }
}
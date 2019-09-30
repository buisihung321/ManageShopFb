using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageShop.Controllers
{
    public class WebhookController : Controller
    {
        // GET: Webhook
        public ActionResult Index()
        {
            

            using (var context = new  ManageShop.DAL.ManageShopContext())
            {
                var model = context.MessageWebhookLogs.ToList();
                return View(model);

            }
        }
    }
}
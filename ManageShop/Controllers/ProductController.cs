using ManageShop.DAL;
using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ManageShop.Controllers
{
    public class ProductController : Controller
    {
        #region LoadDBContext
        private ManageShopContext _context;

        public ProductController()
        {
            _context = new ManageShopContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public ActionResult Add(Product prod)
        {
            _context.Products.Add(prod);
            _context.SaveChanges();

            return RedirectToAction("Edit", "Album", new { albumId = prod.AlbumId });
        }


        //public ActionResult Update(Product prod)
        //{
        //    var product = _context.Products.SingleOrDefault(p => p.Id == prod.Id);
        //    if (product == null)
        //        return HttpNotFound();

        //    product.Name = prod.Name;
        //    product.Price = prod.Price;
        //    product.Quantity = prod.Quantity;

        //    _context.SaveChanges();

        //    return RedirectToAction("Edit", "Album", new { albumId = product.AlbumId });
        //}

        public HttpStatusCodeResult Update(Product prod)
        {
           
            var product = _context.Products.SingleOrDefault(p => p.Id == prod.Id);
            if (product == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Quantity = prod.Quantity;

            _context.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        public ActionResult Detail(string uuid)
        {
            var product = _context.Products.SingleOrDefault(prod => prod.PhotoUUID == uuid);
            if (product == null)
                return null;

            return Json(product);
        }

        public ActionResult List()
        {
            var products = _context.Products.ToList();


            return View("List",products);
        }

        public ActionResult SearchPaging(string searchBy, string searchValue, int? page)
        {
            
            if (searchBy == "albumID")
            {
               var products = _context.Products.Where(prod => prod.AlbumId.Contains(searchValue)).ToList()
                    .ToPagedList(page ?? 1 , 3);
               return View("SearchPaging", products);

            }
            else if(searchBy == "id")
            {

            }else if (searchBy == "Name")
            {

            }
            else
            {
                return View(_context.Products.ToList().ToPagedList(page ?? 1, 3));
            }

            return null;
        }

        [HttpPost]
        public ActionResult Search(string searchBy,string searchValue)
        {
            var products = _context.Products.SqlQuery($"Select * from Products where {searchBy} Like @id",
                new SqlParameter("@id", $"%{searchValue}%")).ToList();


            return PartialView("_ProductList", products);
        }

        // GET: Photo
        public ActionResult Remove(string uuid)
        {

            //find this product
            var prod = _context.Products.SingleOrDefault(p => p.PhotoUUID == uuid);

            if (prod == null)
                return HttpNotFound();

            string albumId = prod.AlbumId;

            //remove this product in Uploadcare 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.uploadcare.com/files/");

                client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Uploadcare.Simple", "49d4a35ffe4282af4c23:97d3eba176003d877e02");

                //HTTP GET
                var responseTask = client.DeleteAsync(prod.PhotoUUID + "/");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //remove this product in our DB
                    _context.Products.Remove(prod);
                    _context.SaveChanges();
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Cannot delete this UUID");
                    return Content("Error when calling API to UploadCare");
                }
            }
            


            return RedirectToAction("Edit","Album",new { albumId = albumId});
        }
    }
}
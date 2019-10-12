using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.Results;
using ManageShop.DAL;
using ManageShop.Models;
using ManageShop.Models.ViewModel;
using Newtonsoft.Json.Linq;

namespace ManageShop.Controllers
{
    public class AlbumController : Controller
    {
        private const string PageToken = "EAAEk1ybcCooBAPbgQa21ZAZCS1eVZBZBR7jg2Ked0JhLe7y6Mon93RVOoZCLvZB1vS1iC2jfXGc0lWrkNKlzIbqajR9Wfe4uVU8oojZAL8p4WbF4WazUiM2zuXRoZAZAfEZBNASjZBJktRbCqOyP90klsl01b7RZAnAFolrFNdBZCXYZAcMAZDZD";
        private const string PageID = "1659222677486198";
        private const string FBApiBase = "https://graph.facebook.com/v4.0/";

        private ManageShopContext _context;

        public AlbumController()
        {
            _context = new ManageShopContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Album
        public ActionResult Index()
        {

            var albums = _context.Albums.ToList();
            //List all Album
            return View(albums);
        }

        public ActionResult Filter(string categoryName)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var albums = _context.Albums
                .Where(a => a.Caterogies.Contains(categoryName)).ToList();

            return View("Index", albums);
        }


        [HttpPost]
        public ActionResult New(Album album,IEnumerable<Product> products)
        {
            //save the album first
            //check if the alubm name was setted
            if (string.IsNullOrEmpty(album.Name))
                album.Name = "Album was not named";

            
            _context.Albums.Add(album);
            _context.SaveChanges();
            
            //save each product
            foreach(var product in products)
            {
                _context.Products.Add(product);
            }
            _context.SaveChanges();
            return Json(new { newUrl = "/Album" });
            //return RedirectToAction("Index","Album");
        }

        public ActionResult Save(Album album)
        {

            album.CreatedTime = DateTime.Now;
            _context.Albums.Add(album);
            _context.SaveChanges();
            //return Json(new { newUrl = Url.Action("Index", "Album") });
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string albumId)
        {
            var album = _context.Albums.SingleOrDefault(al => al.AlbumId == albumId);
            if (album == null)
                return HttpNotFound();
            //load all photo belong to that album by ID
            var viewModel = new AlbumDetailViewModel
            {
                AlbumId = "" + albumId,
                Products = _context.Products.Include(prod => prod.Album).Where(prod => prod.AlbumId == albumId).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult LoadProduct(string albumId)
        {
           IEnumerable<Product> Products = _context.Products.Include(prod => prod.Album).Where(prod => prod.AlbumId == albumId).ToList();
            return Json(new { product = Products });
        }

        public ActionResult AddPhoto(Product prod)
        {
            _context.Products.Add(prod);
            _context.SaveChanges();

            return RedirectToAction("Edit","Album", new { id = prod.AlbumId });
        }


        public ActionResult Remove(int id)
        {
            var album = _context.Albums.SingleOrDefault(al => al.Id == id);
            if (album == null)
                return HttpNotFound();
            //remvoe album
            _context.Albums.Remove(album);
            _context.SaveChanges();

            //neu dung Web Api o day se tra ve ket qua sau khi goi API 
            //ben client se dung ajax

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> PostToFb(string id)
        {
            //1. Get Album data 
            var album = _context.Albums.SingleOrDefault(al => al.AlbumId == id);
            if (album == null)
                return HttpNotFound();

            //2. Create an Album To FB and get the response AlbumID
            string albumApi = FBApiBase + PageID + "/albums";
            JObject albumData = JObject.FromObject(new
            {
                name = album.Name,
                message = album.Description,
                access_token = PageToken
            });


            var albumResult = await ApiHelper.ApiHelper.CallApi(albumApi, albumData.ToString());
            //validate return 200
            if (!albumResult.IsSuccessStatusCode)
                return Content(albumResult.ReasonPhrase);

            JObject albumJson = JObject.Parse(await albumResult.Content.ReadAsStringAsync());
            var albumFbId = albumJson["id"];

            //3. Get all Products belong to that album
            //Iterate each product of that album and post to AlbumID on FB
            var products = _context.Products.Where(prod => prod.AlbumId == id).ToList();
            var productApi = FBApiBase + albumFbId + "/photos";
            foreach (var product in products)
            {
                var photoUrl = $"https://ucarecdn.com/{product.PhotoUUID}/";
                JObject productData = JObject.FromObject(new
                {
                    url = photoUrl,
                    message = $"{product.Name}" +
                              $"\nID: {product.Id}",
                    access_token = PageToken
                });
                var resultProduct = await ApiHelper.ApiHelper.CallApi(productApi, productData.ToString());
                if (!resultProduct.IsSuccessStatusCode)
                    return Content(resultProduct.ReasonPhrase);
            }

            //4. Update Album data after post successful
            //Add FB Album link to Album Table
            album.HasPosted = true;
            album.FbLink = $"https://facebook.com/{albumFbId}";
            _context.SaveChanges();

            //5. Return to View

            return RedirectToAction("Index","Album");
        }

    }
}
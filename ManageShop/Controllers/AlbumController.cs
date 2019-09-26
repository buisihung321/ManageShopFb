using ManageShop.DAL;
using ManageShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using ManageShop.Models.ViewModel;

namespace ManageShop.Controllers
{
    public class AlbumController : Controller
    {
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

            var albums = _context.Albums.Include(a => a.Categories).ToList();
            //List all Album
            return View(albums);
        }

        public ActionResult Filter(string categoryName)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var albums = _context.Categories
                .Include(c => c.Albums)
                .SingleOrDefault(c => c.Name == categoryName).Albums;


            return View("Index", albums);
        }


        /Order
            Album 1 
                Prod 1 Prod2 

            /Order 
                Serch By Product Name

        [HttpPost]
        public ActionResult New(Album album,IEnumerable<Product> products, IEnumerable<string> categories)
        {
            //save the album first
            //check if the alubm name was setted
            if (album.Name == "" || album.Name == null)
                album.Name = "Album was not named";

            //add catogories to album
            foreach (var category in categories)
            {
                album.Categories.Add(new Category { Name = category });
            }

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

    
    }
}
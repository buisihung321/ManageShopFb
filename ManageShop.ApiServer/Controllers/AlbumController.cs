using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManageShop.ApiServer.Models;
using ManageShop.DataAccessLayer;
using ManageShop.DataAccessLayer.Models;

namespace ManageShop.ApiServer.Controllers
{
    public class AlbumController : ApiController
    {
        AlbumRepository albumRepository = new AlbumRepository();
        ProductRepository productRepository = new ProductRepository();
        public IHttpActionResult Get()
        {
            var albums = albumRepository.GetAll().ToList();
            return Ok(albums);
        }

        [HttpGet]
        public IHttpActionResult Filter(string categoryName)
        {
            var albums = albumRepository.GetAll(a => a.Caterogies.Contains(categoryName)).ToList();
            return Ok(albums);
        }
        [Route("api/Album/New")]
        [HttpPost]
        public IHttpActionResult New([FromBody]AlbumAddModel albumAddModel)
        {
            //save the album first
            //check if the alubm name was setted
            //if (string.IsNullOrEmpty(album.Name))
            //    album.Name = "Album was not named";


            //albumRepository.Insert(album);

            ////save each product
            //foreach (var product in products)
            //{
            //    productRepository.Insert(product);
            //}

            return Ok();
            //return RedirectToAction("Index","Album");
        }

    }
}

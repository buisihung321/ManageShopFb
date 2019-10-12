using System.ComponentModel.DataAnnotations;
using ManageShop.Models;

namespace ManageShop.DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string PhotoUUID { get; set; }

        public string AlbumId { get; set; }
        public Album Album { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }

        public int Quantity { get; set; }
    }
}
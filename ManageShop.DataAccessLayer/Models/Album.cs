using System;

namespace ManageShop.DataAccessLayer.Models
{
    public class Album
    {

        public Album()
        {
            CreatedTime = DateTime.Now;
        }

        public string PhotoCover { get; set; }

        public int Id { get; set; }
        public string AlbumId { get; set; }
        public bool HasPosted { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public string FbLink { get; set; }    

        public DateTime CreatedTime { get; set; }

        //public ICollection<Category> Categories { get; set; }
        public string Caterogies { get; set; }
    }
}
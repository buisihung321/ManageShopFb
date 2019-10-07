using System;
using System.Collections.Generic;

namespace ManageShop.Domain
{
    public class AlbumDomainModel
    {

        public string PhotoCover { get; set; }

        public int Id { get; set; }
        public string AlbumId { get; set; }
        public bool HasPosted { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public string FbLink { get; set; }    

        public DateTime CreatedTime { get; set; }
        public string Categories;

    }
}
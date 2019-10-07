using System.Collections.Generic;

namespace ManageShop.Domain
{
    public class Category
    {

        public Category()
        {
            Albums = new List<AlbumDomainModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AlbumDomainModel> Albums { get; set; }
    }
}
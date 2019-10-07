using System.Collections.Generic;

namespace ManageShop.Server.Repository.DbModel
{
    public class Category
    {

        public Category()
        {
            Albums = new List<Album>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
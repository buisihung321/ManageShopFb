namespace ManageShop.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string PhotoUUID { get; set; }

        public string AlbumId { get; set; }
        public AlbumDomainModel AlbumDomainModel { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }

        public int Quantity { get; set; }
    }
}
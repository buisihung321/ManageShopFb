namespace ManageShop.Domain
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string AlbumId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float Total { get; set; }       
    }
}
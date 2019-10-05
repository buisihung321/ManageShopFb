namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        AlbumId = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Order_OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}

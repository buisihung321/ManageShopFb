namespace ManageShop.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoCover = c.String(),
                        AlbumId = c.String(),
                        HasPosted = c.Boolean(nullable: false),
                        Description = c.String(),
                        Name = c.String(),
                        FbLink = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        Caterogies = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoUUID = c.String(nullable: false),
                        AlbumId = c.String(),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Album_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.OrderDetails", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Album_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderId" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Albums");
        }
    }
}

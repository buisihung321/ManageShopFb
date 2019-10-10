namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mergeraaaa : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.OrderDetails", "Order_OrderId", "dbo.Orders");
            //DropIndex("dbo.OrderDetails", new[] { "Order_OrderId" });
            //RenameColumn(table: "dbo.OrderDetails", name: "Order_OrderId", newName: "OrderId");
            //DropPrimaryKey("dbo.Orders");
            //AddColumn("dbo.OrderDetails", "Amount", c => c.Single(nullable: false));
            //AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.Orders", "Description", c => c.String());
            //AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.Orders", "Id");
            //CreateIndex("dbo.OrderDetails", "OrderId");
            //AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            //DropColumn("dbo.OrderDetails", "AlbumId");
            //DropColumn("dbo.OrderDetails", "Total");
            //DropColumn("dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.OrderDetails", "Total", c => c.Single(nullable: false));
            AddColumn("dbo.OrderDetails", "AlbumId", c => c.String());
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int());
            DropColumn("dbo.Orders", "Description");
            DropColumn("dbo.Orders", "Id");
            DropColumn("dbo.OrderDetails", "Amount");
            AddPrimaryKey("dbo.Orders", "OrderId");
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "Order_OrderId");
            CreateIndex("dbo.OrderDetails", "Order_OrderId");
            AddForeignKey("dbo.OrderDetails", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}

namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeName : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            //DropPrimaryKey("dbo.Orders");
            //AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Orders", "OrderId");
            //AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            //DropColumn("dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "OrderId");
            AddPrimaryKey("dbo.Orders", "Id");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}

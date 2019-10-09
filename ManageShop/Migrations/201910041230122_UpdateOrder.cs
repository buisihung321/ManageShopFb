namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Amount", c => c.Single(nullable: false));
            DropColumn("dbo.OrderDetails", "AlbumId");
            DropColumn("dbo.OrderDetails", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Total", c => c.Single(nullable: false));
            AddColumn("dbo.OrderDetails", "AlbumId", c => c.String());
            DropColumn("dbo.OrderDetails", "Amount");
        }
    }
}

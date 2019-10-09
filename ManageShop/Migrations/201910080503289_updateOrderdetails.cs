namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderdetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProductId");
        }
    }
}

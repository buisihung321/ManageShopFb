namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNameOnOrderDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProductName");
        }
    }
}

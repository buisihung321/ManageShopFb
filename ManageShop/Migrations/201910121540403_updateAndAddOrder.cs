namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAndAddOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Name", c => c.String());
            AddColumn("dbo.Orders", "Address", c => c.String());
            AddColumn("dbo.Orders", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Phone");
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "Name");
        }
    }
}

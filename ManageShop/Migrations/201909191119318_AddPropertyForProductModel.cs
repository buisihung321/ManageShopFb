namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyForProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PhotoUUID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PhotoUUID");
        }
    }
}

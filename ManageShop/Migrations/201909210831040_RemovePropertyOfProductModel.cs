namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePropertyOfProductModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Photo", c => c.Binary());
        }
    }
}

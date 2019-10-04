namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperTyForAlbumModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "FbLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "FbLink");
        }
    }
}

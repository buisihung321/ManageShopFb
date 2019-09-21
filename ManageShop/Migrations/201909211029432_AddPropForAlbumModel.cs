namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropForAlbumModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "PhotoCover", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "PhotoCover");
        }
    }
}

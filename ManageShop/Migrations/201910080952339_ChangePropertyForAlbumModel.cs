namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePropertyForAlbumModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Caterogies", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Caterogies");
        }
    }
}

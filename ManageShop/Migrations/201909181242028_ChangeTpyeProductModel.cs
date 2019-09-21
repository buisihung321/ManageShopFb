namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTpyeProductModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Products", new[] { "AlbumId" });
            AddColumn("dbo.Products", "Album_Id", c => c.Int());
            AlterColumn("dbo.Products", "AlbumId", c => c.String());
            CreateIndex("dbo.Products", "Album_Id");
            AddForeignKey("dbo.Products", "Album_Id", "dbo.Albums", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Products", new[] { "Album_Id" });
            AlterColumn("dbo.Products", "AlbumId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Album_Id");
            CreateIndex("dbo.Products", "AlbumId");
            AddForeignKey("dbo.Products", "AlbumId", "dbo.Albums", "Id", cascadeDelete: true);
        }
    }
}

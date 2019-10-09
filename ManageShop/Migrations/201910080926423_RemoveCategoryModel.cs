namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCategoryModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryAlbums", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryAlbums", "Album_Id", "dbo.Albums");
            DropIndex("dbo.CategoryAlbums", new[] { "Category_Id" });
            DropIndex("dbo.CategoryAlbums", new[] { "Album_Id" });
            DropTable("dbo.Categories");
            DropTable("dbo.MessageWebhookLogs");
            DropTable("dbo.CategoryAlbums");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryAlbums",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Album_Id });
            
            CreateTable(
                "dbo.MessageWebhookLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        MessageText = c.String(),
                        Timestamp = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CategoryAlbums", "Album_Id");
            CreateIndex("dbo.CategoryAlbums", "Category_Id");
            AddForeignKey("dbo.CategoryAlbums", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryAlbums", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}

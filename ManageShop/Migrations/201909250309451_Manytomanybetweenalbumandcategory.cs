namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manytomanybetweenalbumandcategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Albums", new[] { "CategoryId" });
            CreateTable(
                "dbo.CategoryAlbums",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Album_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CategoryAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.CategoryAlbums", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CategoryAlbums", new[] { "Album_Id" });
            DropIndex("dbo.CategoryAlbums", new[] { "Category_Id" });
            DropTable("dbo.CategoryAlbums");
            CreateIndex("dbo.Albums", "CategoryId");
            AddForeignKey("dbo.Albums", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}

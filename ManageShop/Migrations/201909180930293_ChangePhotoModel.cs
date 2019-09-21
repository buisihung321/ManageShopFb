namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePhotoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.Binary(),
                        AlbumId = c.Int(nullable: false),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            DropTable("dbo.PhotoPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PhotoPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Products", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Products", new[] { "AlbumId" });
            DropTable("dbo.Products");
        }
    }
}

namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAlbum : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Albums(AlbumId,CreatedTime,Description,HasPosted,Name) values('A12','12/12/2010','This is Description 1',0,'Album 1') ");
            Sql("insert into Albums(AlbumId,CreatedTime,Description,HasPosted,Name) values('A34','01/01/2009','This is Description 2',0,'Album 2') ");
        }
        
        public override void Down()
        {
        }
    }
}

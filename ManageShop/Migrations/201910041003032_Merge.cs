namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Merge : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.MessageWebhookLogs",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            MessageText = c.String(),
            //            Timestamp = c.String(),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //AddColumn("dbo.Albums", "FbLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "FbLink");
            DropTable("dbo.MessageWebhookLogs");
        }
    }
}

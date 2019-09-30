namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageModelForMessageWebhook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageWebhookLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        MessageText = c.String(),
                        Timestamp = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MessageWebhookLogs");
        }
    }
}

namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeModelMessage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MessageWebhookLogs", "Timestamp", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MessageWebhookLogs", "Timestamp", c => c.Int(nullable: false));
        }
    }
}

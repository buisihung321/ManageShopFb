namespace ManageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Customers(Username,Password) values('user','1')");
            Sql("insert into Customers(Username,Password) values('admin','admin')");
        }
        
        public override void Down()
        {
        }
    }
}

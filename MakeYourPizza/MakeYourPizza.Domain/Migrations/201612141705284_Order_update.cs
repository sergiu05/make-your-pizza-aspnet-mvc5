namespace MakeYourPizza.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PhoneNumber");
            DropColumn("dbo.Orders", "City");
        }
    }
}

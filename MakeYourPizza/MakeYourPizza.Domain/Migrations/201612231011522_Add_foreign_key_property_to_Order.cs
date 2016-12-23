namespace MakeYourPizza.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_foreign_key_property_to_Order : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "User_Id");
        }
    }
}

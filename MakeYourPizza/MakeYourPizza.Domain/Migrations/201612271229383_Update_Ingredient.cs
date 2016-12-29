namespace MakeYourPizza.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Ingredient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredients", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Ingredients", "Price", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingredients", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Ingredients", "Name", c => c.String());
        }
    }
}

namespace EMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewVar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "oldCategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "oldCategoryId");
        }
    }
}

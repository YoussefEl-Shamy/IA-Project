namespace EMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataAnnotationForNewVar : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "oldCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "oldCategoryId", c => c.Int(nullable: false));
        }
    }
}

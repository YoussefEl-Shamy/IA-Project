namespace EMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDatatypeNumOfProd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "number_of_products", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "number_of_products", c => c.String());
        }
    }
}

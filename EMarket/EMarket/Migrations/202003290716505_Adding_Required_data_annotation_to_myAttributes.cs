namespace EMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Required_data_annotation_to_myAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "image", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "description", c => c.String());
            AlterColumn("dbo.Products", "image", c => c.String());
            AlterColumn("dbo.Products", "name", c => c.String());
        }
    }
}

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvailableStockToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "AvailableStock", c => c.Short(nullable: false));
            Sql("UPDATE Movies SET AvailableStock = CurrentStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "AvailableStock");
        }
    }
}

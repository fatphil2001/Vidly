namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameMoviesAvailable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Short(nullable: false));
            Sql("UPDATE Movies SET NumberAvailable = AvailableStock");
            DropColumn("dbo.Movies", "AvailableStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "AvailableStock", c => c.Short(nullable: false));
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}

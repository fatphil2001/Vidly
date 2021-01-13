namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.genre (id, name) VALUES (1,'Comedy') ");
            Sql("INSERT INTO dbo.genre (id, name) VALUES (2,'Drama') ");
            Sql("INSERT INTO dbo.genre (id, name) VALUES (3,'Action') ");
            Sql("INSERT INTO dbo.genre (id, name) VALUES (4,'Horror') ");
            Sql("INSERT INTO dbo.genre (id, name) VALUES (5,'Documentary') ");
        }
        
        public override void Down()
        {
        }
    }
}

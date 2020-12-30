namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreGenreDefinitions : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Childrens') ");
            Sql("INSERT INTO Genres (Name) VALUES ('Family') ");
        }
        
        public override void Down()
        {
        }
    }
}

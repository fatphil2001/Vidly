namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.customer",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                        is_subscribed_to_newsletter = c.Boolean(nullable: false),
                        membership_type_id = c.Short(nullable: false),
                        date_of_birth = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.membership_type", t => t.membership_type_id, cascadeDelete: true)
                .Index(t => t.membership_type_id);
            
            CreateTable(
                "dbo.membership_type",
                c => new
                    {
                        id = c.Short(nullable: false),
                        name = c.String(),
                        sign_up_fee = c.Short(nullable: false),
                        duration_in_months = c.Short(nullable: false),
                        discount_rate = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.genre",
                c => new
                    {
                        id = c.Short(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.movie",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                        genre_id = c.Short(nullable: false),
                        release_date = c.DateTime(nullable: false),
                        added_date = c.DateTime(nullable: false),
                        current_stock = c.Short(nullable: false),
                        number_available = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.genre", t => t.genre_id, cascadeDelete: true)
                .Index(t => t.genre_id);
            
            CreateTable(
                "dbo.rental",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date_rented = c.DateTime(nullable: false),
                        date_returned = c.DateTime(),
                        Customer_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.customer", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.movie", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        user_id = c.String(nullable: false, maxLength: 128),
                        role_id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.user_id, t.role_id })
                .ForeignKey("dbo.AspNetRoles", t => t.role_id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        driving_license = c.String(nullable: false, maxLength: 255),
                        telephone_number = c.String(nullable: false, maxLength: 50),
                        email = c.String(maxLength: 256),
                        email_confirmed = c.Boolean(nullable: false),
                        password_hash = c.String(),
                        security_stamp = c.String(),
                        phone_number = c.String(),
                        phone_number_confirmed = c.Boolean(nullable: false),
                        two_factor_enabled = c.Boolean(nullable: false),
                        lockout_end_date_utc = c.DateTime(),
                        lockout_enabled = c.Boolean(nullable: false),
                        access_failed_count = c.Int(nullable: false),
                        user_name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.user_name, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.String(nullable: false, maxLength: 128),
                        claim_type = c.String(),
                        claim_value = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        login_provider = c.String(nullable: false, maxLength: 128),
                        provider_key = c.String(nullable: false, maxLength: 128),
                        user_id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.login_provider, t.provider_key, t.user_id })
                .ForeignKey("dbo.AspNetUsers", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "role_id", "dbo.AspNetRoles");
            DropForeignKey("dbo.rental", "Movie_Id", "dbo.movie");
            DropForeignKey("dbo.rental", "Customer_Id", "dbo.customer");
            DropForeignKey("dbo.movie", "genre_id", "dbo.genre");
            DropForeignKey("dbo.customer", "membership_type_id", "dbo.membership_type");
            DropIndex("dbo.AspNetUserLogins", new[] { "user_id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "user_id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "role_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "user_id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.rental", new[] { "Movie_Id" });
            DropIndex("dbo.rental", new[] { "Customer_Id" });
            DropIndex("dbo.movie", new[] { "genre_id" });
            DropIndex("dbo.customer", new[] { "membership_type_id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.rental");
            DropTable("dbo.movie");
            DropTable("dbo.genre");
            DropTable("dbo.membership_type");
            DropTable("dbo.customer");
        }
    }
}

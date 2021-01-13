namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO dbo.membership_type (id, sign_up_fee, duration_in_months, discount_rate, name) VALUES (1, 0, 0, 0, 'Pay as You Go')");
            Sql("INSERT INTO dbo.membership_type (id, sign_up_fee, duration_in_months, discount_rate, name) VALUES (2, 30, 1, 10, 'Monthly')");
            Sql("INSERT INTO dbo.membership_type (id, sign_up_fee, duration_in_months, discount_rate, name) VALUES (3, 90, 3, 15, 'Quarterly')");
            Sql("INSERT INTO dbo.membership_type (id, sign_up_fee, duration_in_months, discount_rate, name) VALUES (4, 300, 12, 20, 'Yearly' )");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM dbo.membership_type");

        }
    }
}

namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2928ec68-6108-4786-8c96-8ded3dda4315', N'phil@philgeorge.com', 0, N'AOlExcKVkVUsKVQtACKrMYnIy5N2cQ84piKcmYzQgq2ZGXSvFSzJCZXMx9t2pykl/w==', N'b2f34203-a99b-4646-a80a-c7c75e1cb314', NULL, 0, 0, NULL, 1, 0, N'phil@philgeorge.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'455928d9-ebcd-4d8e-bece-77ae8dda33cb', N'admin@vidley.com', 0, N'AJ2fCBjyihWjcK6vfR5oq8DlF5BdYjOKZb3+KwLOjnCUIYRUwNkwJKwNO/TOqEaZVA==', N'83d76c17-959c-43fe-ba0a-e0917e69377b', NULL, 0, 0, NULL, 1, 0, N'admin@vidley.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'abb1052e-2182-4a57-9210-b9d231dc28b4', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'455928d9-ebcd-4d8e-bece-77ae8dda33cb', N'abb1052e-2182-4a57-9210-b9d231dc28b4')


");
        }
        
        public override void Down()
        {
        }
    }
}

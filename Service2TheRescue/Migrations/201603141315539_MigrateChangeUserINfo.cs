namespace Service2TheRescue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateChangeUserINfo : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(maxLength: 80),
                        FirstName = c.String(maxLength: 40),
                        CompanyName = c.String(maxLength: 80),
                        Department = c.String(maxLength: 80),
                        Title = c.String(maxLength: 80),
                        Street = c.String(),
                        City = c.String(maxLength: 40),
                        State = c.String(maxLength: 80),
                        PostalCode = c.String(maxLength: 20),
                        Country = c.String(maxLength: 80),
                        Email = c.String(),
                        MobilePhone = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        AboutMe = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}

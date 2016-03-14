namespace Service2TheRescue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigrate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        MasterRecordId = c.String(),
                        AccountId = c.String(),
                        LastName = c.String(maxLength: 80),
                        FirstName = c.String(maxLength: 40),
                        Salutation = c.String(),
                        Name = c.String(maxLength: 121),
                        OtherStreet = c.String(),
                        OtherCity = c.String(maxLength: 40),
                        OtherState = c.String(maxLength: 80),
                        OtherPostalCode = c.String(maxLength: 20),
                        OtherCountry = c.String(maxLength: 80),
                        OtherLatitude = c.Double(),
                        OtherLongitude = c.Double(),
                        MailingStreet = c.String(),
                        MailingCity = c.String(maxLength: 40),
                        MailingState = c.String(maxLength: 80),
                        MailingPostalCode = c.String(maxLength: 20),
                        MailingCountry = c.String(maxLength: 80),
                        MailingLatitude = c.Double(),
                        MailingLongitude = c.Double(),
                        Phone = c.String(),
                        Fax = c.String(),
                        MobilePhone = c.String(),
                        HomePhone = c.String(),
                        OtherPhone = c.String(),
                        AssistantPhone = c.String(),
                        ReportsToId = c.String(),
                        Email = c.String(),
                        Title = c.String(maxLength: 128),
                        Department = c.String(maxLength: 80),
                        AssistantName = c.String(maxLength: 40),
                        LeadSource = c.String(),
                        Birthdate = c.DateTimeOffset(precision: 7),
                        Description = c.String(),
                        OwnerId = c.String(),
                        CreatedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedById = c.String(),
                        LastModifiedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        LastModifiedById = c.String(),
                        SystemModstamp = c.DateTimeOffset(nullable: false, precision: 7),
                        LastActivityDate = c.DateTimeOffset(precision: 7),
                        LastCURequestDate = c.DateTimeOffset(precision: 7),
                        LastCUUpdateDate = c.DateTimeOffset(precision: 7),
                        LastViewedDate = c.DateTimeOffset(precision: 7),
                        LastReferencedDate = c.DateTimeOffset(precision: 7),
                        EmailBouncedReason = c.String(maxLength: 255),
                        EmailBouncedDate = c.DateTimeOffset(precision: 7),
                        IsEmailBounced = c.Boolean(nullable: false),
                        PhotoUrl = c.String(),
                        Jigsaw = c.String(maxLength: 20),
                        JigsawContactId = c.String(maxLength: 20),
                        CleanStatus = c.String(),
                        Level__c = c.String(),
                        Languages__c = c.String(maxLength: 100),
                        isAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }
        
        public override void Down()
        {

            
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Contacts");
        }
    }
}

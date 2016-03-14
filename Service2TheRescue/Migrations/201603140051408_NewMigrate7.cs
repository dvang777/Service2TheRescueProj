namespace Service2TheRescue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigrate7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserInfos", "Username");
            DropColumn("dbo.UserInfos", "UserRoleId");
            DropColumn("dbo.UserInfos", "FullPhotoUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfos", "FullPhotoUrl", c => c.String());
            AddColumn("dbo.UserInfos", "UserRoleId", c => c.String());
            AddColumn("dbo.UserInfos", "Username", c => c.String(maxLength: 80));
        }
    }
}

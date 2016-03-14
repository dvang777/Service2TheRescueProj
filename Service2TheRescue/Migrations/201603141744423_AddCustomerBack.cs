namespace Service2TheRescue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerBack : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        CustomerID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            

        }
        
        public override void Down()
        {
            
            DropTable("dbo.Customers");
            }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeLeave_Model_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeLeave",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeUsername = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        LastModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeLeave");
        }
    }
}

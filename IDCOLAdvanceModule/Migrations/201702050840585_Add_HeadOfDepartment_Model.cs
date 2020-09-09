namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_HeadOfDepartment_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HeadOfDepartment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DepartmentId = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HeadOfDepartment");
        }
    }
}

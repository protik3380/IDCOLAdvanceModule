namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeleted_Added_In_EmployeeLeave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeLeave", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeLeave", "IsDeleted");
        }
    }
}

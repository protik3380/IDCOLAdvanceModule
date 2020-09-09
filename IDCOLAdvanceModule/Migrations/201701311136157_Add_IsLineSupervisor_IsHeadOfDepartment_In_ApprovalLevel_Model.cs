namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsLineSupervisor_IsHeadOfDepartment_In_ApprovalLevel_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevel", "IsLineSupervisor", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApprovalLevel", "IsHeadOfDepartment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalLevel", "IsHeadOfDepartment");
            DropColumn("dbo.ApprovalLevel", "IsLineSupervisor");
        }
    }
}

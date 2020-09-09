namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_EmployeeUserName_Type_In_ApprovalLevelMember_Model : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApprovalLevelMember", "EmployeeUserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApprovalLevelMember", "EmployeeUserName", c => c.Long(nullable: false));
        }
    }
}

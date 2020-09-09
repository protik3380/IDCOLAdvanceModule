namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AuditTrail_In_ApprovalLevel_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalLevel", "CreatedBy", c => c.String());
            AddColumn("dbo.ApprovalLevel", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApprovalLevel", "LastModifiedBy", c => c.String());
            AddColumn("dbo.ApprovalLevel", "LastModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalLevel", "LastModifiedOn");
            DropColumn("dbo.ApprovalLevel", "LastModifiedBy");
            DropColumn("dbo.ApprovalLevel", "CreatedOn");
            DropColumn("dbo.ApprovalLevel", "CreatedBy");
        }
    }
}

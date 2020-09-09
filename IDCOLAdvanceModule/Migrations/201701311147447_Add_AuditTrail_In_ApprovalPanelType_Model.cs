namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AuditTrail_In_ApprovalPanelType_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalPanelType", "CreatedBy", c => c.String());
            AddColumn("dbo.ApprovalPanelType", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApprovalPanelType", "LastModifiedBy", c => c.String());
            AddColumn("dbo.ApprovalPanelType", "LastModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalPanelType", "LastModifiedOn");
            DropColumn("dbo.ApprovalPanelType", "LastModifiedBy");
            DropColumn("dbo.ApprovalPanelType", "CreatedOn");
            DropColumn("dbo.ApprovalPanelType", "CreatedBy");
        }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AuditTrail_In_ApprovalPanel_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalPanel", "CreatedBy", c => c.String());
            AddColumn("dbo.ApprovalPanel", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApprovalPanel", "LastModifiedBy", c => c.String());
            AddColumn("dbo.ApprovalPanel", "LastModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalPanel", "LastModifiedOn");
            DropColumn("dbo.ApprovalPanel", "LastModifiedBy");
            DropColumn("dbo.ApprovalPanel", "CreatedOn");
            DropColumn("dbo.ApprovalPanel", "CreatedBy");
        }
    }
}

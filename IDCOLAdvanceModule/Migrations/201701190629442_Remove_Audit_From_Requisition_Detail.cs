namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Audit_From_Requisition_Detail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AdvanceRequisitionDetail", "CreatedBy");
            DropColumn("dbo.AdvanceRequisitionDetail", "CreatedOn");
            DropColumn("dbo.AdvanceRequisitionDetail", "LastModifiedBy");
            DropColumn("dbo.AdvanceRequisitionDetail", "LastModifiedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionDetail", "LastModifiedOn", c => c.DateTime());
            AddColumn("dbo.AdvanceRequisitionDetail", "LastModifiedBy", c => c.String());
            AddColumn("dbo.AdvanceRequisitionDetail", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.AdvanceRequisitionDetail", "CreatedBy", c => c.String());
        }
    }
}

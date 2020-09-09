namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvanceStatusRenameToAdvanceRequisitionStatus : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AdvanceRequisitionHeader", name: "AdvanceStatusId", newName: "AdvanceRequisitionStatusId");
            RenameIndex(table: "dbo.AdvanceRequisitionHeader", name: "IX_AdvanceStatusId", newName: "IX_AdvanceRequisitionStatusId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AdvanceRequisitionHeader", name: "IX_AdvanceRequisitionStatusId", newName: "IX_AdvanceStatusId");
            RenameColumn(table: "dbo.AdvanceRequisitionHeader", name: "AdvanceRequisitionStatusId", newName: "AdvanceStatusId");
        }
    }
}

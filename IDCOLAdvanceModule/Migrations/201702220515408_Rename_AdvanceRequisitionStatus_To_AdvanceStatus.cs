namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_AdvanceRequisitionStatus_To_AdvanceStatus : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AdvanceRequisitionStatus", newName: "AdvanceStatus");
            RenameColumn(table: "dbo.AdvanceRequisitionHeader", name: "AdvanceRequisitionStatusId", newName: "AdvanceStatusId");
            RenameIndex(table: "dbo.AdvanceRequisitionHeader", name: "IX_AdvanceRequisitionStatusId", newName: "IX_AdvanceStatusId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AdvanceRequisitionHeader", name: "IX_AdvanceStatusId", newName: "IX_AdvanceRequisitionStatusId");
            RenameColumn(table: "dbo.AdvanceRequisitionHeader", name: "AdvanceStatusId", newName: "AdvanceRequisitionStatusId");
            RenameTable(name: "dbo.AdvanceStatus", newName: "AdvanceRequisitionStatus");
        }
    }
}

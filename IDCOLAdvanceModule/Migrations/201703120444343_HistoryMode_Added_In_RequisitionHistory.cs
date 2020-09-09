namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryMode_Added_In_RequisitionHistory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RequisitionDetailHistory", newName: "RequisitionHistoryDetail");
            RenameColumn(table: "dbo.RequisitionHistoryDetail", name: "RequisitionHeaderHistoryId", newName: "RequisitionHistoryHeaderId");
            RenameIndex(table: "dbo.RequisitionHistoryDetail", name: "IX_RequisitionHeaderHistoryId", newName: "IX_RequisitionHistoryHeaderId");
            AddColumn("dbo.AdvanceExpenseDetail", "OverseasSponsorFinancedDetailAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.RequisitionHistoryDetail", "HistoryModeId", c => c.Long(nullable: false));
            AddColumn("dbo.RequisitionHistoryHeader", "HistoryModeId", c => c.Long(nullable: false));
            CreateIndex("dbo.RequisitionHistoryHeader", "HistoryModeId");
            CreateIndex("dbo.RequisitionHistoryDetail", "HistoryModeId");
            AddForeignKey("dbo.RequisitionHistoryHeader", "HistoryModeId", "dbo.HistoryMode", "Id");
            AddForeignKey("dbo.RequisitionHistoryDetail", "HistoryModeId", "dbo.HistoryMode", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionHistoryDetail", "HistoryModeId", "dbo.HistoryMode");
            DropForeignKey("dbo.RequisitionHistoryHeader", "HistoryModeId", "dbo.HistoryMode");
            DropIndex("dbo.RequisitionHistoryDetail", new[] { "HistoryModeId" });
            DropIndex("dbo.RequisitionHistoryHeader", new[] { "HistoryModeId" });
            DropColumn("dbo.RequisitionHistoryHeader", "HistoryModeId");
            DropColumn("dbo.RequisitionHistoryDetail", "HistoryModeId");
            DropColumn("dbo.AdvanceExpenseDetail", "OverseasSponsorFinancedDetailAmount");
            RenameIndex(table: "dbo.RequisitionHistoryDetail", name: "IX_RequisitionHistoryHeaderId", newName: "IX_RequisitionHeaderHistoryId");
            RenameColumn(table: "dbo.RequisitionHistoryDetail", name: "RequisitionHistoryHeaderId", newName: "RequisitionHeaderHistoryId");
            RenameTable(name: "dbo.RequisitionHistoryDetail", newName: "RequisitionDetailHistory");
        }
    }
}

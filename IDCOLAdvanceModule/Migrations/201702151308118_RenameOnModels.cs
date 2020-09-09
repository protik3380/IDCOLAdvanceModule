namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameOnModels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BaseAdvanceRequisitionCategory", newName: "BaseAdvanceCategory");
            RenameTable(name: "dbo.AdvanceRequisitionCategory", newName: "AdvanceCategory");
            RenameTable(name: "dbo.RequisitionCategoryWiseCostItemSetting", newName: "AdvanceCategoryWiseCostItemSetting");
            DropForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalLevelId", "dbo.ApprovalLevel");
            DropForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalPanelId", "dbo.ApprovalPanel");
            DropForeignKey("dbo.ApprovalLevelForApprovalPanel", "ParentApprovalLevelId", "dbo.ApprovalLevel");
            DropIndex("dbo.ApprovalLevelForApprovalPanel", new[] { "ApprovalPanelId" });
            DropIndex("dbo.ApprovalLevelForApprovalPanel", new[] { "ApprovalLevelId" });
            DropIndex("dbo.ApprovalLevelForApprovalPanel", new[] { "ParentApprovalLevelId" });
            RenameColumn(table: "dbo.AdvanceCategory", name: "ApprovalPanelId", newName: "RequisitionApprovalPanelId");
            RenameColumn(table: "dbo.AdvanceCategory", name: "BaseAdvanceRequisitionCategoryId", newName: "BaseAdvanceCategoryId");
            RenameColumn(table: "dbo.AdvanceRequisitionHeader", name: "AdvanceRequisitionCategoryId", newName: "AdvanceCategoryId");
            RenameColumn(table: "dbo.AdvanceCategoryWiseCostItemSetting", name: "AdvanceRequisitionCategoryId", newName: "AdvanceCategoryId");
            RenameColumn(table: "dbo.EntitlementMappingSettingHeader", name: "AdvanceRequisitionCategoryId", newName: "AdvanceCategoryId");
            RenameIndex(table: "dbo.AdvanceCategory", name: "IX_BaseAdvanceRequisitionCategoryId", newName: "IX_BaseAdvanceCategoryId");
            RenameIndex(table: "dbo.AdvanceCategory", name: "IX_ApprovalPanelId", newName: "IX_RequisitionApprovalPanelId");
            RenameIndex(table: "dbo.AdvanceRequisitionHeader", name: "IX_AdvanceRequisitionCategoryId", newName: "IX_AdvanceCategoryId");
            RenameIndex(table: "dbo.AdvanceCategoryWiseCostItemSetting", name: "IX_AdvanceRequisitionCategoryId", newName: "IX_AdvanceCategoryId");
            RenameIndex(table: "dbo.EntitlementMappingSettingHeader", name: "IX_AdvanceRequisitionCategoryId", newName: "IX_AdvanceCategoryId");
            AddColumn("dbo.AdvanceCategory", "ExpenseApprovalPanelId", c => c.Long());
            CreateIndex("dbo.AdvanceCategory", "ExpenseApprovalPanelId");
            AddForeignKey("dbo.AdvanceCategory", "ExpenseApprovalPanelId", "dbo.ApprovalPanel", "Id");
            DropTable("dbo.ApprovalLevelForApprovalPanel");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApprovalLevelForApprovalPanel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApprovalPanelId = c.Long(nullable: false),
                        ApprovalLevelId = c.Long(nullable: false),
                        ParentApprovalLevelId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.AdvanceCategory", "ExpenseApprovalPanelId", "dbo.ApprovalPanel");
            DropIndex("dbo.AdvanceCategory", new[] { "ExpenseApprovalPanelId" });
            DropColumn("dbo.AdvanceCategory", "ExpenseApprovalPanelId");
            RenameIndex(table: "dbo.EntitlementMappingSettingHeader", name: "IX_AdvanceCategoryId", newName: "IX_AdvanceRequisitionCategoryId");
            RenameIndex(table: "dbo.AdvanceCategoryWiseCostItemSetting", name: "IX_AdvanceCategoryId", newName: "IX_AdvanceRequisitionCategoryId");
            RenameIndex(table: "dbo.AdvanceRequisitionHeader", name: "IX_AdvanceCategoryId", newName: "IX_AdvanceRequisitionCategoryId");
            RenameIndex(table: "dbo.AdvanceCategory", name: "IX_RequisitionApprovalPanelId", newName: "IX_ApprovalPanelId");
            RenameIndex(table: "dbo.AdvanceCategory", name: "IX_BaseAdvanceCategoryId", newName: "IX_BaseAdvanceRequisitionCategoryId");
            RenameColumn(table: "dbo.EntitlementMappingSettingHeader", name: "AdvanceCategoryId", newName: "AdvanceRequisitionCategoryId");
            RenameColumn(table: "dbo.AdvanceCategoryWiseCostItemSetting", name: "AdvanceCategoryId", newName: "AdvanceRequisitionCategoryId");
            RenameColumn(table: "dbo.AdvanceRequisitionHeader", name: "AdvanceCategoryId", newName: "AdvanceRequisitionCategoryId");
            RenameColumn(table: "dbo.AdvanceCategory", name: "BaseAdvanceCategoryId", newName: "BaseAdvanceRequisitionCategoryId");
            RenameColumn(table: "dbo.AdvanceCategory", name: "RequisitionApprovalPanelId", newName: "ApprovalPanelId");
            CreateIndex("dbo.ApprovalLevelForApprovalPanel", "ParentApprovalLevelId");
            CreateIndex("dbo.ApprovalLevelForApprovalPanel", "ApprovalLevelId");
            CreateIndex("dbo.ApprovalLevelForApprovalPanel", "ApprovalPanelId");
            AddForeignKey("dbo.ApprovalLevelForApprovalPanel", "ParentApprovalLevelId", "dbo.ApprovalLevel", "Id");
            AddForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalPanelId", "dbo.ApprovalPanel", "Id");
            AddForeignKey("dbo.ApprovalLevelForApprovalPanel", "ApprovalLevelId", "dbo.ApprovalLevel", "Id");
            RenameTable(name: "dbo.AdvanceCategoryWiseCostItemSetting", newName: "RequisitionCategoryWiseCostItemSetting");
            RenameTable(name: "dbo.AdvanceCategory", newName: "AdvanceRequisitionCategory");
            RenameTable(name: "dbo.BaseAdvanceCategory", newName: "BaseAdvanceRequisitionCategory");
        }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApprovalRequisitionTrackerAndTicet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", "dbo.BaseAdvanceRequisitionCategory");
            DropForeignKey("dbo.AdvanceRequisitionDetail", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionStatusId", "dbo.AdvanceRequisitionStatus");
            DropForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.EmployeeCategorySetting", "EmployeeCategoryId", "dbo.EmployeeCategory");
            DropForeignKey("dbo.EntitlementMappingSettingDetail", "EntitlementMappingSettingHeaderId", "dbo.EntitlementMappingSettingHeader");
            DropForeignKey("dbo.EntitlementMappingSettingHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.EntitlementMappingSettingHeader", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.Entitlement", "CostItemId", "dbo.CostItem");
            CreateTable(
                "dbo.ApprovalStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequisitionApprovalTicket",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AdvanceRequisitionHeaderId = c.Long(nullable: false),
                        EmployeeUserName = c.String(),
                        ApprovalStatusId = c.Int(nullable: false),
                        ApprovalPanelId = c.Int(nullable: false),
                        ApprovalLevelId = c.Int(nullable: false),
                        Remarks = c.String(),
                        ApprovedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdvanceRequisitionHeader", t => t.AdvanceRequisitionHeaderId)
                .ForeignKey("dbo.ApprovalStatus", t => t.ApprovalStatusId)
                .Index(t => t.AdvanceRequisitionHeaderId)
                .Index(t => t.ApprovalStatusId);
            
            CreateTable(
                "dbo.RequisitionApprovalTracker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequisitionApprovalTicketId = c.Long(nullable: false),
                        ApprovalStatusId = c.Int(nullable: false),
                        ApprovalPanelId = c.Int(nullable: false),
                        ApprovalLevelId = c.Int(nullable: false),
                        Remarks = c.String(),
                        ApprovedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApprovalStatus", t => t.ApprovalStatusId)
                .ForeignKey("dbo.RequisitionApprovalTicket", t => t.RequisitionApprovalTicketId)
                .Index(t => t.RequisitionApprovalTicketId)
                .Index(t => t.ApprovalStatusId);
            
            AddForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", "dbo.BaseAdvanceRequisitionCategory", "Id");
            AddForeignKey("dbo.AdvanceRequisitionDetail", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader", "Id");
            AddForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory", "Id");
            AddForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionStatusId", "dbo.AdvanceRequisitionStatus", "Id");
            AddForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "CostItemId", "dbo.CostItem", "Id");
            AddForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory", "Id");
            AddForeignKey("dbo.EmployeeCategorySetting", "EmployeeCategoryId", "dbo.EmployeeCategory", "Id");
            AddForeignKey("dbo.EntitlementMappingSettingDetail", "EntitlementMappingSettingHeaderId", "dbo.EntitlementMappingSettingHeader", "Id");
            AddForeignKey("dbo.EntitlementMappingSettingHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory", "Id");
            AddForeignKey("dbo.EntitlementMappingSettingHeader", "CostItemId", "dbo.CostItem", "Id");
            AddForeignKey("dbo.Entitlement", "CostItemId", "dbo.CostItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entitlement", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.EntitlementMappingSettingHeader", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.EntitlementMappingSettingHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.EntitlementMappingSettingDetail", "EntitlementMappingSettingHeaderId", "dbo.EntitlementMappingSettingHeader");
            DropForeignKey("dbo.EmployeeCategorySetting", "EmployeeCategoryId", "dbo.EmployeeCategory");
            DropForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "CostItemId", "dbo.CostItem");
            DropForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionStatusId", "dbo.AdvanceRequisitionStatus");
            DropForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory");
            DropForeignKey("dbo.AdvanceRequisitionDetail", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", "dbo.BaseAdvanceRequisitionCategory");
            DropForeignKey("dbo.RequisitionApprovalTracker", "RequisitionApprovalTicketId", "dbo.RequisitionApprovalTicket");
            DropForeignKey("dbo.RequisitionApprovalTracker", "ApprovalStatusId", "dbo.ApprovalStatus");
            DropForeignKey("dbo.RequisitionApprovalTicket", "ApprovalStatusId", "dbo.ApprovalStatus");
            DropForeignKey("dbo.RequisitionApprovalTicket", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader");
            DropIndex("dbo.RequisitionApprovalTracker", new[] { "ApprovalStatusId" });
            DropIndex("dbo.RequisitionApprovalTracker", new[] { "RequisitionApprovalTicketId" });
            DropIndex("dbo.RequisitionApprovalTicket", new[] { "ApprovalStatusId" });
            DropIndex("dbo.RequisitionApprovalTicket", new[] { "AdvanceRequisitionHeaderId" });
            DropTable("dbo.RequisitionApprovalTracker");
            DropTable("dbo.RequisitionApprovalTicket");
            DropTable("dbo.ApprovalStatus");
            AddForeignKey("dbo.Entitlement", "CostItemId", "dbo.CostItem", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EntitlementMappingSettingHeader", "CostItemId", "dbo.CostItem", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EntitlementMappingSettingHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EntitlementMappingSettingDetail", "EntitlementMappingSettingHeaderId", "dbo.EntitlementMappingSettingHeader", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeCategorySetting", "EmployeeCategoryId", "dbo.EmployeeCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequisitionCategoryWiseCostItemSetting", "CostItemId", "dbo.CostItem", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionStatusId", "dbo.AdvanceRequisitionStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdvanceRequisitionHeader", "AdvanceRequisitionCategoryId", "dbo.AdvanceRequisitionCategory", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdvanceRequisitionDetail", "AdvanceRequisitionHeaderId", "dbo.AdvanceRequisitionHeader", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdvanceRequisitionCategory", "BaseAdvanceRequisitionCategoryId", "dbo.BaseAdvanceRequisitionCategory", "Id", cascadeDelete: true);
        }
    }
}

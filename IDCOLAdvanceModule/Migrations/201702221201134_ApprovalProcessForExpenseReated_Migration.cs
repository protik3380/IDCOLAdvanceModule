namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApprovalProcessForExpenseReated_Migration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ExecutiveTravellingAllowance", newName: "ExecutiveOverseasTravellingAllowance");
            DropIndex("dbo.ApprovalTicket", new[] { "AdvanceRequisitionHeaderId" });
            DropIndex("dbo.ExecutiveOverseasTravellingAllowance", new[] { "CostItem_Id" });
            DropIndex("dbo.ExecutiveOverseasTravellingAllowance", new[] { "EmployeeCategory_Id" });
            DropIndex("dbo.ExecutiveOverseasTravellingAllowance", new[] { "LocationGroup_Id" });
            DropColumn("dbo.ExecutiveOverseasTravellingAllowance", "CostItemId");
            DropColumn("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategoryId");
            DropColumn("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroupId");
            RenameColumn(table: "dbo.ExecutiveOverseasTravellingAllowance", name: "CostItem_Id", newName: "CostItemId");
            RenameColumn(table: "dbo.ExecutiveOverseasTravellingAllowance", name: "EmployeeCategory_Id", newName: "EmployeeCategoryId");
            RenameColumn(table: "dbo.ExecutiveOverseasTravellingAllowance", name: "LocationGroup_Id", newName: "LocationGroupId");
            RenameColumn(table: "dbo.ApprovalTicket", name: "AdvanceExpenseHeader_Id", newName: "AdvanceExpenseHeaderId");
            RenameIndex(table: "dbo.ApprovalTicket", name: "IX_AdvanceExpenseHeader_Id", newName: "IX_AdvanceExpenseHeaderId");
            DropPrimaryKey("dbo.ExecutiveOverseasTravellingAllowance");
            AddColumn("dbo.ApprovalTicket", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ApprovalTicket", "AdvanceRequisitionHeaderId", c => c.Long());
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroupId", c => c.Long(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategoryId", c => c.Long(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "CostItemId", c => c.Long(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "CostItemId", c => c.Long(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategoryId", c => c.Long(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroupId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.ExecutiveOverseasTravellingAllowance", "Id");
            CreateIndex("dbo.ApprovalTicket", "AdvanceRequisitionHeaderId");
            CreateIndex("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroupId");
            CreateIndex("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategoryId");
            CreateIndex("dbo.ExecutiveOverseasTravellingAllowance", "CostItemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExecutiveOverseasTravellingAllowance", new[] { "CostItemId" });
            DropIndex("dbo.ExecutiveOverseasTravellingAllowance", new[] { "EmployeeCategoryId" });
            DropIndex("dbo.ExecutiveOverseasTravellingAllowance", new[] { "LocationGroupId" });
            DropIndex("dbo.ApprovalTicket", new[] { "AdvanceRequisitionHeaderId" });
            DropPrimaryKey("dbo.ExecutiveOverseasTravellingAllowance");
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroupId", c => c.Long());
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategoryId", c => c.Long());
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "CostItemId", c => c.Long());
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "CostItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroupId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExecutiveOverseasTravellingAllowance", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ApprovalTicket", "AdvanceRequisitionHeaderId", c => c.Long(nullable: false));
            DropColumn("dbo.ApprovalTicket", "Discriminator");
            AddPrimaryKey("dbo.ExecutiveOverseasTravellingAllowance", "Id");
            RenameIndex(table: "dbo.ApprovalTicket", name: "IX_AdvanceExpenseHeaderId", newName: "IX_AdvanceExpenseHeader_Id");
            RenameColumn(table: "dbo.ApprovalTicket", name: "AdvanceExpenseHeaderId", newName: "AdvanceExpenseHeader_Id");
            RenameColumn(table: "dbo.ExecutiveOverseasTravellingAllowance", name: "LocationGroupId", newName: "LocationGroup_Id");
            RenameColumn(table: "dbo.ExecutiveOverseasTravellingAllowance", name: "EmployeeCategoryId", newName: "EmployeeCategory_Id");
            RenameColumn(table: "dbo.ExecutiveOverseasTravellingAllowance", name: "CostItemId", newName: "CostItem_Id");
            AddColumn("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroupId", c => c.Int(nullable: false));
            AddColumn("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.ExecutiveOverseasTravellingAllowance", "CostItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExecutiveOverseasTravellingAllowance", "LocationGroup_Id");
            CreateIndex("dbo.ExecutiveOverseasTravellingAllowance", "EmployeeCategory_Id");
            CreateIndex("dbo.ExecutiveOverseasTravellingAllowance", "CostItem_Id");
            CreateIndex("dbo.ApprovalTicket", "AdvanceRequisitionHeaderId");
            RenameTable(name: "dbo.ExecutiveOverseasTravellingAllowance", newName: "ExecutiveTravellingAllowance");
        }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnAdded_AdvanceTravelExpenseDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdvanceExpenseDetail", "TravelCostItemId", c => c.Long());
            CreateIndex("dbo.AdvanceExpenseDetail", "TravelCostItemId");
            AddForeignKey("dbo.AdvanceExpenseDetail", "TravelCostItemId", "dbo.CostItem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdvanceExpenseDetail", "TravelCostItemId", "dbo.CostItem");
            DropIndex("dbo.AdvanceExpenseDetail", new[] { "TravelCostItemId" });
            DropColumn("dbo.AdvanceExpenseDetail", "TravelCostItemId");
        }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverseasTravelWiseCostItemSetting_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OverseasTravelWiseCostItemSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OverseasTravelGroupId = c.Long(nullable: false),
                        CostItemId = c.Long(nullable: false),
                        Currency = c.String(),
                        EntitlementAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostItem", t => t.CostItemId)
                .ForeignKey("dbo.OverseasTravelGroup", t => t.OverseasTravelGroupId)
                .Index(t => t.OverseasTravelGroupId)
                .Index(t => t.CostItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OverseasTravelWiseCostItemSetting", "OverseasTravelGroupId", "dbo.OverseasTravelGroup");
            DropForeignKey("dbo.OverseasTravelWiseCostItemSetting", "CostItemId", "dbo.CostItem");
            DropIndex("dbo.OverseasTravelWiseCostItemSetting", new[] { "CostItemId" });
            DropIndex("dbo.OverseasTravelWiseCostItemSetting", new[] { "OverseasTravelGroupId" });
            DropTable("dbo.OverseasTravelWiseCostItemSetting");
        }
    }
}

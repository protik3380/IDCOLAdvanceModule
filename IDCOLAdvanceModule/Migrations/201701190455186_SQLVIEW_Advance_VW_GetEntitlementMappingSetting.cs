namespace IDCOLAdvanceModule.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SQLVIEW_Advance_VW_GetEntitlementMappingSetting : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Advance_VW_GetEntitlementMappingSetting
                AS
                SELECT NEWID() as Id,header.Id as HeaderId,detail.Id as DetailId,
                category.Name as CategoryName,
                category.Id as CategoryId,
                cost.Id as CostItemId,cost.Name as CostItemName,
                rank.RankID as RankId,
                rank.RankName,detail.EntitlementAmount,
                categoryCostSetting.IsEntitlementMandatory FROM IDCOLAdvanceDB.dbo.EntitlementMappingSettingDetail detail
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.EntitlementMappingSettingHeader header
                ON detail.EntitlementMappingSettingHeaderId = header.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank ON detail.RankID = rank.RankID
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionCategory category
                ON header.AdvanceRequisitionCategoryId = category.Id
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.CostItem cost ON header.CostItemId = cost.Id
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.RequisitionCategoryWiseCostItemSetting categoryCostSetting
                ON header.AdvanceRequisitionCategoryId = categoryCostSetting.AdvanceRequisitionCategoryId AND
                header.CostItemId = categoryCostSetting.CostItemId");

        }

        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetEntitlementMappingSetting')
                    DROP VIEW Advance_VW_GetEntitlementMappingSetting");
        }
    }
}

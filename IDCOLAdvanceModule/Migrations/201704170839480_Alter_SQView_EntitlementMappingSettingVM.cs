namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_SQView_EntitlementMappingSettingVM : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetEntitlementMappingSetting]
                AS
                SELECT NEWID() as Id,header.Id as HeaderId,detail.Id as DetailId,
                category.Name as CategoryName,
                category.Id as CategoryId,
                cost.Id as CostItemId,cost.Name as CostItemName,
                rank.RankID as RankId,
                rank.RankName,detail.EntitlementAmount,
				detail.IsFullAmountEntitlement,
                categoryCostSetting.IsEntitlementMandatory FROM IDCOLAdvanceDB.dbo.EntitlementMappingSettingDetail detail
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.EntitlementMappingSettingHeader header
                ON detail.EntitlementMappingSettingHeaderId = header.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank ON detail.RankID = rank.RankID
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceCategory category
                ON header.AdvanceCategoryId = category.Id
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.CostItem cost ON header.CostItemId = cost.Id
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceCategoryWiseCostItemSetting categoryCostSetting
                ON header.AdvanceCategoryId = categoryCostSetting.AdvanceCategoryId AND
                header.CostItemId = categoryCostSetting.CostItemId");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetEntitlementMappingSetting]
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
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceCategory category
                ON header.AdvanceCategoryId = category.Id
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.CostItem cost ON header.CostItemId = cost.Id
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceCategoryWiseCostItemSetting categoryCostSetting
                ON header.AdvanceCategoryId = categoryCostSetting.AdvanceCategoryId AND
                header.CostItemId = categoryCostSetting.CostItemId");
        }
    }
}

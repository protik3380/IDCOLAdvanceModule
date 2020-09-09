namespace IDCOLAdvanceModule.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SQLVIEW_Advance_VW_GetDesignationWithEmployeeCategory : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetDesignationWithEmployeeCategory]
                AS
                SELECT NEWID() as Id,rank.RankID,rank.RankName,
                category.Id as EmployeeCategoryId,category.Name as
                EmployeeCategoryName,settings.Id as EmployeeCategorySettingsId
                ,rank.IsActive FROM IDCOLMIS.dbo.Admin_Rank rank
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.EmployeeCategorySetting settings
                ON rank.RankID = settings.AdminRankId
                LEFT OUTER JOIN IDCOLAdvanceDB.dbo.EmployeeCategory category
                ON settings.EmployeeCategoryId = category.Id");

        }

        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetDesignationWithEmployeeCategory')
                    DROP VIEW Advance_VW_GetDesignationWithEmployeeCategory");
        }
    }
}

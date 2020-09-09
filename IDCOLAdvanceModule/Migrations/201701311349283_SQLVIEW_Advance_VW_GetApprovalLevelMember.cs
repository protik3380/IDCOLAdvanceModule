namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SQLVIEW_Advance_VW_GetApprovalLevelMember : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Advance_VW_GetApprovalLevelMember
                    AS
                    SELECT NEWID() AS Id, member.Id AS ApprovalLevelMemberId, member.ApprovalLevelId, u.EmployeeID, (u.FirstName+' '+u.MiddleName+' '+u.LastName) AS EmployeeFullName,
                    u.UserName AS EmployeeUserName, u.RankID, rank.RankName FROM IDCOLAdvanceDB.dbo.ApprovalLevelMember member
                    LEFT OUTER JOIN ApprovalLevel level ON member.ApprovalLevelId = level.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON member.EmployeeUserName  = u.UserName 
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank ON u.RankID = rank.RankID");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetApprovalLevelMember')
                    DROP VIEW Advance_VW_GetApprovalLevelMember");
        }
    }
}

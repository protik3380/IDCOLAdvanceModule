namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_Advance_VW_GetLevelWiseMember_IsDeleted_Added : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetApprovalLevelMember]
                    AS
                    SELECT NEWID() AS Id, 
					member.Id AS ApprovalLevelMemberId, 
					member.ApprovalLevelId, 
					member.IsDeleted,
					u.EmployeeID, 
					(u.FirstName+' '+u.MiddleName+' '+u.LastName) AS EmployeeFullName,
                    u.UserName AS EmployeeUserName, 
					u.RankID, 
					rank.RankName 
					
					FROM IDCOLAdvanceDB.dbo.ApprovalLevelMember member
                    LEFT OUTER JOIN ApprovalLevel level ON member.ApprovalLevelId = level.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON member.EmployeeUserName  = u.UserName 
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank ON u.RankID = rank.RankID
                GO");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetApprovalLevelMember]
                    AS
                    SELECT NEWID() AS Id, member.Id AS ApprovalLevelMemberId, member.ApprovalLevelId, u.EmployeeID, (u.FirstName+' '+u.MiddleName+' '+u.LastName) AS EmployeeFullName,
                    u.UserName AS EmployeeUserName, u.RankID, rank.RankName FROM IDCOLAdvanceDB.dbo.ApprovalLevelMember member
                    LEFT OUTER JOIN ApprovalLevel level ON member.ApprovalLevelId = level.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON member.EmployeeUserName  = u.UserName 
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank ON u.RankID = rank.RankID
                GO");
        }
    }
}

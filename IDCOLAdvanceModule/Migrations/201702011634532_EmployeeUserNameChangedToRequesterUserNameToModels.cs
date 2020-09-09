namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeUserNameChangedToRequesterUserNameToModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionApprovalTicket", "RequesterUserName", c => c.String());
            AddColumn("dbo.AdvanceRequisitionHeader", "RequesterUserName", c => c.String());
            DropColumn("dbo.RequisitionApprovalTicket", "EmployeeUserName");
            DropColumn("dbo.AdvanceRequisitionHeader", "EmployeeUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvanceRequisitionHeader", "EmployeeUserName", c => c.String());
            AddColumn("dbo.RequisitionApprovalTicket", "EmployeeUserName", c => c.String());
            DropColumn("dbo.AdvanceRequisitionHeader", "RequesterUserName");
            DropColumn("dbo.RequisitionApprovalTicket", "RequesterUserName");

            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAdvanceRequisition]
                    AS
                    SELECT NEWID() as Id,
                    u.UserName as EmployeeUserName,
                    (u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName,
                    header.Id HeaderId, 
                    header.AdvanceRequisitionCategoryId, 
                    category.Name RequisitionCategoryName, 
                    category.IsCeilingApplicable,
                    category.CeilingAmount,
                    header.FromDate,
                    header.ToDate,
                    header.Currency,
                    header.ConversionRate,
                    header.NoOfDays,
                    header.Purpose,
                    header.RequisitionDate,
                    header.IsSponsorFinanced,
                    header.SponsorName, 
                    header.AdvanceRequisitionStatusId,
                    status.Name as RequisitionStatusName,
                    header.CreatedBy,
                    header.CreatedOn,
                    header.ApprovedBy,
                    header.ApprovedOn,  
                    header.IsDeleted,  
                    header.RecommendedBy,
                    header.RecommendedOn,   
                    header.PlaceOfEvent,
                    header.PlaceOfVisit, 
                    header.SourceOfFund,
                    dept.DepartmentID EmployeeDepartmentID,
                    dept.DepartmentName EmployeeDepartmentName,
                    rank.RankID EmployeeRankID,
                    rank.RankName EmployeeRankName,
                    ISNULL(SUM(detail.AdvanceAmount),0) as AdvanceAmount
                    FROM IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader header 
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionDetail detail
                    ON detail.AdvanceRequisitionHeaderId = header.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionCategory category
                    ON header.AdvanceRequisitionCategoryId = category.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionStatus status
                    ON header.AdvanceRequisitionStatusId = status.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.BaseAdvanceRequisitionCategory base
                    ON category.BaseAdvanceRequisitionCategoryId = base.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.CostItem cost
                    ON detail.TravelCostItemId = cost.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u
                    ON header.EmployeeUserName = u.UserName
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept
                    ON u.DepartmentID = dept.DepartmentID
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank
                    ON u.RankID = rank.RankID
                
                    Group By 
                    header.Id,
                    u.UserName,
                    u.FirstName,
                    u.MiddleName,
                    u.LastName,
                    header.AdvanceRequisitionCategoryId,
                    category.Name, 
                    category.IsCeilingApplicable,
                    category.CeilingAmount,
                    header.FromDate,
                    header.ToDate,
                    header.Currency,
                    header.ConversionRate,
                    header.NoOfDays,
                    header.Purpose,
                    header.RequisitionDate,
                     header.IsSponsorFinanced,
                    header.SponsorName, 
                    header.AdvanceRequisitionStatusId,
                    status.Name,
                    header.CreatedBy,
                    header.CreatedOn,
                    header.ApprovedBy,
                    header.ApprovedOn,  
                    header.IsDeleted,  
                    header.RecommendedBy,
                    header.RecommendedOn,   
                    header.PlaceOfEvent,
                    header.PlaceOfVisit, 
                    header.SourceOfFund,
                    header.SponsorName, 
                    dept.DepartmentID,
                    dept.DepartmentName,
                    rank.RankID,
                    rank.RankName 

                    GO");
        }
    }
}

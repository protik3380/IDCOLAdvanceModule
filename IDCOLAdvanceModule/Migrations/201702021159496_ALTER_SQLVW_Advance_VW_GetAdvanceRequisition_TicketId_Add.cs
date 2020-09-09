namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVW_Advance_VW_GetAdvanceRequisition_TicketId_Add : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAdvanceRequisition]
                    AS
                    SELECT NEWID() as Id,
                    u.UserName as RequesterUserName,
                    (u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName,
                    header.Id HeaderId, 
                    ticket.Id RequistionApprovalTicketId,
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
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.RequisitionApprovalTicket ticket 
                    ON ticket.AdvanceRequisitionHeaderId = header.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u
                    ON header.RequesterUserName = u.UserName
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
                    rank.RankName,
                    ticket.Id

                    GO


                    ");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAdvanceRequisition]
                    AS
                    SELECT NEWID() as Id,
                    u.UserName as RequesterUserName,
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
                    ON header.RequesterUserName = u.UserName
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

                    GO


                    ");
        }
    }
}

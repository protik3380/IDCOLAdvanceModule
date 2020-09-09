namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Advance_VW_GetPendingRequisitionReport : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetPendingRequisitionReport]

                    AS

                    SELECT NEWID() AS Id, 
					reqHeader.Id AS RequisitionId, 
					u.EmployeeID AS EmployeeId, 
					(u.FirstName+' '+u.MiddleName+' '+u.LAStName) AS EmployeeName,
				    empRank.RankName, 
					empRank.RankID AS RankId, 
					dept.DepartmentID AS DepartmentId, 
					dept.DepartmentName,
                    SUM(reqDetail.AdvanceAmount) AS AdvanceAmount, 
					(SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) AS AdvanceAmountInBDT,
					reqHeader.RequisitionDate AS RequisitionEntryDate, 
					reqHeader.Purpose AS RequisitionPurpose,
                    baseCat.Id AS BaseAdvanceCategoryId, 
					baseCat.Name AS BaseAdvanceCategoryName,
                    cat.Id AS AdvanceCategoryId, 
					cat.Name AS AdvanceCategoryName,
                   
					reqHeader.ConversionRate AS RequisitionConversionRate,
					reqHeader.Currency AS RequisitionCurrency,
				    reqHeader.ApprovedOn AS RequisitionApprovalDate,
                   
					appLevel.Name AS RequisitionApprovalLevelName,
				    reqStatus.Id AS RequisitionStatusId, 
					reqStatus.Name AS RequisitionStatus,
				    
					CASE WHEN reqHeader.PlaceOfVisit IS NULL 
					THEN reqHeader.PlaceOfEvent 
					WHEN reqHeader.PlaceOfVisitId IS NULL 
					THEN reqHeader.PlaceOfVisit 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE reqHeader.PlaceOfVisitId =pov.Id)
					END AS RequisitionPlaceOfOccurrance,	

					ISNULL((DATEDIFF(DAY,reqHeader.RequisitionDate,GETDATE())-IDCOLMIS.dbo.GetWeeklyHolidaysCount(reqHeader.RequisitionDate,GETDATE())),0) as ExpenseDaysCount
					
                    FROM AdvanceRequisitionHeader reqHeader 
                    LEFT OUTER JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON reqHeader.RequesterUserName = u.UserName
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON reqHeader.RequesterDepartmentId = dept.DepartmentID
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON reqHeader.RequesterRankID = empRank.RankID
                    LEFT OUTER JOIN AdvanceCategory cat ON reqHeader.AdvanceCategoryId = cat.Id
                    LEFT OUTER JOIN BASeAdvanceCategory baseCat ON cat.BASeAdvanceCategoryId = baseCat.Id
                    LEFT OUTER JOIN AdvanceExpenseHeader expHeader ON reqHeader.Id = expHeader.AdvanceRequisitionHeaderId
                    LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON expHeader.Id= expDetail.AdvanceExpenseHeaderId
                    LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
                    LEFT OUTER JOIN ApprovalStatus reqStatus ON ticket.ApprovalStatusId = reqStatus.Id
                    LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				    LEFT OUTER JOIN ApprovalTicket expenseTicket ON expenseTicket.AdvanceExpenseHeaderId = expHeader.Id
				    LEFT OUTER JOIN ApprovalStatus expenseStatus ON expenseTicket.ApprovalStatusId = expenseStatus.Id
                    LEFT OUTER JOIN ApprovalLevel expenseLevel ON expenseTicket.ApprovalLevelId = expenseLevel.Id
					LEFT OUTER JOIN IDCOLMIS.dbo.UserTable requisitionUser ON ticket.AuthorizedBy = requisitionUser.UserName
					LEFT OUTER JOIN IDCOLMIS.dbo.UserTable expenseUser ON expenseTicket.AuthorizedBy = expenseUser.UserName

                    GROUP BY 
					u.EmployeeID, 
					u.FirstName, 
					u.MiddleName, 
					u.LAStName, 
					empRank.RankName, 
					empRank.RankID,
                    dept.DepartmentID, 
					dept.DepartmentName, 
					reqHeader.RequisitionDate, 
					reqHeader.Id,
				    baseCat.Id, baseCat.Name,
                    cat.Id, cat.Name, 
					reqHeader.ConversionRate,
                    reqHeader.Currency, 
					reqStatus.Name,
                    reqHeader.ApprovedOn, 
					appLevel.Name, 
					reqStatus.Id,
					requisitionUser.FirstName, 
					requisitionUser.MiddleName, 
					requisitionUser.LastName,
					reqHeader.Purpose,
					reqHeader.PlaceOfVisit, 
					reqHeader.PlaceOfVisitId,
					reqHeader.PlaceOfEvent,
					ticket.AuthorizedOn

				    HAVING reqStatus.Id = 3

                GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetPendingRequisitionReport')
                    DROP VIEW Advance_VW_GetPendingRequisitionReport");
        }
    }
}

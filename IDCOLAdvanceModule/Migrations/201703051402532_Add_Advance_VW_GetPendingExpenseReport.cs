namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Advance_VW_GetPendingExpenseReport : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetPendingExpenseReport]

                    AS

                    SELECT NEWID() AS Id, 
					u.EmployeeID AS EmployeeId, 
					(u.FirstName+' '+u.MiddleName+' '+u.LAStName) AS EmployeeName,
				    empRank.RankName, 
					empRank.RankID AS RankId, 
					dept.DepartmentID AS DepartmentId, 
					dept.DepartmentName,
                    SUM(reqDetail.AdvanceAmount) AS AdvanceAmount, 
					
                    baseCat.Id AS BaseAdvanceCategoryId, 
					baseCat.Name AS BaseAdvanceCategoryName,
                    cat.Id AS AdvanceCategoryId, 
					cat.Name AS AdvanceCategoryName,
                    expHeader.Id ExpenseId, 
					expHeader.ExpenseEntryDate, 
					expHeader.Purpose AS ExpensePurpose, 
					SUM(expDetail.ExpenseAmount) AS ExpenseAmount,
                    
				    (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) AS ExpenseAmountInBDT,
                    expHeader.ConversionRate AS ExpenseConversionRate, 
					expHeader.Currency AS ExpenseCurrency,
				    
                    expHeader.ApprovedOn AS ExpenseApprovalDate, 
					expenseLevel.Name as ExpenseApprovalLevelName,
				    expenseStatus.Id AS ExpenseStatusId, 
					expenseStatus.Name AS ExpenseStatus,
					
					CASE WHEN expHeader.PlaceOfVisit IS NULL 
					THEN expHeader.PlaceOfEvent 
					WHEN expHeader.PlaceOfVisitId IS NULL 
					THEN expHeader.PlaceOfVisit 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
					END AS ExpensePlaceOfOccurrance,

					ISNULL((DATEDIFF(DAY,expHeader.ExpenseEntryDate,GETDATE())-IDCOLMIS.dbo.GetWeeklyHolidaysCount(expHeader.ExpenseEntryDate,GETDATE())),0) as ExpenseDaysCount
					
                    FROM AdvanceExpenseHeader expHeader 				
					LEFT OUTER JOIN ApprovalTicket expenseTicket ON expenseTicket.AdvanceExpenseHeaderId = expHeader.Id
					LEFT OUTER JOIN	AdvanceRequisitionHeader reqHeader ON reqHeader.Id = expHeader.AdvanceRequisitionHeaderId	
                    LEFT OUTER JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON expHeader.RequesterUserName = u.UserName
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON expHeader.RequesterDepartmentId = dept.DepartmentID
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON expHeader.RequesterRankID = empRank.RankID
                    LEFT OUTER JOIN AdvanceCategory cat ON expHeader.AdvanceCategoryId = cat.Id
                    LEFT OUTER JOIN BASeAdvanceCategory baseCat ON cat.BASeAdvanceCategoryId = baseCat.Id
                    
                    LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON expHeader.Id= expDetail.AdvanceExpenseHeaderId
                    LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
                    LEFT OUTER JOIN ApprovalStatus reqStatus ON ticket.ApprovalStatusId = reqStatus.Id
                    LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				   
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
					baseCat.Id, baseCat.Name,
                    cat.Id, cat.Name, 
					expHeader.ConversionRate, 
					expHeader.Currency, 
					expHeader.ApprovedOn, 
					appLevel.Name,
				    expenseStatus.Id, 
					expenseStatus.Name, 
					expHeader.Id,
					expenseUser.FirstName, 
					expenseUser.MiddleName,	
					expenseUser.LastName,
					expHeader.ExpenseEntryDate,
					expHeader.Purpose,
					expHeader.PlaceOfVisit, 
					expHeader.PlaceOfVisitId,
					expHeader.PlaceOfEvent,
					expenseLevel.Name

				    HAVING expenseStatus.Id = 3

                GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetPendingExpenseReport')
                    DROP VIEW Advance_VW_GetPendingExpenseReport");
        }
    }
}

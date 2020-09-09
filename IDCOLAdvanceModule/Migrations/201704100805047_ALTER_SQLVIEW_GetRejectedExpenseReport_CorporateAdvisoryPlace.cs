namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_GetRejectedExpenseReport_CorporateAdvisoryPlace : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetRejectedExpenseReport]

                AS

                SELECT NEWID() AS Id, 
				u.EmployeeID AS EmployeeId, 
				(u.FirstName+' '+u.MiddleName+' '+u.LAStName) AS EmployeeName,
				empRank.RankName, 
				empRank.RankID AS RankId, 
				dept.DepartmentID AS DepartmentId, 
				dept.DepartmentName,
                baseCat.Id AS BaseAdvanceCategoryId, 
				baseCat.Name AS BaseAdvanceCategoryName,
                cat.Id AS AdvanceCategoryId, 
				cat.Name AS AdvanceCategoryName,
                expHeader.Id ExpenseId, 
				expHeader.ExpenseEntryDate, 
				expHeader.Purpose AS ExpensePurpose, 
				SUM(expDetail.ExpenseAmount) AS ExpenseAmount,
				CASE WHEN expHeader.AdvanceCategoryId=2 THEN SUM(expDetail.ExpenseAmount * expDetail.ConversionRate)
				ELSE (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) 
				END AS ExpenseAmountInBDT,
                expHeader.ConversionRate AS ExpenseConversionRate, 
				expHeader.Currency AS ExpenseCurrency,				    
                expHeader.ApprovedOn AS ExpenseApprovalDate, 					
				expenseLevel.Name as ExpenseApprovalLevelName,				    
				expenseStatus.Id AS ExpenseStatusId, 
				expenseStatus.Name AS ExpenseStatus,					
				(expenseUser.FirstName+' '+expenseUser.MiddleName+' '+expenseUser.LastName) AS ExpenseRejectedBy,

				CASE WHEN expHeader.PlaceOfVisit IS NOT NULL THEN expHeader.PlaceOfVisit 
				WHEN expHeader.PlaceOfEvent IS NOT NULL THEN expHeader.PlaceOfEvent
				WHEN expHeader.CorporateAdvisoryPlaceOfEvent IS NOT NULL THEN expHeader.CorporateAdvisoryPlaceOfEvent
				ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
				END AS ExpensePlaceOfOccurrance,
					
				expenseTicket.Remarks AS ExpenseRejectioinRemarks,					
				expenseTicket.AuthorizedOn AS ExpenseRejectionDate
					
                FROM AdvanceExpenseHeader expHeader 				
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expenseTicket.AdvanceExpenseHeaderId = expHeader.Id					
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON expHeader.RequesterUserName = u.UserName
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON expHeader.RequesterDepartmentId = dept.DepartmentID
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON expHeader.RequesterRankID = empRank.RankID
                LEFT OUTER JOIN AdvanceCategory cat ON expHeader.AdvanceCategoryId = cat.Id
                LEFT OUTER JOIN BASeAdvanceCategory baseCat ON cat.BASeAdvanceCategoryId = baseCat.Id                    
                LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON expHeader.Id= expDetail.AdvanceExpenseHeaderId 
				LEFT OUTER JOIN ApprovalStatus expenseStatus ON expenseTicket.ApprovalStatusId = expenseStatus.Id
                LEFT OUTER JOIN ApprovalLevel expenseLevel ON expenseTicket.ApprovalLevelId = expenseLevel.Id					
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
				expHeader.CorporateAdvisoryPlaceOfEvent,
				expenseTicket.Remarks,
				expenseTicket.AuthorizedOn,
				expenseLevel.Name,
				expHeader.AdvanceCategoryId

				HAVING expenseStatus.Id = 6

                GO");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetRejectedExpenseReport]

                AS

                SELECT NEWID() AS Id, 
				u.EmployeeID AS EmployeeId, 
				(u.FirstName+' '+u.MiddleName+' '+u.LAStName) AS EmployeeName,
				empRank.RankName, 
				empRank.RankID AS RankId, 
				dept.DepartmentID AS DepartmentId, 
				dept.DepartmentName,
                baseCat.Id AS BaseAdvanceCategoryId, 
				baseCat.Name AS BaseAdvanceCategoryName,
                cat.Id AS AdvanceCategoryId, 
				cat.Name AS AdvanceCategoryName,
                expHeader.Id ExpenseId, 
				expHeader.ExpenseEntryDate, 
				expHeader.Purpose AS ExpensePurpose, 
				SUM(expDetail.ExpenseAmount) AS ExpenseAmount,
				CASE WHEN expHeader.AdvanceCategoryId=2 THEN SUM(expDetail.ExpenseAmount * expDetail.ConversionRate)
				ELSE (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) 
				END AS ExpenseAmountInBDT,
                expHeader.ConversionRate AS ExpenseConversionRate, 
				expHeader.Currency AS ExpenseCurrency,				    
                expHeader.ApprovedOn AS ExpenseApprovalDate, 					
				expenseLevel.Name as ExpenseApprovalLevelName,				    
				expenseStatus.Id AS ExpenseStatusId, 
				expenseStatus.Name AS ExpenseStatus,					
				(expenseUser.FirstName+' '+expenseUser.MiddleName+' '+expenseUser.LastName) AS ExpenseRejectedBy,

				CASE WHEN expHeader.PlaceOfVisit IS NOT NULL THEN expHeader.PlaceOfVisit 
				WHEN expHeader.PlaceOfEvent IS NOT NULL THEN expHeader.PlaceOfEvent 
				ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
				END AS ExpensePlaceOfOccurrance,
					
				expenseTicket.Remarks AS ExpenseRejectioinRemarks,					
				expenseTicket.AuthorizedOn AS ExpenseRejectionDate
					
                FROM AdvanceExpenseHeader expHeader 				
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expenseTicket.AdvanceExpenseHeaderId = expHeader.Id					
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON expHeader.RequesterUserName = u.UserName
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON expHeader.RequesterDepartmentId = dept.DepartmentID
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON expHeader.RequesterRankID = empRank.RankID
                LEFT OUTER JOIN AdvanceCategory cat ON expHeader.AdvanceCategoryId = cat.Id
                LEFT OUTER JOIN BASeAdvanceCategory baseCat ON cat.BASeAdvanceCategoryId = baseCat.Id                    
                LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON expHeader.Id= expDetail.AdvanceExpenseHeaderId 
				LEFT OUTER JOIN ApprovalStatus expenseStatus ON expenseTicket.ApprovalStatusId = expenseStatus.Id
                LEFT OUTER JOIN ApprovalLevel expenseLevel ON expenseTicket.ApprovalLevelId = expenseLevel.Id					
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
				expenseTicket.Remarks,
				expenseTicket.AuthorizedOn,
				expenseLevel.Name,
				expHeader.AdvanceCategoryId

				HAVING expenseStatus.Id = 6

                GO");
        }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_GetAgingReport_CorporateAdvisoryPlace : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAgingReport]
                AS

                SELECT NEWID() as Id,
				reqHeader.Id as RequisitionID,
				u.EmployeeID,
				(u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName,
				empRank.RankName,
				empRank.RankID,
				dept.DepartmentID,
				dept.DepartmentName,
                SUM(reqDetail.AdvanceAmount) as AdvanceAmount,
				reqHeader.RequisitionDate as RequisitionEntryDate,
                expHeader.ExpenseEntryDate,
				baseCat.Id as BaseAdvanceCategoryId,
				baseCat.Name as BaseAdvanceCategoryName,
                cat.Id as AdvanceCategoryId,
				cat.Name as AdvanceCategoryName,
                SUM(expDetail.ExpenseAmount) as ExpenseAmount,
                --(SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) as AdvanceAmountInBDT,
				
				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN (SUM(reqDetail.AdvanceAmount)*reqDetail.ConversionRate)
				ELSE (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) END AS AdvanceAmountInBDT,
                
				--reqHeader.ConversionRate as RequisitionConversionRate,
				
				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN reqDetail.ConversionRate
				ELSE reqHeader.ConversionRate END AS RequisitionConversionRate,

				--reqHeader.Currency as RequisitionCurrency,

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN reqDetail.Currency
				ELSE reqHeader.Currency END AS RequisitionCurrency,

                --,(SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) as ExpenseAmountInBDT,

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN (SUM(expDetail.ExpenseAmount)*expDetail.ConversionRate)
				ELSE (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) END AS ExpenseAmountInBDT,

                --,expHeader.ConversionRate as ExpenseConversionRate,

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN expDetail.ConversionRate
				ELSE expHeader.ConversionRate END AS ExpenseConversionRate,

				--expHeader.Currency as ExpenseCurrency

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN expDetail.Currency
				ELSE expHeader.Currency END AS ExpenseCurrency

                ,reqStatus.Name as RequisitionStatus,
				reqHeader.ApprovedOn as RequisitionApprovalDate,
                 expHeader.ApprovedOn as ExpenseApprovalDate,
				 appLevel.Name as ApprovalLevelName 
                 ,reqHeader.AdvanceRequisitionStatusId as RequisitionStatusId,
				 reqHeader.AdvanceIssueDate,reqHeader.Purpose,
				 CASE WHEN reqHeader.PlaceOfVisit IS NOT NULL THEN reqHeader.PlaceOfVisit 
					WHEN reqHeader.PlaceOfEvent IS NOT NULL THEN reqHeader.PlaceOfEvent
					WHEN reqHeader.CorporateAdvisoryPlaceOfEvent IS NOT NULL THEN reqHeader.CorporateAdvisoryPlaceOfEvent 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE reqHeader.PlaceOfVisitId =pov.Id)
					END AS RequisitionPlaceOfOccurrance,	

					CASE WHEN expHeader.PlaceOfVisit IS NOT NULL THEN expHeader.PlaceOfVisit 
					WHEN expHeader.PlaceOfEvent IS NOT NULL THEN expHeader.PlaceOfEvent
					WHEN expHeader.CorporateAdvisoryPlaceOfEvent IS NOT NULL THEN expHeader.CorporateAdvisoryPlaceOfEvent 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
					END AS ExpensePlaceOfOccurrance,

					--ISNULL((DATEDIFF(DAY,reqHeader.AdvanceIssueDate,
					CASE WHEN expHeader.AdvanceExpenseStatusId=4 
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END AS AdjustedDate,
					 ISNULL(DATEDIFF(DAY,reqHeader.AdvanceIssueDate,
					 CASE WHEN expHeader.AdvanceExpenseStatusId=4
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) - ISNULL(IDCOLMIS.dbo.GetPublicHolidayCount(reqHeader.AdvanceIssueDate, CASE WHEN expHeader.AdvanceExpenseStatusId=4 
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) - ISNULL(IDCOLMIS.dbo.GetWeeklyHolidaysCount(reqHeader.AdvanceIssueDate, CASE WHEN expHeader.AdvanceExpenseStatusId=4 
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) as DaysCount,

					expHeader.AdvanceExpenseStatusId AS ExpenseStatusId, 
					expenseStatus.Name AS ExpenseStatus,
					ISNULL(reqHeader.IsPaid,0) AS IsAdvancePaid,
					ISNULL(expHeader.IsPaid,0) AS IsExpensePaid,
					expHeader.ExpenseIssueDate
					
				 
 
                FROM AdvanceRequisitionHeader reqHeader

                LEFT OUTER JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON reqHeader.RequesterUserName = u.UserName
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON reqHeader.RequesterDepartmentId = dept.DepartmentID
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON reqHeader.RequesterRankID = empRank.RankID
                LEFT OUTER JOIN AdvanceCategory cat ON reqHeader.AdvanceCategoryId = cat.Id
                LEFT OUTER JOIN BaseAdvanceCategory baseCat ON cat.BaseAdvanceCategoryId = baseCat.Id
                LEFT OUTER JOIN AdvanceExpenseHeader expHeader ON expHeader.Id = reqHeader.AdvanceExpenseHeaderId
                LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON reqDetail.Id= expDetail.AdvanceRequisitionDetailId
                LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
                LEFT OUTER JOIN AdvanceStatus reqStatus ON reqHeader.AdvanceRequisitionStatusId = reqStatus.Id
                LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expHeader.Id = expenseTicket.AdvanceExpenseHeaderId
				LEFT OUTER JOIN AdvanceStatus expenseStatus ON expHeader.AdvanceExpenseStatusId = expenseStatus.Id
				WHERE reqHeader.AdvanceIssueDate IS NOT NULL

                GROUP BY 
				reqHeader.Id,
				u.EmployeeID,
				u.FirstName,
				u.MiddleName,
				u.LastName,
				empRank.RankName,
				empRank.RankID,
                dept.DepartmentID,
				dept.DepartmentName,
				reqHeader.RequisitionDate
				--expHeader.Id
                ,baseCat.Id,
				baseCat.Name,
                cat.Id,
				cat.Name,
				reqHeader.ConversionRate,
                reqHeader.Currency,
				expHeader.ConversionRate,
				expHeader.Currency,
				reqStatus.Name,
                reqHeader.ApprovedOn,
				expHeader.ApprovedOn,
				appLevel.Name,
				reqHeader.AdvanceRequisitionStatusId,
				reqHeader.AdvanceIssueDate,
				expHeader.ExpenseEntryDate,
				expHeader.Purpose,
				reqHeader.Purpose,
				reqHeader.PlaceOfVisit, 
				reqHeader.PlaceOfVisitId,
				reqHeader.PlaceOfEvent,
				reqHeader.CorporateAdvisoryPlaceOfEvent,
				expHeader.PlaceOfVisit, 
				expHeader.PlaceOfVisitId,
				expHeader.PlaceOfEvent,
				expHeader.CorporateAdvisoryPlaceOfEvent,
				expenseStatus.Id, 
				expenseStatus.Name,
				expHeader.AdvanceExpenseStatusId,
				reqHeader.IsPaid, 
				expHeader.IsPaid,
				expHeader.ExpenseIssueDate,
				reqHeader.AdvanceCategoryId,
				reqDetail.ConversionRate,
				reqDetail.Currency,
				expDetail.ConversionRate,
				expDetail.Currency

                GO");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAgingReport]
                AS

                SELECT NEWID() as Id,
				reqHeader.Id as RequisitionID,
				u.EmployeeID,
				(u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName,
				empRank.RankName,
				empRank.RankID,
				dept.DepartmentID,
				dept.DepartmentName,
                SUM(reqDetail.AdvanceAmount) as AdvanceAmount,
				reqHeader.RequisitionDate as RequisitionEntryDate,
                expHeader.ExpenseEntryDate,
				baseCat.Id as BaseAdvanceCategoryId,
				baseCat.Name as BaseAdvanceCategoryName,
                cat.Id as AdvanceCategoryId,
				cat.Name as AdvanceCategoryName,
                SUM(expDetail.ExpenseAmount) as ExpenseAmount,
                --(SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) as AdvanceAmountInBDT,
				
				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN (SUM(reqDetail.AdvanceAmount)*reqDetail.ConversionRate)
				ELSE (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) END AS AdvanceAmountInBDT,
                
				--reqHeader.ConversionRate as RequisitionConversionRate,
				
				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN reqDetail.ConversionRate
				ELSE reqHeader.ConversionRate END AS RequisitionConversionRate,

				--reqHeader.Currency as RequisitionCurrency,

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN reqDetail.Currency
				ELSE reqHeader.Currency END AS RequisitionCurrency,

                --,(SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) as ExpenseAmountInBDT,

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN (SUM(expDetail.ExpenseAmount)*expDetail.ConversionRate)
				ELSE (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) END AS ExpenseAmountInBDT,

                --,expHeader.ConversionRate as ExpenseConversionRate,

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN expDetail.ConversionRate
				ELSE expHeader.ConversionRate END AS ExpenseConversionRate,

				--expHeader.Currency as ExpenseCurrency

				CASE WHEN reqHeader.AdvanceCategoryId=2 THEN expDetail.Currency
				ELSE expHeader.Currency END AS ExpenseCurrency

                ,reqStatus.Name as RequisitionStatus,
				reqHeader.ApprovedOn as RequisitionApprovalDate,
                 expHeader.ApprovedOn as ExpenseApprovalDate,
				 appLevel.Name as ApprovalLevelName 
                 ,reqHeader.AdvanceRequisitionStatusId as RequisitionStatusId,
				 reqHeader.AdvanceIssueDate,reqHeader.Purpose,
				 CASE WHEN reqHeader.PlaceOfVisit IS NOT NULL THEN reqHeader.PlaceOfVisit 
					WHEN reqHeader.PlaceOfEvent IS NOT NULL THEN reqHeader.PlaceOfEvent 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE reqHeader.PlaceOfVisitId =pov.Id)
					END AS RequisitionPlaceOfOccurrance,	

					CASE WHEN expHeader.PlaceOfVisit IS NOT NULL THEN expHeader.PlaceOfVisit 
					WHEN expHeader.PlaceOfEvent IS NOT NULL THEN expHeader.PlaceOfEvent 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
					END AS ExpensePlaceOfOccurrance,

					--ISNULL((DATEDIFF(DAY,reqHeader.AdvanceIssueDate,
					CASE WHEN expHeader.AdvanceExpenseStatusId=4 
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END AS AdjustedDate,
					 ISNULL(DATEDIFF(DAY,reqHeader.AdvanceIssueDate,
					 CASE WHEN expHeader.AdvanceExpenseStatusId=4
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) - ISNULL(IDCOLMIS.dbo.GetPublicHolidayCount(reqHeader.AdvanceIssueDate, CASE WHEN expHeader.AdvanceExpenseStatusId=4 
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) - ISNULL(IDCOLMIS.dbo.GetWeeklyHolidaysCount(reqHeader.AdvanceIssueDate, CASE WHEN expHeader.AdvanceExpenseStatusId=4 
					THEN expHeader.ApprovedOn 
					WHEN expHeader.AdvanceExpenseStatusId<>4 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) as DaysCount,

					expHeader.AdvanceExpenseStatusId AS ExpenseStatusId, 
					expenseStatus.Name AS ExpenseStatus,
					ISNULL(reqHeader.IsPaid,0) AS IsAdvancePaid,
					ISNULL(expHeader.IsPaid,0) AS IsExpensePaid,
					expHeader.ExpenseIssueDate
					
				 
 
                FROM AdvanceRequisitionHeader reqHeader

                LEFT OUTER JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON reqHeader.RequesterUserName = u.UserName
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON reqHeader.RequesterDepartmentId = dept.DepartmentID
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON reqHeader.RequesterRankID = empRank.RankID
                LEFT OUTER JOIN AdvanceCategory cat ON reqHeader.AdvanceCategoryId = cat.Id
                LEFT OUTER JOIN BaseAdvanceCategory baseCat ON cat.BaseAdvanceCategoryId = baseCat.Id
                LEFT OUTER JOIN AdvanceExpenseHeader expHeader ON expHeader.Id = reqHeader.AdvanceExpenseHeaderId
                LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON reqDetail.Id= expDetail.AdvanceRequisitionDetailId
                LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
                LEFT OUTER JOIN AdvanceStatus reqStatus ON reqHeader.AdvanceRequisitionStatusId = reqStatus.Id
                LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expHeader.Id = expenseTicket.AdvanceExpenseHeaderId
				LEFT OUTER JOIN AdvanceStatus expenseStatus ON expHeader.AdvanceExpenseStatusId = expenseStatus.Id
				WHERE reqHeader.AdvanceIssueDate IS NOT NULL

                GROUP BY 
				reqHeader.Id,
				u.EmployeeID,
				u.FirstName,
				u.MiddleName,
				u.LastName,
				empRank.RankName,
				empRank.RankID,
                dept.DepartmentID,
				dept.DepartmentName,
				reqHeader.RequisitionDate
				--expHeader.Id
                ,baseCat.Id,
				baseCat.Name,
                cat.Id,
				cat.Name,
				reqHeader.ConversionRate,
                reqHeader.Currency,
				expHeader.ConversionRate,
				expHeader.Currency,
				reqStatus.Name,
                reqHeader.ApprovedOn,
				expHeader.ApprovedOn,
				appLevel.Name,
				reqHeader.AdvanceRequisitionStatusId,
				reqHeader.AdvanceIssueDate,
				expHeader.ExpenseEntryDate,
				expHeader.Purpose,
				reqHeader.Purpose,
				reqHeader.PlaceOfVisit, 
				reqHeader.PlaceOfVisitId,
				reqHeader.PlaceOfEvent,
				expHeader.PlaceOfVisit, 
				expHeader.PlaceOfVisitId,
				expHeader.PlaceOfEvent,
				expenseStatus.Id, 
				expenseStatus.Name,
				expHeader.AdvanceExpenseStatusId,
				reqHeader.IsPaid, 
				expHeader.IsPaid,
				expHeader.ExpenseIssueDate,
				reqHeader.AdvanceCategoryId,
				reqDetail.ConversionRate,
				reqDetail.Currency,
				expDetail.ConversionRate,
				expDetail.Currency

                GO");
        }
    }
}

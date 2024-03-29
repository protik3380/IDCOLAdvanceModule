namespace IDCOLAdvanceModule.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class ALTER_SQLVIEW_Advance_VW_GetAgingReport_AdjustedStatus : DbMigration
	{
		public override void Up()
		{

            Sql(@"USE [IDCOLMIS]
                IF EXISTS 
	                (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPublicHolidayCount]') AND type in (N'FN', N'IF',N'TF', N'FS', N'FT'))
	                DROP FUNCTION [dbo].[GetPublicHolidayCount];
	                GO
                CREATE FUNCTION GetPublicHolidayCount(@DateFrom date,@DateTo date)
	                returns int
	                AS
	                BEGIN
	                DECLARE @COUNT INT=0;

	                SET @COUNT = ( SELECT COUNT(HolidayDate) FROM IDCOLMIS.dbo.Accounts_NC_HolidayInfo
	                WHERE HolidayDate>=@DateFrom AND HolidayDate<=@DateTo)
	                return @COUNT;
	                END"
				);

			Sql(@"
					USE [IDCOLAdvanceDB]
					GO

				ALTER VIEW [dbo].[Advance_VW_GetAgingReport]
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
				(SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) as AdvanceAmountInBDT,
				reqHeader.ConversionRate as RequisitionConversionRate,
				reqHeader.Currency as RequisitionCurrency
				,(SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) as ExpenseAmountInBDT,
				expHeader.ConversionRate as ExpenseConversionRate,
				expHeader.Currency as ExpenseCurrency
				,reqStatus.Name as RequisitionStatus,
				reqHeader.ApprovedOn as RequisitionApprovalDate,
				 expHeader.ApprovedOn as ExpenseApprovalDate,
				 appLevel.Name as ApprovalLevelName 
				 ,reqStatus.Id as RequisitionStatusId,
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
					CASE WHEN expHeader.IsPaid=1 
					THEN expHeader.ExpenseIssueDate 
					WHEN expHeader.IsPaid<>1 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END AS AdjustedDate,
					 ISNULL(DATEDIFF(DAY,reqHeader.AdvanceIssueDate,
					 CASE WHEN expHeader.IsPaid=1
					THEN expHeader.ApprovedOn 
					WHEN expHeader.IsPaid<>1 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) - ISNULL(IDCOLMIS.dbo.GetPublicHolidayCount(reqHeader.AdvanceIssueDate, CASE WHEN expHeader.IsPaid=1 
					THEN expHeader.ExpenseIssueDate 
					WHEN expHeader.IsPaid<>1 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) - ISNULL(IDCOLMIS.dbo.GetWeeklyHolidaysCount(reqHeader.AdvanceIssueDate, CASE WHEN expHeader.IsPaid=1 
					THEN expHeader.ExpenseIssueDate 
					WHEN expHeader.IsPaid<>1 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) as DaysCount,

					expenseStatus.Id AS ExpenseStatusId, 
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
				LEFT OUTER JOIN AdvanceExpenseHeader expHeader ON reqHeader.Id = expHeader.AdvanceRequisitionHeaderId
				LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON reqDetail.Id= expDetail.AdvanceRequisitionDetailId
				LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
				LEFT OUTER JOIN ApprovalStatus reqStatus ON ticket.ApprovalStatusId = reqStatus.Id
				LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expHeader.Id = expenseTicket.AdvanceExpenseHeaderId
				LEFT OUTER JOIN ApprovalStatus expenseStatus ON expenseTicket.ApprovalStatusId = expenseStatus.Id
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
				reqStatus.Id,
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
				expHeader.ExpenseIssueDate

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
				(SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) as AdvanceAmountInBDT,
				reqHeader.ConversionRate as RequisitionConversionRate,
				reqHeader.Currency as RequisitionCurrency
				,(SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) as ExpenseAmountInBDT,
				expHeader.ConversionRate as ExpenseConversionRate,
				expHeader.Currency as ExpenseCurrency
				,reqStatus.Name as RequisitionStatus,
				reqHeader.ApprovedOn as RequisitionApprovalDate,
				 expHeader.ApprovedOn as ExpenseApprovalDate,
				 appLevel.Name as ApprovalLevelName 
				 ,reqStatus.Id as RequisitionStatusId,
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
					CASE WHEN expHeader.IsPaid=1 
					THEN expHeader.ExpenseIssueDate 
					WHEN expHeader.IsPaid<>1 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END AS AdjustedDate ,

					 ISNULL(DATEDIFF(DAY,reqHeader.AdvanceIssueDate,
					 
					 CASE WHEN expHeader.IsPaid=1
					THEN expHeader.ApprovedOn 
					WHEN expHeader.IsPaid<>1 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) - ISNULL(IDCOLMIS.dbo.GetWeeklyHolidaysCount(reqHeader.AdvanceIssueDate, CASE WHEN expHeader.IsPaid=1 
					THEN expHeader.ExpenseIssueDate 
					WHEN expHeader.IsPaid<>1 
					THEN GETDATE()
					 ELSE GETDATE() 
					 END),0) as DaysCount,

					expenseStatus.Id AS ExpenseStatusId, 
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
				LEFT OUTER JOIN AdvanceExpenseHeader expHeader ON reqHeader.Id = expHeader.AdvanceRequisitionHeaderId
				LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON reqDetail.Id= expDetail.AdvanceRequisitionDetailId
				LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
				LEFT OUTER JOIN ApprovalStatus reqStatus ON ticket.ApprovalStatusId = reqStatus.Id
				LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expHeader.Id = expenseTicket.AdvanceExpenseHeaderId
				LEFT OUTER JOIN ApprovalStatus expenseStatus ON expenseTicket.ApprovalStatusId = expenseStatus.Id
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
				reqStatus.Id,
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
				expHeader.ExpenseIssueDate

				GO");
		}
	}
}

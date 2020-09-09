namespace IDCOLAdvanceModule.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ALTER_SQLVIEW_Advance_GetAgingReport_DaysCount : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAgingReport]

                AS

                SELECT NEWID() as Id,reqHeader.Id as RequisitionID,expHeader.Id as ExpenseId,u.EmployeeID,(u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName
                ,empRank.RankName,empRank.RankID,dept.DepartmentID,dept.DepartmentName,
                SUM(reqDetail.AdvanceAmount) as AdvanceAmount,reqHeader.RequisitionDate as RequisitionEntryDate,
                expHeader.ExpenseEntryDate,baseCat.Id as BaseAdvanceCategoryId,baseCat.Name as BaseAdvanceCategoryName,
                cat.Id as AdvanceCategoryId,cat.Name as AdvanceCategoryName,
                SUM(expDetail.ExpenseAmount) as ExpenseAmount,
                (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) as AdvanceAmountInBDT,
                reqHeader.ConversionRate as RequisitionConversionRate,reqHeader.Currency as RequisitionCurrency
                ,(SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) as ExpenseAmountInBDT,
                expHeader.ConversionRate as ExpenseConversionRate,expHeader.Currency as ExpenseCurrency
                ,reqStatus.Name as RequisitionStatus,reqHeader.ApprovedOn as RequisitionApprovalDate,
                 expHeader.ApprovedOn as ExpenseApprovalDate,appLevel.Name as ApprovalLevelName 
                 ,reqStatus.Id as RequisitionStatusId,reqHeader.AdvanceIssueDate,reqHeader.Purpose,
				 CASE WHEN reqHeader.PlaceOfVisit IS NULL 
					THEN reqHeader.PlaceOfEvent 
					WHEN reqHeader.PlaceOfVisitId IS NULL 
					THEN reqHeader.PlaceOfVisit 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE reqHeader.PlaceOfVisitId =pov.Id)
					END AS RequisitionPlaceOfOccurrance,	

					CASE WHEN expHeader.PlaceOfVisit IS NULL 
					THEN expHeader.PlaceOfEvent 
					WHEN expHeader.PlaceOfVisitId IS NULL 
					THEN expHeader.PlaceOfVisit 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
					END AS ExpensePlaceOfOccurrance,
					ISNULL((DATEDIFF(DAY,expHeader.ApprovedOn,GETDATE())-IDCOLMIS.dbo.GetWeeklyHolidaysCount(expHeader.ApprovedOn,GETDATE())),0) as DaysCount
				 
 
                FROM AdvanceRequisitionHeader reqHeader

                LEFT OUTER JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON reqHeader.RequesterUserName = u.UserName
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON reqHeader.RequesterDepartmentId = dept.DepartmentID
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON reqHeader.RequesterRankID = empRank.RankID
                LEFT OUTER JOIN AdvanceCategory cat ON reqHeader.AdvanceCategoryId = cat.Id
                LEFT OUTER JOIN BaseAdvanceCategory baseCat ON cat.BaseAdvanceCategoryId = baseCat.Id
                LEFT OUTER JOIN AdvanceExpenseHeader expHeader ON reqHeader.Id = expHeader.AdvanceRequisitionHeaderId
                LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON expHeader.Id= expDetail.AdvanceExpenseHeaderId
                LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
                LEFT OUTER JOIN ApprovalStatus reqStatus ON ticket.ApprovalStatusId = reqStatus.Id
                LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expHeader.Id = expenseTicket.AdvanceExpenseHeaderId

                GROUP BY 
				u.EmployeeID,
				u.FirstName,
				u.MiddleName,
				u.LastName,
				empRank.RankName,
				empRank.RankID,
                dept.DepartmentID,
				dept.DepartmentName,
				reqHeader.RequisitionDate,
				reqHeader.Id
                ,baseCat.Id,
				baseCat.Name,
                cat.Id,
				cat.Name,
				expDetail.ExpenseAmount,
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
				expHeader.Id,
				expHeader.ExpenseEntryDate,
				expHeader.Purpose,
				reqHeader.Purpose,
				reqHeader.PlaceOfVisit, 
				reqHeader.PlaceOfVisitId,
				reqHeader.PlaceOfEvent,
				expHeader.PlaceOfVisit, 
				expHeader.PlaceOfVisitId,
				expHeader.PlaceOfEvent

                GO");
        }

        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAgingReport]

                AS

                SELECT NEWID() as Id,reqHeader.Id as RequisitionID,expHeader.Id as ExpenseId,u.EmployeeID,(u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName
                ,empRank.RankName,empRank.RankID,dept.DepartmentID,dept.DepartmentName,
                SUM(reqDetail.AdvanceAmount) as AdvanceAmount,reqHeader.RequisitionDate as RequisitionEntryDate,
                expHeader.ExpenseEntryDate,baseCat.Id as BaseAdvanceCategoryId,baseCat.Name as BaseAdvanceCategoryName,
                cat.Id as AdvanceCategoryId,cat.Name as AdvanceCategoryName,
                SUM(expDetail.ExpenseAmount) as ExpenseAmount,
                (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) as AdvanceAmountInBDT,
                reqHeader.ConversionRate as RequisitionConversionRate,reqHeader.Currency as RequisitionCurrency
                ,(SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) as ExpenseAmountInBDT,
                expHeader.ConversionRate as ExpenseConversionRate,expHeader.Currency as ExpenseCurrency
                ,reqStatus.Name as RequisitionStatus,ticket.AuthorizedOn as RequisitionApprovalDate,
                 expenseTicket.AuthorizedOn as ExpenseApprovalDate,appLevel.Name as ApprovalLevelName 
                 ,reqStatus.Id as RequisitionStatusId,reqHeader.AdvanceIssueDate,reqHeader.Purpose,
				 CASE WHEN reqHeader.PlaceOfVisit IS NULL 
					THEN reqHeader.PlaceOfEvent 
					WHEN reqHeader.PlaceOfVisitId IS NULL 
					THEN reqHeader.PlaceOfVisit 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE reqHeader.PlaceOfVisitId =pov.Id)
					END AS RequisitionPlaceOfOccurrance,	

					CASE WHEN expHeader.PlaceOfVisit IS NULL 
					THEN expHeader.PlaceOfEvent 
					WHEN expHeader.PlaceOfVisitId IS NULL 
					THEN expHeader.PlaceOfVisit 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
					END AS ExpensePlaceOfOccurrance
				 
 
                FROM AdvanceRequisitionHeader reqHeader

                LEFT OUTER JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u ON reqHeader.RequesterUserName = u.UserName
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept ON reqHeader.RequesterDepartmentId = dept.DepartmentID
                LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank empRank ON reqHeader.RequesterRankID = empRank.RankID
                LEFT OUTER JOIN AdvanceCategory cat ON reqHeader.AdvanceCategoryId = cat.Id
                LEFT OUTER JOIN BaseAdvanceCategory baseCat ON cat.BaseAdvanceCategoryId = baseCat.Id
                LEFT OUTER JOIN AdvanceExpenseHeader expHeader ON reqHeader.Id = expHeader.AdvanceRequisitionHeaderId
                LEFT OUTER JOIN AdvanceExpenseDetail expDetail ON expHeader.Id= expDetail.AdvanceExpenseHeaderId
                LEFT OUTER JOIN ApprovalTicket ticket ON reqHeader.Id = ticket.AdvanceRequisitionHeaderId
                LEFT OUTER JOIN ApprovalStatus reqStatus ON ticket.ApprovalStatusId = reqStatus.Id
                LEFT OUTER JOIN ApprovalLevel appLevel ON ticket.ApprovalLevelId = appLevel.Id
				LEFT OUTER JOIN ApprovalTicket expenseTicket ON expHeader.Id = expenseTicket.AdvanceExpenseHeaderId

                GROUP BY 
				u.EmployeeID,
				u.FirstName,
				u.MiddleName,
				u.LastName,
				empRank.RankName,
				empRank.RankID,
                dept.DepartmentID,
				dept.DepartmentName,
				reqHeader.RequisitionDate,
				reqHeader.Id
                ,baseCat.Id,
				baseCat.Name,
                cat.Id,
				cat.Name,
				expDetail.ExpenseAmount,
				reqHeader.ConversionRate,
                reqHeader.Currency,
				expHeader.ConversionRate,
				expHeader.Currency,
				reqStatus.Name,
                ticket.AuthorizedOn,
				expenseTicket.AuthorizedOn,
				appLevel.Name,
				reqStatus.Id,
				reqHeader.AdvanceIssueDate,
				expHeader.Id,
				expHeader.ExpenseEntryDate,
				expHeader.Purpose,
				reqHeader.Purpose,
				reqHeader.PlaceOfVisit, 
				reqHeader.PlaceOfVisitId,
				reqHeader.PlaceOfEvent,
				expHeader.PlaceOfVisit, 
				expHeader.PlaceOfVisitId,
				expHeader.PlaceOfEvent

                GO");
        }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_Advance_VW_GetRejectedExpenseReport_PlaceOfVisit_Fixed : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetRejectedExpenseReport]

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
					reqHeader.RequisitionDate AS RequisitionEntryDate, 
					reqHeader.Purpose AS RequisitionPurpose,
                    baseCat.Id AS BaseAdvanceCategoryId, 
					baseCat.Name AS BaseAdvanceCategoryName,
                    cat.Id AS AdvanceCategoryId, 
					cat.Name AS AdvanceCategoryName,
                    expHeader.Id ExpenseId, 
					expHeader.ExpenseEntryDate, 
					expHeader.Purpose AS ExpensePurpose, 
					SUM(expDetail.ExpenseAmount) AS ExpenseAmount,
                    (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) AS AdvanceAmountInBDT,
                    reqHeader.ConversionRate AS RequisitionConversionRate,
					reqHeader.Currency AS RequisitionCurrency,
				    (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) AS ExpenseAmountInBDT,
                    expHeader.ConversionRate AS ExpenseConversionRate, 
					expHeader.Currency AS ExpenseCurrency,
				    reqHeader.ApprovedOn AS RequisitionApprovalDate,
                    expHeader.ApprovedOn AS ExpenseApprovalDate, 
					appLevel.Name AS ApprovalLevelName,
					expenseLevel.Name as ExpenseApprovalLevelName,
				    reqStatus.Id AS RequisitionStatusId, 
					reqStatus.Name AS RequisitionStatus,
				    expenseStatus.Id AS ExpenseStatusId, 
					expenseStatus.Name AS ExpenseStatus,
					(requisitionUser.FirstName+' '+requisitionUser.MiddleName+' '+requisitionUser.LastName) AS RequisitionRejectedBy,
					(expenseUser.FirstName+' '+expenseUser.MiddleName+' '+expenseUser.LastName) AS ExpenseRejectedBy,

					CASE WHEN reqHeader.PlaceOfVisit IS NOT NULL THEN reqHeader.PlaceOfVisit 
					WHEN reqHeader.PlaceOfEvent IS NOT NULL THEN reqHeader.PlaceOfEvent 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE reqHeader.PlaceOfVisitId =pov.Id)
					END AS RequisitionPlaceOfOccurrance,	

					CASE WHEN expHeader.PlaceOfVisit IS NOT NULL THEN expHeader.PlaceOfVisit 
					WHEN expHeader.PlaceOfEvent IS NOT NULL THEN expHeader.PlaceOfEvent 
					ELSE ( SELECT TOP 1 pov.Name FROM PlaceOfVisit pov WHERE expHeader.PlaceOfVisitId =pov.Id)
					END AS ExpensePlaceOfOccurrance,

					ticket.Remarks AS RequisitionRejectionRemarks,
					expenseTicket.Remarks AS ExpenseRejectioinRemarks,
					ticket.AuthorizedOn AS RequisitionRejectionDate,
					expenseTicket.AuthorizedOn AS ExpenseRejectionDate
					
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
					reqHeader.RequisitionDate, 
					reqHeader.Id,
				    baseCat.Id, baseCat.Name,
                    cat.Id, cat.Name, 
					reqHeader.ConversionRate,
                    reqHeader.Currency, 
					expHeader.ConversionRate, 
					expHeader.Currency, 
					reqStatus.Name,
                    reqHeader.ApprovedOn, 
					expHeader.ApprovedOn, 
					appLevel.Name, 
					reqStatus.Id,
				    expenseStatus.Id, 
					expenseStatus.Name, 
					expHeader.Id,
					requisitionUser.FirstName, 
					requisitionUser.MiddleName, 
					requisitionUser.LastName,
					expenseUser.FirstName, 
					expenseUser.MiddleName,	
					expenseUser.LastName,
					expHeader.ExpenseEntryDate,
					expHeader.Purpose,
					reqHeader.Purpose,
					reqHeader.PlaceOfVisit, 
					reqHeader.PlaceOfVisitId,
					reqHeader.PlaceOfEvent,
					expHeader.PlaceOfVisit, 
					expHeader.PlaceOfVisitId,
					expHeader.PlaceOfEvent,
					ticket.Remarks,
					expenseTicket.Remarks,
					ticket.AuthorizedOn,
					expenseTicket.AuthorizedOn,
					expenseLevel.Name

				    HAVING expenseStatus.Id = 6

                GO");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetRejectedExpenseReport]

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
					reqHeader.RequisitionDate AS RequisitionEntryDate, 
					reqHeader.Purpose AS RequisitionPurpose,
                    baseCat.Id AS BaseAdvanceCategoryId, 
					baseCat.Name AS BaseAdvanceCategoryName,
                    cat.Id AS AdvanceCategoryId, 
					cat.Name AS AdvanceCategoryName,
                    expHeader.Id ExpenseId, 
					expHeader.ExpenseEntryDate, 
					expHeader.Purpose AS ExpensePurpose, 
					SUM(expDetail.ExpenseAmount) AS ExpenseAmount,
                    (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) AS AdvanceAmountInBDT,
                    reqHeader.ConversionRate AS RequisitionConversionRate,
					reqHeader.Currency AS RequisitionCurrency,
				    (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) AS ExpenseAmountInBDT,
                    expHeader.ConversionRate AS ExpenseConversionRate, 
					expHeader.Currency AS ExpenseCurrency,
				    reqHeader.ApprovedOn AS RequisitionApprovalDate,
                    expHeader.ApprovedOn AS ExpenseApprovalDate, 
					appLevel.Name AS ApprovalLevelName,
					expenseLevel.Name as ExpenseApprovalLevelName,
				    reqStatus.Id AS RequisitionStatusId, 
					reqStatus.Name AS RequisitionStatus,
				    expenseStatus.Id AS ExpenseStatusId, 
					expenseStatus.Name AS ExpenseStatus,
					(requisitionUser.FirstName+' '+requisitionUser.MiddleName+' '+requisitionUser.LastName) AS RequisitionRejectedBy,
					(expenseUser.FirstName+' '+expenseUser.MiddleName+' '+expenseUser.LastName) AS ExpenseRejectedBy,

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

					ticket.Remarks AS RequisitionRejectionRemarks,
					expenseTicket.Remarks AS ExpenseRejectioinRemarks,
					ticket.AuthorizedOn AS RequisitionRejectionDate,
					expenseTicket.AuthorizedOn AS ExpenseRejectionDate
					
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
					reqHeader.RequisitionDate, 
					reqHeader.Id,
				    baseCat.Id, baseCat.Name,
                    cat.Id, cat.Name, 
					reqHeader.ConversionRate,
                    reqHeader.Currency, 
					expHeader.ConversionRate, 
					expHeader.Currency, 
					reqStatus.Name,
                    reqHeader.ApprovedOn, 
					expHeader.ApprovedOn, 
					appLevel.Name, 
					reqStatus.Id,
				    expenseStatus.Id, 
					expenseStatus.Name, 
					expHeader.Id,
					requisitionUser.FirstName, 
					requisitionUser.MiddleName, 
					requisitionUser.LastName,
					expenseUser.FirstName, 
					expenseUser.MiddleName,	
					expenseUser.LastName,
					expHeader.ExpenseEntryDate,
					expHeader.Purpose,
					reqHeader.Purpose,
					reqHeader.PlaceOfVisit, 
					reqHeader.PlaceOfVisitId,
					reqHeader.PlaceOfEvent,
					expHeader.PlaceOfVisit, 
					expHeader.PlaceOfVisitId,
					expHeader.PlaceOfEvent,
					ticket.Remarks,
					expenseTicket.Remarks,
					ticket.AuthorizedOn,
					expenseTicket.AuthorizedOn,
					expenseLevel.Name

				    HAVING expenseStatus.Id = 6

                GO");
        }
    }
}

namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SQLVIEW_Add_Advance_VW_GetRejectedRequisitionExpenseReport : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetRejectedRequisitionExpenseReport]

                    AS

                    SELECT NEWID() AS Id,reqHeader.Id AS RequisitionId,reqDetail.Id AS RequisitionDetailId, u.EmployeeID AS EmployeeId, (u.FirstName+' '+u.MiddleName+' '+u.LAStName) AS EmployeeName,
				    empRank.RankName, empRank.RankID AS RankId, dept.DepartmentID AS DepartmentId, dept.DepartmentName,
                    SUM(reqDetail.AdvanceAmount) AS AdvanceAmount, reqHeader.RequisitionDate AS AdvanceIssueDate,
                    baseCat.Id AS BaseAdvanceCategoryId, baseCat.Name AS BaseAdvanceCategoryName,
                    cat.Id AS AdvanceCategoryId, cat.Name AS AdvanceCategoryName,
                    SUM(expDetail.ExpenseAmount) AS ExpenseAmount,
                    (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) AS AdvanceAmountInBDT,
                    reqHeader.ConversionRate AS RequisitionConversionRate,reqHeader.Currency AS RequisitionCurrency,
				    (SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) AS ExpenseAmountInBDT,
                    expHeader.ConversionRate AS ExpenseConversionRate, expHeader.Currency AS ExpenseCurrency,
				    reqHeader.ApprovedOn AS RequisitionApprovalDate,
                    expHeader.ApprovedOn AS ExpenseApprovalDate, appLevel.Name AS ApprovalLevelName,
				    reqStatus.Id AS RequisitionStatusId, reqStatus.Name AS RequisitionStatus,
				    expenseStatus.Id AS ExpenseStatusId, expenseStatus.Name AS ExpenseStatus
 
                    FROM AdvanceRequisitionDetail reqDetail
                    LEFT OUTER JOIN AdvanceRequisitionHeader reqHeader ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
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

                    GROUP BY u.EmployeeID, u.FirstName, u.MiddleName, u.LAStName, empRank.RankName, empRank.RankID,
                    dept.DepartmentID, dept.DepartmentName, reqHeader.RequisitionDate, reqHeader.Id,
				    baseCat.Id, baseCat.Name,
                    cat.Id, cat.Name, expDetail.ExpenseAmount, reqHeader.ConversionRate,
                    reqHeader.Currency, expHeader.ConversionRate, expHeader.Currency, reqStatus.Name,
                    reqHeader.ApprovedOn, expHeader.ApprovedOn, appLevel.Name, reqStatus.Id, reqDetail.Id,
				    expenseStatus.Id, expenseStatus.Name

				    HAVING reqStatus.Id = 6 OR expenseStatus.Id = 6
                GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetRejectedRequisitionExpenseReport')
                    DROP VIEW Advance_VW_GetRejectedRequisitionExpenseReport");
        }
    }
}

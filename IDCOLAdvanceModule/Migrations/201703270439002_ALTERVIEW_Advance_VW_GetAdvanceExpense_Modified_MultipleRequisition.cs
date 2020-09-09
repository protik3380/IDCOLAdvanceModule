namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ALTERVIEW_Advance_VW_GetAdvanceExpense_Modified_MultipleRequisition : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAdvanceExpense]
                    AS
                    SELECT NEWID() AS Id, u.UserName AS RequesterUserName, u.FirstName + ' ' + u.MiddleName + ' ' + u.LastName AS EmployeeName, 
					header.Id AS HeaderId, 
					header.ExpenseNo,
					ticket.Id AS ExpenseApprovalTicketId, 
                    ticket.ApprovalStatusId, 
					ticket.AuthorizedOn,
                    header.AdvanceCategoryId, 
                    category.Name AS AdvanceCategoryName, 
                    category.IsCeilingApplicable, 
                    category.CeilingAmount, 
                    header.FromDate, 
                    header.ToDate, 
                    header.Currency,
                    header.ConversionRate, 
                    header.NoOfDays, 
                    header.Purpose, 
                    header.ExpenseEntryDate, 
                    header.AdvanceExpenseStatusId,
                    status.Name AS AdvanceExpenseStatusName, 
                    header.CreatedBy, 
                    header.CreatedOn, 
                    header.ApprovedBy, 
                    header.ApprovedOn, 
                    header.IsDeleted, 
                    header.RecommendedBy, 
                    header.RecommendedOn, 
                    dept.DepartmentID AS EmployeeDepartmentID, 
                    dept.DepartmentName AS EmployeeDepartmentName, 
                    rank.RankID AS EmployeeRankID, 
                    rank.RankName AS EmployeeRankName, 
                    ISNULL(SUM(detail.AdvanceAmount), 0) AS AdvanceAmount,
					CASE WHEN header.AdvanceCategoryId=2 THEN ISNULL(SUM(detail.AdvanceAmount * detail.ConversionRate), 0)
					ELSE ISNULL((SUM(detail.AdvanceAmount)*header.ConversionRate), 0) 
					END AS AdvanceAmountInBDT,
                    ISNULL(SUM(detail.ExpenseAmount), 0) AS ExpenseAmount,
					CASE WHEN header.AdvanceCategoryId=2 THEN ISNULL(SUM(detail.ExpenseAmount * detail.ConversionRate), 0)
					ELSE ISNULL((SUM(detail.ExpenseAmount)*header.ConversionRate), 0) 
					END AS ExpenseAmountInBDT,
					header.IsPaid,
					header.PaidBy,
					header.ExpenseIssueDate
	                FROM            
                    dbo.AdvanceExpenseHeader AS header LEFT OUTER JOIN
                    dbo.AdvanceExpenseDetail AS detail ON detail.AdvanceExpenseHeaderId = header.Id LEFT OUTER JOIN
					dbo.AdvanceRequisitionHeader AS requisitionHeader On requisitionHeader.AdvanceExpenseHeaderId = header.Id LEFT OUTER JOIN
                    dbo.AdvanceCategory AS category ON header.AdvanceCategoryId = category.Id LEFT OUTER JOIN
                    dbo.AdvanceStatus AS status ON header.AdvanceExpenseStatusId = status.Id LEFT OUTER JOIN
                    dbo.BaseAdvanceCategory AS base ON category.BaseAdvanceCategoryId = base.Id LEFT OUTER JOIN
                    dbo.ApprovalTicket AS ticket ON ticket.AdvanceExpenseHeaderId = header.Id LEFT OUTER JOIN
                    IDCOLMIS.dbo.UserTable AS u ON header.RequesterUserName = u.UserName LEFT OUTER JOIN
                    IDCOLMIS.dbo.Admin_Departments AS dept ON u.DepartmentID = dept.DepartmentID LEFT OUTER JOIN
                    IDCOLMIS.dbo.Admin_Rank AS rank ON u.RankID = rank.RankID
                    GROUP BY header.Id,
					u.UserName, 
					u.FirstName,
					u.MiddleName,
					u.LastName, 
					header.ExpenseNo,
					header.AdvanceCategoryId,
					category.Name, 
					category.IsCeilingApplicable,
					category.CeilingAmount,
					header.FromDate, 
					header.ToDate, 
                    header.Currency, 
					header.ConversionRate, 
					header.NoOfDays, 
					header.Purpose, 
					header.ExpenseEntryDate, 
					header.AdvanceExpenseStatusId,
					status.Name, 
					header.CreatedBy,
					header.CreatedOn, 
					header.ApprovedBy,
					header.ApprovedOn, 
					header.IsDeleted, 
					header.RecommendedBy, 
					header.RecommendedOn,
					dept.DepartmentID, 
					dept.DepartmentName, 
					rank.RankID, 
					rank.RankName, 
					ticket.Id, 
                    ticket.ApprovalStatusId,
					header.IsPaid,
					header.PaidBy,
					header.ExpenseIssueDate,
					header.AdvanceCategoryId,
					header.ConversionRate,
					ticket.AuthorizedOn
                GO");
        }

        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAdvanceExpense]
                    AS
                    SELECT NEWID() AS Id, u.UserName AS RequesterUserName, u.FirstName + ' ' + u.MiddleName + ' ' + u.LastName AS EmployeeName, 
					header.Id AS HeaderId, 
					requisitionHeader.Id As RequisitionHeaderId,
					header.ExpenseNo,
					ticket.Id AS ExpenseApprovalTicketId, 
                    ticket.ApprovalStatusId,
					ticket.AuthorizedOn,
                    header.AdvanceCategoryId, 
                    category.Name AS AdvanceCategoryName, 
                    category.IsCeilingApplicable, 
                    category.CeilingAmount, 
                    header.FromDate, 
                    header.ToDate, 
                    header.Currency,
                    header.ConversionRate, 
                    header.NoOfDays, 
                    header.Purpose, 
                    header.ExpenseEntryDate, 
                    header.AdvanceExpenseStatusId,
                    status.Name AS AdvanceExpenseStatusName, 
                    header.CreatedBy, 
                    header.CreatedOn, 
                    header.ApprovedBy, 
                    header.ApprovedOn, 
                    header.IsDeleted, 
                    header.RecommendedBy, 
                    header.RecommendedOn, 
                    dept.DepartmentID AS EmployeeDepartmentID, 
                    dept.DepartmentName AS EmployeeDepartmentName, 
                    rank.RankID AS EmployeeRankID, 
                    rank.RankName AS EmployeeRankName, 
                    ISNULL(SUM(detail.AdvanceAmount), 0) AS AdvanceAmount,
					CASE WHEN requisitionHeader.AdvanceCategoryId=2 THEN ISNULL(SUM(detail.AdvanceAmount * detail.ConversionRate), 0)
					ELSE ISNULL((SUM(detail.AdvanceAmount)*requisitionHeader.ConversionRate), 0) 
					END AS AdvanceAmountInBDT,
                    ISNULL(SUM(detail.ExpenseAmount), 0) AS ExpenseAmount,
					CASE WHEN header.AdvanceCategoryId=2 THEN ISNULL(SUM(detail.AdvanceAmount * detail.ConversionRate), 0)
					ELSE ISNULL((SUM(detail.ExpenseAmount)*header.ConversionRate), 0) 
					END AS ExpenseAmountInBDT,
					header.IsPaid,
					header.PaidBy,
					header.ExpenseIssueDate
	                FROM            
                    dbo.AdvanceExpenseHeader AS header LEFT OUTER JOIN
                    dbo.AdvanceExpenseDetail AS detail ON detail.AdvanceExpenseHeaderId = header.Id LEFT OUTER JOIN
					dbo.AdvanceRequisitionHeader AS requisitionHeader On requisitionHeader.Id = header.AdvanceRequisitionHeaderId LEFT OUTER JOIN
                    dbo.AdvanceCategory AS category ON header.AdvanceCategoryId = category.Id LEFT OUTER JOIN
                    dbo.AdvanceStatus AS status ON header.AdvanceExpenseStatusId = status.Id LEFT OUTER JOIN
                    dbo.BaseAdvanceCategory AS base ON category.BaseAdvanceCategoryId = base.Id LEFT OUTER JOIN
                    dbo.ApprovalTicket AS ticket ON ticket.AdvanceExpenseHeaderId = header.Id LEFT OUTER JOIN
                    IDCOLMIS.dbo.UserTable AS u ON header.RequesterUserName = u.UserName LEFT OUTER JOIN
                    IDCOLMIS.dbo.Admin_Departments AS dept ON u.DepartmentID = dept.DepartmentID LEFT OUTER JOIN
                    IDCOLMIS.dbo.Admin_Rank AS rank ON u.RankID = rank.RankID
                    GROUP BY header.Id,
					u.UserName, 
					u.FirstName,
					u.MiddleName,
					u.LastName, 
					header.ExpenseNo,
					header.AdvanceCategoryId,
					category.Name, 
					category.IsCeilingApplicable,
					category.CeilingAmount,
					header.FromDate, 
					header.ToDate, 
                    header.Currency, 
					header.ConversionRate, 
					header.NoOfDays, 
					header.Purpose, 
					header.ExpenseEntryDate, 
					header.AdvanceExpenseStatusId,
					status.Name, 
					header.CreatedBy,
					header.CreatedOn, 
					header.ApprovedBy,
					header.ApprovedOn, 
					header.IsDeleted, 
					header.RecommendedBy, 
					header.RecommendedOn,
					dept.DepartmentID, 
					dept.DepartmentName, 
					rank.RankID, 
					rank.RankName, 
					ticket.Id, 
                    ticket.ApprovalStatusId,
					requisitionHeader.Id,
					header.IsPaid,
					header.PaidBy,
					header.ExpenseIssueDate,
					requisitionHeader.AdvanceCategoryId,
					requisitionHeader.ConversionRate,
					ticket.AuthorizedOn");
        }
    }
}

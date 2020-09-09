namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SQLVIEW_Advance_VW_GetAdvanceExpense : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetAdvanceExpense]
                    AS
                    SELECT        NEWID() AS Id, u.UserName AS RequesterUserName, u.FirstName + ' ' + u.MiddleName + ' ' + u.LastName AS EmployeeName, header.Id AS HeaderId, ticket.Id AS RequistionApprovalTicketId, 
                                             ticket.ApprovalStatusId, header.AdvanceCategoryId, category.Name AS RequisitionCategoryName, category.IsCeilingApplicable, category.CeilingAmount, header.FromDate, header.ToDate, header.Currency, 
                                             header.ConversionRate, header.NoOfDays, header.Purpose, header.ExpenseEntryDate, header.AdvanceExpenseStatusId, status.Name AS RequisitionStatusName, header.CreatedBy, header.CreatedOn, 
                                             header.ApprovedBy, header.ApprovedOn, header.IsDeleted, header.RecommendedBy, header.RecommendedOn, dept.DepartmentID AS EmployeeDepartmentID, 
                                             dept.DepartmentName AS EmployeeDepartmentName, rank.RankID AS EmployeeRankID, rank.RankName AS EmployeeRankName, ISNULL(SUM(detail.AdvanceAmount), 0) AS AdvanceAmount,ISNULL(SUM(detail.ExpenseAmount), 0) AS ExpenseAmount
                    FROM            dbo.AdvanceExpenseHeader AS header LEFT OUTER JOIN
                                             dbo.AdvanceExpenseDetail AS detail ON detail.AdvanceExpenseHeaderId = header.Id LEFT OUTER JOIN
                                             dbo.AdvanceCategory AS category ON header.AdvanceCategoryId = category.Id LEFT OUTER JOIN
                                             dbo.AdvanceExpenseStatus AS status ON header.AdvanceExpenseStatusId = status.Id LEFT OUTER JOIN
                                             dbo.BaseAdvanceCategory AS base ON category.BaseAdvanceCategoryId = base.Id LEFT OUTER JOIN
                                             dbo.ApprovalTicket AS ticket ON ticket.AdvanceRequisitionHeaderId = header.Id LEFT OUTER JOIN
                                             IDCOLMIS.dbo.UserTable AS u ON header.RequesterUserName = u.UserName LEFT OUTER JOIN
                                             IDCOLMIS.dbo.Admin_Departments AS dept ON u.DepartmentID = dept.DepartmentID LEFT OUTER JOIN
                                             IDCOLMIS.dbo.Admin_Rank AS rank ON u.RankID = rank.RankID
                    GROUP BY header.Id, u.UserName, u.FirstName, u.MiddleName, u.LastName, header.AdvanceCategoryId, category.Name, category.IsCeilingApplicable, category.CeilingAmount, header.FromDate, header.ToDate, 
                                             header.Currency, header.ConversionRate, header.NoOfDays, header.Purpose, header.ExpenseEntryDate, header.AdvanceExpenseStatusId, status.Name, header.CreatedBy, header.CreatedOn, 
                                             header.ApprovedBy, header.ApprovedOn, header.IsDeleted, header.RecommendedBy, header.RecommendedOn, dept.DepartmentID, dept.DepartmentName, rank.RankID, rank.RankName, ticket.Id, 
                                             ticket.ApprovalStatusId   

                    GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetAdvanceExpense')
                    DROP VIEW Advance_VW_GetAdvanceExpense");
        }
    }
}

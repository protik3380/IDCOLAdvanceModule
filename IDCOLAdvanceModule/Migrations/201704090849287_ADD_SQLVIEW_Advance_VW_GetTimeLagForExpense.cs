namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_SQLVIEW_Advance_VW_GetTimeLagForExpense : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetTimeLagForExpense]
                AS
                SELECT NEWID() as Id,
                panel.Id PanelId,
                panel.Name PanelName,
                level.Id LevelId,
                level.Name LevelName,
				expense.Id ExpenseId,
				expense.ExpenseNo,
				expense.AdvanceCategoryId,
				category.Name CategoryName,
                IDCOLMIS.dbo.GetWorkingDaysCount(ticket.AuthorizedOn, GETDATE()) AS PendingDays,
				CASE WHEN expense.AdvanceCategoryId = 2 THEN SUM(detail.AdvanceAmount * detail.ConversionRate)
				ELSE (SUM(detail.AdvanceAmount) * expense.ConversionRate) 
				END AS AdvanceAmountInBDT,
				CASE WHEN expense.AdvanceCategoryId=2 THEN ISNULL(SUM(detail.ExpenseAmount * detail.ConversionRate), 0)
				ELSE ISNULL(SUM(detail.ExpenseAmount * expense.ConversionRate), 0) 
				END AS ExpenseAmountInBDT,
				u.UserName as RequesterUserName,
				u.EmployeeID,
                (u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName
                FROM IDCOLAdvanceDB.dbo.ApprovalPanel panel
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalLevel level
                ON level.ApprovalPanelId = panel.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalTicket ticket
                ON ticket.ApprovalLevelId = level.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceExpenseHeader expense
                ON expense.Id = ticket.AdvanceExpenseHeaderId
				LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceExpenseDetail detail
				ON detail.AdvanceExpenseHeaderId = expense.Id
				LEFT JOIN IDCOLMIS.dbo.UserTable u
				ON expense.RequesterUserName = u.UserName
				LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceCategory category
				ON category.Id = expense.AdvanceCategoryId
				GROUP BY
				panel.Id,
				panel.Name,
                level.Id,
                level.Name,
				expense.Id,
				expense.ExpenseNo,
				ticket.AuthorizedOn,
				expense.AdvanceCategoryId,
				category.Name,
				expense.ConversionRate,
				ticket.TicketTypeId,
				ticket.ApprovalStatusId,
				u.UserName,
				u.EmployeeID,
				u.FirstName,
				u.MiddleName,
				u.LastName
                HAVING ticket.TicketTypeId = 2 AND ticket.ApprovalStatusId = 3

                GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetTimeLagForExpense')
                    DROP VIEW Advance_VW_GetTimeLagForExpense");
        }
    }
}

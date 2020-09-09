namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_TimeLag : DbMigration
    {
        public override void Up()
        {

            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetTimeLagForRequisition]
                AS
                SELECT NEWID() as Id,
                panel.Id PanelId,
                panel.Name PanelName,
                level.Id LevelId,
                level.Name LevelName,
				requisition.Id RequisitionId,
				requisition.RequisitionNo,
				requisition.AdvanceCategoryId,
				category.Name CategoryName,
                (IDCOLMIS.dbo.GetWorkingDaysCount(ticket.AuthorizedOn, GETDATE()) + 1) AS PendingDays,
				CASE WHEN requisition.AdvanceCategoryId = 2 THEN SUM(detail.AdvanceAmount * detail.ConversionRate)
				ELSE (SUM(detail.AdvanceAmount) * requisition.ConversionRate) 
				END AS AdvanceAmountInBDT,
				u.UserName as RequesterUserName,
				u.EmployeeID,
                (u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName
                FROM IDCOLAdvanceDB.dbo.ApprovalPanel panel
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalLevel level
                ON level.ApprovalPanelId = panel.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalTicket ticket
                ON ticket.ApprovalLevelId = level.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader requisition
                ON requisition.Id = ticket.AdvanceRequisitionHeaderId
				LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionDetail detail
				ON detail.AdvanceRequisitionHeaderId = requisition.Id
				LEFT JOIN IDCOLMIS.dbo.UserTable u
				ON requisition.RequesterUserName = u.UserName
				LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceCategory category
				ON category.Id = requisition.AdvanceCategoryId
				GROUP BY
				panel.Id,
				panel.Name,
                level.Id,
                level.Name,
				requisition.Id,
				requisition.RequisitionNo,
				ticket.AuthorizedOn,
				requisition.AdvanceCategoryId,
				category.Name,
				requisition.ConversionRate,
				ticket.TicketTypeId,
				ticket.ApprovalStatusId,
				u.UserName,
				u.EmployeeID,
				u.FirstName,
				u.MiddleName,
				u.LastName
                HAVING ticket.TicketTypeId = 1 AND ticket.ApprovalStatusId = 3

                GO");

            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetTimeLagForExpense]
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
                (IDCOLMIS.dbo.GetWorkingDaysCount(ticket.AuthorizedOn, GETDATE()) + 1) AS PendingDays,
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
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetTimeLagForRequisition]
                AS
                SELECT NEWID() as Id,
                panel.Id PanelId,
                panel.Name PanelName,
                level.Id LevelId,
                level.Name LevelName,
				requisition.Id RequisitionId,
				requisition.RequisitionNo,
				requisition.AdvanceCategoryId,
				category.Name CategoryName,
                IDCOLMIS.dbo.GetWorkingDaysCount(ticket.AuthorizedOn, GETDATE()) AS PendingDays,
				CASE WHEN requisition.AdvanceCategoryId = 2 THEN SUM(detail.AdvanceAmount * detail.ConversionRate)
				ELSE (SUM(detail.AdvanceAmount) * requisition.ConversionRate) 
				END AS AdvanceAmountInBDT,
				u.UserName as RequesterUserName,
				u.EmployeeID,
                (u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName
                FROM IDCOLAdvanceDB.dbo.ApprovalPanel panel
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalLevel level
                ON level.ApprovalPanelId = panel.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalTicket ticket
                ON ticket.ApprovalLevelId = level.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader requisition
                ON requisition.Id = ticket.AdvanceRequisitionHeaderId
				LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionDetail detail
				ON detail.AdvanceRequisitionHeaderId = requisition.Id
				LEFT JOIN IDCOLMIS.dbo.UserTable u
				ON requisition.RequesterUserName = u.UserName
				LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceCategory category
				ON category.Id = requisition.AdvanceCategoryId
				GROUP BY
				panel.Id,
				panel.Name,
                level.Id,
                level.Name,
				requisition.Id,
				requisition.RequisitionNo,
				ticket.AuthorizedOn,
				requisition.AdvanceCategoryId,
				category.Name,
				requisition.ConversionRate,
				ticket.TicketTypeId,
				ticket.ApprovalStatusId,
				u.UserName,
				u.EmployeeID,
				u.FirstName,
				u.MiddleName,
				u.LastName
                HAVING ticket.TicketTypeId = 1 AND ticket.ApprovalStatusId = 3

                GO");

            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetTimeLagForExpense]
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
    }
}

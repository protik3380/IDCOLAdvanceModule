namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_Advance_VW_GetTimeLagForRequisition_AdvanceAmountInBdt_Added : DbMigration
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
                IDCOLMIS.dbo.GetWorkingDaysCount(ticket.AuthorizedOn, GETDATE()) AS PendingDays,
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
				LEFT JOIN IDCOLMIS.dbo.UserTable u
				ON requisition.RequesterUserName = u.UserName
                WHERE ticket.TicketTypeId = 1 AND ticket.ApprovalStatusId = 3

                GO");
        }
    }
}

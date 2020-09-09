namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_Advance_VW_GetTimeLagForRequisition_RequisitionNo_Added : DbMigration
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
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetTimeLagForRequisition]
                AS
                SELECT NEWID() as Id,
                panel.Id PanelId,
                panel.Name PanelName,
                level.Id LevelId,
                level.Name LevelName,
                IDCOLMIS.dbo.GetWorkingDaysCount(ticket.AuthorizedOn, GETDATE()) AS PendingDays
                FROM IDCOLAdvanceDB.dbo.ApprovalPanel panel
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalLevel level
                ON level.ApprovalPanelId = panel.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.ApprovalTicket ticket
                ON ticket.ApprovalLevelId = level.Id
                LEFT JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader requisition
                ON requisition.Id = ticket.AdvanceRequisitionHeaderId
                WHERE ticket.TicketTypeId = 1 AND ticket.ApprovalStatusId = 3

                GO");
        }
    }
}

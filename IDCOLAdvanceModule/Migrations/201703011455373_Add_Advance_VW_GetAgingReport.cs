namespace IDCOLAdvanceModule.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Add_Advance_VW_GetAgingReport : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Advance_VW_GetAgingReport

                AS

                SELECT NEWID() as Id,reqHeader.Id as RequisitionID,reqDetail.Id as RequisitionDetailId,u.EmployeeID,(u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName
                ,empRank.RankName,empRank.RankID,dept.DepartmentID,dept.DepartmentName,
                SUM(reqDetail.AdvanceAmount) as AdvanceAmount,reqHeader.RequisitionDate as AdvanceIssueDate,
                baseCat.Id as BaseAdvanceCategoryId,baseCat.Name as BaseAdvanceCategoryName,
                cat.Id as AdvanceCategoryId,cat.Name as AdvanceCategoryName,
                SUM(expDetail.ExpenseAmount) as ExpenseAmount,
                (SUM(reqDetail.AdvanceAmount)*reqHeader.ConversionRate) as AdvanceAmountInBDT,
                reqHeader.ConversionRate as RequisitionConversionRate,reqHeader.Currency as RequisitionCurrency
                ,(SUM(expDetail.ExpenseAmount)*expHeader.ConversionRate) as ExpenseAmountInBDT,
                expHeader.ConversionRate as ExpenseConversionRate,expHeader.Currency as ExpenseCurrency
                ,reqStatus.Name as RequisitionStatus,reqHeader.ApprovedOn as RequisitionApprovalDate,
                 expHeader.ApprovedOn as ExpenseApprovalDate,appLevel.Name as ApprovalLevelName 
                 ,reqStatus.Id as RequisitionStatusId
 
                FROM AdvanceRequisitionDetail reqDetail

                LEFT OUTER JOIN AdvanceRequisitionHeader reqHeader ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
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

                GROUP BY u.EmployeeID,u.FirstName,u.MiddleName,u.LastName,empRank.RankName,empRank.RankID,
                dept.DepartmentID,dept.DepartmentName,reqHeader.RequisitionDate,reqHeader.Id
                ,baseCat.Id,baseCat.Name,
                cat.Id,cat.Name,expDetail.ExpenseAmount,reqHeader.ConversionRate,
                reqHeader.Currency,expHeader.ConversionRate,expHeader.Currency,reqStatus.Name,
                reqHeader.ApprovedOn,expHeader.ApprovedOn,appLevel.Name,reqStatus.Id,reqDetail.Id");
        }

        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetAgingReport')
                    DROP VIEW Advance_VW_GetAgingReport");
        }
    }
}

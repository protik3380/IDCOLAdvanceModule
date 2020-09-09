namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_SQLVIEW_Advance_VW_GetLocalTravelReport : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Advance_VW_GetLocalTravelReport
                AS

                SELECT NEWID() Id,
                employee.EmployeeID EmployeeId,
                employee.UserName EmployeeUserName, 
                employee.FirstName + ' ' + employee.MiddleName + ' ' + employee.LastName AS EmployeeName,
                reqHeader.Id RequisitionId,
                reqHeader.RequisitionNo, 
                reqHeader.Purpose RequisitionPurpose,
                reqHeader.PlaceOfVisit RequisitionPlace,
                reqHeader.FromDate RequisitionFromDate,
                reqHeader.ToDate RequisitionToDate,
                reqHeader.AdvanceIssueDate,
                SUM(reqDetail.AdvanceAmount * reqHeader.ConversionRate) AS AdvanceAmount,
                expHeader.Id ExpenseId,
                expHeader.ExpenseNo, 
                expHeader.Purpose ExpensePurpose,
                expHeader.PlaceOfVisit ExpensePlace,
                expHeader.FromDate ExpenseFromDate,
                expHeader.ToDate ExpenseToDate,
                expHeader.ExpenseIssueDate,
                SUM(expDetail.ExpenseAmount * expHeader.ConversionRate) AS ExpenseAmount

                FROM AdvanceRequisitionHeader reqHeader
                LEFT JOIN AdvanceRequisitionDetail reqDetail ON reqDetail.AdvanceRequisitionHeaderId = reqHeader.Id
                LEFT JOIN AdvanceCategory category ON category.Id = reqHeader.AdvanceCategoryId
                LEFT JOIN AdvanceExpenseHeader expHeader ON expHeader.Id = reqHeader.AdvanceExpenseHeaderId
                LEFT JOIN AdvanceExpenseDetail expDetail ON expDetail.AdvanceExpenseHeaderId = expHeader.Id
                LEFT OUTER JOIN IDCOLMIS.dbo.UserTable employee ON employee.UserName = reqHeader.RequesterUserName

                GROUP BY
                employee.EmployeeID,
                employee.UserName,
                employee.FirstName,
                employee.MiddleName,
                employee.LastName,
                reqHeader.Id,
                reqHeader.RequisitionNo,
                reqHeader.Purpose,
                reqHeader.PlaceOfVisit,
                reqHeader.FromDate,
                reqHeader.ToDate,
                reqHeader.AdvanceIssueDate,
                reqHeader.AdvanceCategoryId,
                expHeader.Id,
                expHeader.ExpenseNo,
                expHeader.Purpose,
                expHeader.PlaceOfVisit,
                expHeader.FromDate,
                expHeader.ToDate,
                expHeader.ExpenseIssueDate,
                expHeader.AdvanceCategoryId

                HAVING reqHeader.AdvanceCategoryId = 1");
        }

        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetLocalTravelReport')
                    DROP VIEW Advance_VW_GetLocalTravelReport");
        }
    }
}

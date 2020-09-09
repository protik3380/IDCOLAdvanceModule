namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_SQLVIEW_Advance_VW_GetAdvanceExpenseDetail : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [dbo].[Advance_VW_GetAdvanceExpenseDetail]
                    AS
                    SELECT NEWID() AS Id, 
					u.UserName AS RequesterUserName, 
					u.EmployeeID,
					(u.FirstName + ' ' + u.MiddleName + ' ' + u.LastName) AS EmployeeName, 
					header.Id AS HeaderId, 
					header.ExpenseNo,
					ticket.Id AS ExpenseApprovalTicketId, 
                    ticket.ApprovalStatusId, 
					ticket.AuthorizedOn,
                    header.AdvanceCategoryId, 
                    category.Name AS AdvanceCategoryName, 
                    category.IsCeilingApplicable, 
                    category.CeilingAmount, 
                    header.FromDate HeaderFromDate, 
                    header.ToDate HeaderToDate, 
                    header.Currency HeaderCurrency,
                    header.ConversionRate HeaderConversionRate, 
                    header.NoOfDays, 
                    header.Purpose HeaderPurpose, 
                    header.ExpenseEntryDate, 
					header.IsSponsorFinanced,
					header.IsOverseasSponsorFinanced,
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
                    header.IsPaid,
					header.PaidBy,
					header.ExpenseIssueDate,
					detail.NoOfUnit,
					ISNULL(detail.UnitCost, 0) as UnitCost,
					detail.Purpose DetailPurpose,
					ISNULL(detail.AdvanceAmount, 0) as AdvanceAmount,
					CASE WHEN header.AdvanceCategoryId = 2 THEN (detail.AdvanceAmount * detail.ConversionRate)
					ELSE (detail.AdvanceAmount * header.ConversionRate) 
					END AS AdvanceAmountInBDT,
					ISNULL(detail.ExpenseAmount, 0) as ExpenseAmount,
					CASE WHEN header.AdvanceCategoryId = 2 THEN (detail.ExpenseAmount * detail.ConversionRate)
					ELSE (detail.ExpenseAmount * header.ConversionRate) 
					END AS ExpenseAmountInBDT,
					detail.Remarks DetailRemarks,
					detail.TravelCostItemId,
					detail.FromDate DetailFromDate,
					detail.ToDate DetailToDate,
					detail.OverseasTravelCostItemId,
					detail.OverseasFromDate,
					detail.OverseasToDate,
					detail.ReceipientOrPayeeName,
					detail.IsThirdPartyReceipient,
					ISNULL(detail.OverseasSponsorFinancedDetailAmount, 0) as OverseasSponsorFinancedDetailAmount,
					detail.Currency DetailCurrency,
					detail.ConversionRate DetailConversionRate,
					detail.VatTaxTypeId,
					vat.Name VatTaxTypeName,
					detail.ExpenseVoucherDetailId
	                
					FROM dbo.AdvanceExpenseDetail AS detail 
					LEFT OUTER JOIN dbo.AdvanceExpenseHeader AS header 
					ON detail.AdvanceExpenseHeaderId = header.Id 
					LEFT OUTER JOIN dbo.AdvanceCategory AS category 
					ON header.AdvanceCategoryId = category.Id 
					LEFT OUTER JOIN dbo.AdvanceStatus AS status 
					ON header.AdvanceExpenseStatusId = status.Id 
					LEFT OUTER JOIN dbo.BaseAdvanceCategory AS base 
					ON category.BaseAdvanceCategoryId = base.Id 
					LEFT OUTER JOIN dbo.ApprovalTicket AS ticket 
					ON ticket.AdvanceExpenseHeaderId = header.Id 
					LEFT OUTER JOIN IDCOLMIS.dbo.UserTable AS u 
					ON header.RequesterUserName = u.UserName 
					LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments AS dept 
					ON u.DepartmentID = dept.DepartmentID 
					LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank AS rank 
					ON u.RankID = rank.RankID 
					LEFT OUTER JOIN IDCOLAdvanceDB.dbo.VatTaxType vat 
					ON detail.VatTaxTypeId = vat.Id 
					LEFT OUTER JOIN IDCOLAdvanceDB.dbo.PlaceOfVisit place 
					ON header.PlaceOfVisitId = place.Id
                    
					GROUP BY 
					header.Id,
					u.UserName, 
					u.EmployeeID,
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
					ticket.AuthorizedOn,
					header.IsSponsorFinanced,
					header.IsOverseasSponsorFinanced,
					detail.Id,
					detail.NoOfUnit,
					detail.UnitCost,
					detail.Purpose,
					detail.AdvanceAmount,
					detail.ExpenseAmount,
					detail.Remarks,
					detail.TravelCostItemId,
					detail.FromDate,
					detail.ToDate,
					detail.OverseasTravelCostItemId,
					detail.OverseasFromDate,
					detail.OverseasToDate,
					detail.ReceipientOrPayeeName,
					detail.IsThirdPartyReceipient,
					detail.OverseasSponsorFinancedDetailAmount,
					detail.Currency,
					detail.ConversionRate,
					detail.VatTaxTypeId,
					vat.Name,
					detail.ExpenseVoucherDetailId	


            GO");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT 1 FROM sys.views
                    WHERE Name = 'Advance_VW_GetAdvanceExpenseDetail')
                    DROP VIEW Advance_VW_GetAdvanceExpenseDetail");
        }
    }
}

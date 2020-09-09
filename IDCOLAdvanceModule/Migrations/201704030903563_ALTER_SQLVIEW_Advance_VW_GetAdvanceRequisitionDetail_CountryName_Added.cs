namespace IDCOLAdvanceModule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALTER_SQLVIEW_Advance_VW_GetAdvanceRequisitionDetail_CountryName_Added : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAdvanceRequisitionDetail]
                    AS
                    SELECT NEWID() as Id,
                    u.UserName as RequesterUserName,
					u.EmployeeID,
                    (u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName,
                    header.Id HeaderId,
					header.RequisitionNo, 
                    ticket.Id RequistionApprovalTicketId,
					ticket.ApprovalStatusId,
					ticket.AuthorizedOn,
                    header.AdvanceCategoryId, 
					category.BaseAdvanceCategoryId,
                    category.Name RequisitionCategoryName, 
                    category.IsCeilingApplicable,
                    category.CeilingAmount,
                    header.FromDate HeaderFromDate,
                    header.ToDate HeaderToDate,
                    header.Currency HeaderCurrency,
                    header.ConversionRate HeaderConversionRate,
                    header.NoOfDays,
                    header.Purpose HeaderPurpose,
                    header.RequisitionDate,
                    header.IsSponsorFinanced,
                    header.SponsorName, 
                    header.AdvanceRequisitionStatusId,
                    status.Name as RequisitionStatusName,
                    header.CreatedBy,
                    header.CreatedOn,
                    header.ApprovedBy,
                    header.ApprovedOn,  
                    header.IsDeleted,  
                    header.RecommendedBy,
                    header.RecommendedOn,   
                    header.PlaceOfEvent,
                    header.PlaceOfVisit,
					header.CountryName, 
                    header.SourceOfFund,
                    dept.DepartmentID EmployeeDepartmentID,
                    dept.DepartmentName EmployeeDepartmentName,
                    rank.RankID EmployeeRankID,
                    rank.RankName EmployeeRankName,
					header.IsPaid,
					header.PaidBy,
					header.AdvanceIssueDate,
					header.AdvanceExpenseHeaderId,
					detail.NoOfUnit,
					ISNULL(detail.UnitCost, 0) as UnitCost,
					detail.Purpose DetailPurpose,
					ISNULL(detail.AdvanceAmount, 0) as AdvanceAmount,
					CASE WHEN header.AdvanceCategoryId = 2 THEN (detail.AdvanceAmount * detail.ConversionRate)
					ELSE (detail.AdvanceAmount * header.ConversionRate) 
					END AS AdvanceAmountInBDT,
					detail.Remarks DetailRemarks,
					detail.MiscelleneousCostItemId,
					detail.TravelCostItemId,
					detail.FromDate DetailFromDate,
					detail.ToDate DetailToDate,
					detail.OverseasTravelCostItemId,
					detail.OverseasFromDate,
					detail.OverseasToDate,
					detail.ReceipientOrPayeeName,
					detail.IsThirdPartyReceipient,
					ISNULL(detail.OverseasSponsorFinancedDetailAmount, 0) as OverseasSponsorFinancedDetailAmount,
					ISNULL(detail.TravelSponsorFinancedDetailAmount, 0) as TravelSponsorFinancedDetailAmount,
					detail.Currency DetailCurrency,
					detail.ConversionRate DetailConversionRate,
					detail.VatTaxTypeId,
					vat.Name VatTaxTypeName,
					detail.RequisitionVoucherDetailId

                    FROM IDCOLAdvanceDB.dbo.AdvanceRequisitionDetail detail
					LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader header
                    ON detail.AdvanceRequisitionHeaderId = header.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceCategory category
                    ON header.AdvanceCategoryId = category.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceStatus status
                    ON header.AdvanceRequisitionStatusId = status.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.BaseAdvanceCategory base
                    ON category.BaseAdvanceCategoryId = base.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.CostItem cost
                    ON detail.TravelCostItemId = cost.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.ApprovalTicket ticket 
                    ON ticket.AdvanceRequisitionHeaderId = header.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u
                    ON header.RequesterUserName = u.UserName
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept
                    ON u.DepartmentID = dept.DepartmentID
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank
                    ON u.RankID = rank.RankID
					LEFT OUTER JOIN IDCOLAdvanceDB.dbo.VatTaxType vat
					ON detail.VatTaxTypeId = vat.Id                
                
                    Group By 
                    header.Id,
                    u.UserName,
					u.EmployeeID,
                    u.FirstName,
                    u.MiddleName,
                    u.LastName,
					header.RequisitionNo,
                    header.AdvanceCategoryId,
					category.BaseAdvanceCategoryId,
                    category.Name, 
                    category.IsCeilingApplicable,
                    category.CeilingAmount,
                    header.FromDate,
                    header.ToDate,
                    header.Currency,
                    header.ConversionRate,
                    header.NoOfDays,
                    header.Purpose,
                    header.RequisitionDate,
                    header.IsSponsorFinanced,
                    header.SponsorName, 
                    header.AdvanceRequisitionStatusId,
                    status.Name,
                    header.CreatedBy,
                    header.CreatedOn,
                    header.ApprovedBy,
                    header.ApprovedOn,  
                    header.IsDeleted,  
                    header.RecommendedBy,
                    header.RecommendedOn,   
                    header.PlaceOfEvent,
                    header.PlaceOfVisit, 
					header.CountryName,
                    header.SourceOfFund,
                    header.SponsorName, 
                    dept.DepartmentID,
                    dept.DepartmentName,
                    rank.RankID,
                    rank.RankName,
                    ticket.Id,
					ticket.ApprovalStatusId,
					header.IsPaid,
					header.PaidBy,
					header.AdvanceIssueDate,
					ticket.AuthorizedOn,
					header.AdvanceExpenseHeaderId,
					detail.Id,
					detail.NoOfUnit,
					detail.UnitCost,
					detail.Purpose,
					detail.AdvanceAmount,
					detail.Remarks,
					detail.MiscelleneousCostItemId,
					detail.TravelCostItemId,
					detail.FromDate,
					detail.ToDate,
					detail.OverseasTravelCostItemId,
					detail.OverseasFromDate,
					detail.OverseasToDate,
					detail.ReceipientOrPayeeName,
					detail.IsThirdPartyReceipient,
					detail.OverseasSponsorFinancedDetailAmount,
					detail.TravelSponsorFinancedDetailAmount,
					detail.Currency,
					detail.ConversionRate,
					detail.VatTaxTypeId,
					vat.Name,
					detail.RequisitionVoucherDetailId					

                GO");
        }
        
        public override void Down()
        {
            Sql(@"ALTER VIEW [dbo].[Advance_VW_GetAdvanceRequisitionDetail]
                    AS
                    SELECT NEWID() as Id,
                    u.UserName as RequesterUserName,
					u.EmployeeID,
                    (u.FirstName+' '+u.MiddleName+' '+u.LastName) as EmployeeName,
                    header.Id HeaderId,
					header.RequisitionNo, 
                    ticket.Id RequistionApprovalTicketId,
					ticket.ApprovalStatusId,
					ticket.AuthorizedOn,
                    header.AdvanceCategoryId, 
					category.BaseAdvanceCategoryId,
                    category.Name RequisitionCategoryName, 
                    category.IsCeilingApplicable,
                    category.CeilingAmount,
                    header.FromDate HeaderFromDate,
                    header.ToDate HeaderToDate,
                    header.Currency HeaderCurrency,
                    header.ConversionRate HeaderConversionRate,
                    header.NoOfDays,
                    header.Purpose HeaderPurpose,
                    header.RequisitionDate,
                    header.IsSponsorFinanced,
                    header.SponsorName, 
                    header.AdvanceRequisitionStatusId,
                    status.Name as RequisitionStatusName,
                    header.CreatedBy,
                    header.CreatedOn,
                    header.ApprovedBy,
                    header.ApprovedOn,  
                    header.IsDeleted,  
                    header.RecommendedBy,
                    header.RecommendedOn,   
                    header.PlaceOfEvent,
                    header.PlaceOfVisit, 
                    header.SourceOfFund,
                    dept.DepartmentID EmployeeDepartmentID,
                    dept.DepartmentName EmployeeDepartmentName,
                    rank.RankID EmployeeRankID,
                    rank.RankName EmployeeRankName,
					header.IsPaid,
					header.PaidBy,
					header.AdvanceIssueDate,
					header.AdvanceExpenseHeaderId,
					detail.NoOfUnit,
					ISNULL(detail.UnitCost, 0) as UnitCost,
					detail.Purpose DetailPurpose,
					ISNULL(detail.AdvanceAmount, 0) as AdvanceAmount,
					CASE WHEN header.AdvanceCategoryId = 2 THEN (detail.AdvanceAmount * detail.ConversionRate)
					ELSE (detail.AdvanceAmount * header.ConversionRate) 
					END AS AdvanceAmountInBDT,
					detail.Remarks DetailRemarks,
					detail.MiscelleneousCostItemId,
					detail.TravelCostItemId,
					detail.FromDate DetailFromDate,
					detail.ToDate DetailToDate,
					detail.OverseasTravelCostItemId,
					detail.OverseasFromDate,
					detail.OverseasToDate,
					detail.ReceipientOrPayeeName,
					detail.IsThirdPartyReceipient,
					ISNULL(detail.OverseasSponsorFinancedDetailAmount, 0) as OverseasSponsorFinancedDetailAmount,
					ISNULL(detail.TravelSponsorFinancedDetailAmount, 0) as TravelSponsorFinancedDetailAmount,
					detail.Currency DetailCurrency,
					detail.ConversionRate DetailConversionRate,
					detail.VatTaxTypeId,
					vat.Name VatTaxTypeName,
					detail.RequisitionVoucherDetailId

                    FROM IDCOLAdvanceDB.dbo.AdvanceRequisitionDetail detail
					LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceRequisitionHeader header
                    ON detail.AdvanceRequisitionHeaderId = header.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceCategory category
                    ON header.AdvanceCategoryId = category.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.AdvanceStatus status
                    ON header.AdvanceRequisitionStatusId = status.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.BaseAdvanceCategory base
                    ON category.BaseAdvanceCategoryId = base.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.CostItem cost
                    ON detail.TravelCostItemId = cost.Id
                    LEFT OUTER JOIN IDCOLAdvanceDB.dbo.ApprovalTicket ticket 
                    ON ticket.AdvanceRequisitionHeaderId = header.Id
                    LEFT OUTER JOIN IDCOLMIS.dbo.UserTable u
                    ON header.RequesterUserName = u.UserName
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Departments dept
                    ON u.DepartmentID = dept.DepartmentID
                    LEFT OUTER JOIN IDCOLMIS.dbo.Admin_Rank rank
                    ON u.RankID = rank.RankID
					LEFT OUTER JOIN IDCOLAdvanceDB.dbo.VatTaxType vat
					ON detail.VatTaxTypeId = vat.Id                
                
                    Group By 
                    header.Id,
                    u.UserName,
					u.EmployeeID,
                    u.FirstName,
                    u.MiddleName,
                    u.LastName,
					header.RequisitionNo,
                    header.AdvanceCategoryId,
					category.BaseAdvanceCategoryId,
                    category.Name, 
                    category.IsCeilingApplicable,
                    category.CeilingAmount,
                    header.FromDate,
                    header.ToDate,
                    header.Currency,
                    header.ConversionRate,
                    header.NoOfDays,
                    header.Purpose,
                    header.RequisitionDate,
                    header.IsSponsorFinanced,
                    header.SponsorName, 
                    header.AdvanceRequisitionStatusId,
                    status.Name,
                    header.CreatedBy,
                    header.CreatedOn,
                    header.ApprovedBy,
                    header.ApprovedOn,  
                    header.IsDeleted,  
                    header.RecommendedBy,
                    header.RecommendedOn,   
                    header.PlaceOfEvent,
                    header.PlaceOfVisit, 
                    header.SourceOfFund,
                    header.SponsorName, 
                    dept.DepartmentID,
                    dept.DepartmentName,
                    rank.RankID,
                    rank.RankName,
                    ticket.Id,
					ticket.ApprovalStatusId,
					header.IsPaid,
					header.PaidBy,
					header.AdvanceIssueDate,
					ticket.AuthorizedOn,
					header.AdvanceExpenseHeaderId,
					detail.Id,
					detail.NoOfUnit,
					detail.UnitCost,
					detail.Purpose,
					detail.AdvanceAmount,
					detail.Remarks,
					detail.MiscelleneousCostItemId,
					detail.TravelCostItemId,
					detail.FromDate,
					detail.ToDate,
					detail.OverseasTravelCostItemId,
					detail.OverseasFromDate,
					detail.OverseasToDate,
					detail.ReceipientOrPayeeName,
					detail.IsThirdPartyReceipient,
					detail.OverseasSponsorFinancedDetailAmount,
					detail.TravelSponsorFinancedDetailAmount,
					detail.Currency,
					detail.ConversionRate,
					detail.VatTaxTypeId,
					vat.Name,
					detail.RequisitionVoucherDetailId					

            GO");
        }
    }
}

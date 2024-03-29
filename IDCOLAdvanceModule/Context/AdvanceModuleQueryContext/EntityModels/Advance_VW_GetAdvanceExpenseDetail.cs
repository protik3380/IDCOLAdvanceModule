//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IDCOLAdvanceModule.Context.AdvanceModuleQueryContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Advance_VW_GetAdvanceExpenseDetail
    {
        public Nullable<System.Guid> Id { get; set; }
        public string RequesterUserName { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<long> HeaderId { get; set; }
        public string ExpenseNo { get; set; }
        public Nullable<long> ExpenseApprovalTicketId { get; set; }
        public Nullable<long> ApprovalStatusId { get; set; }
        public Nullable<System.DateTime> AuthorizedOn { get; set; }
        public Nullable<long> AdvanceCategoryId { get; set; }
        public string AdvanceCategoryName { get; set; }
        public Nullable<bool> IsCeilingApplicable { get; set; }
        public Nullable<decimal> CeilingAmount { get; set; }
        public Nullable<System.DateTime> HeaderFromDate { get; set; }
        public Nullable<System.DateTime> HeaderToDate { get; set; }
        public string HeaderCurrency { get; set; }
        public Nullable<double> HeaderConversionRate { get; set; }
        public Nullable<double> NoOfDays { get; set; }
        public string HeaderPurpose { get; set; }
        public Nullable<System.DateTime> ExpenseEntryDate { get; set; }
        public Nullable<bool> IsSponsorFinanced { get; set; }
        public Nullable<bool> IsOverseasSponsorFinanced { get; set; }
        public string SponsorName { get; set; }
        public string OverseasSponsorName { get; set; }
        public Nullable<long> AdvanceExpenseStatusId { get; set; }
        public string AdvanceExpenseStatusName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedOn { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string RecommendedBy { get; set; }
        public Nullable<System.DateTime> RecommendedOn { get; set; }
        public string PlaceOfEvent { get; set; }
        public string CorporateAdvisoryPlaceOfEvent { get; set; }
        public string PlaceOfVisit { get; set; }
        public Nullable<long> OverseasPlaceOfVisitId { get; set; }
        public string OverseasPlaceOfVisit { get; set; }
        public string CountryName { get; set; }
        public string SourceOfFund { get; set; }
        public Nullable<decimal> EmployeeDepartmentID { get; set; }
        public string EmployeeDepartmentName { get; set; }
        public Nullable<decimal> EmployeeRankID { get; set; }
        public string EmployeeRankName { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public string PaidBy { get; set; }
        public Nullable<System.DateTime> ExpenseIssueDate { get; set; }
        public Nullable<double> HeaderNoOfUnit { get; set; }
        public Nullable<decimal> HeaderUnitCost { get; set; }
        public Nullable<decimal> TotalRevenue { get; set; }
        public string AdvanceCorporateRemarks { get; set; }
        public Nullable<double> NoOfUnit { get; set; }
        public decimal UnitCost { get; set; }
        public string DetailPurpose { get; set; }
        public decimal AdvanceAmount { get; set; }
        public Nullable<double> AdvanceAmountInBDT { get; set; }
        public decimal ExpenseAmount { get; set; }
        public Nullable<double> ExpenseAmountInBDT { get; set; }
        public string DetailRemarks { get; set; }
        public Nullable<long> TravelCostItemId { get; set; }
        public Nullable<System.DateTime> DetailFromDate { get; set; }
        public Nullable<System.DateTime> DetailToDate { get; set; }
        public Nullable<long> OverseasTravelCostItemId { get; set; }
        public Nullable<System.DateTime> OverseasFromDate { get; set; }
        public Nullable<System.DateTime> OverseasToDate { get; set; }
        public string ReceipientOrPayeeName { get; set; }
        public bool IsThirdPartyReceipient { get; set; }
        public decimal OverseasSponsorFinancedDetailAmount { get; set; }
        public decimal TravelSponsorFinancedDetailAmount { get; set; }
        public string DetailCurrency { get; set; }
        public Nullable<double> DetailConversionRate { get; set; }
        public Nullable<long> VatTaxTypeId { get; set; }
        public string VatTaxTypeName { get; set; }
        public Nullable<long> ExpenseVoucherDetailId { get; set; }
        public string Justification { get; set; }
    }
}

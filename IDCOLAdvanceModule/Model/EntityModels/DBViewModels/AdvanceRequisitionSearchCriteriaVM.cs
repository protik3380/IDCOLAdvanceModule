
using System;

namespace IDCOLAdvanceModule.Model.EntityModels.DBViewModels
{
    public class AdvanceRequisitionSearchCriteriaVM
    {
        public System.Guid Id { get; set; }
        public long? HeaderId { get; set; }
        public string EmployeeUserName { get; set; }
        public string EmployeeName { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public string Remarks { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public string Purpose { get; set; }
        public long? VendorId { get; set; }
        public bool IsVendorReceipient { get; set; }
        public string Discriminator { get; set; }
        public long? RequisitionCategoryId { get; set; }
        public string RequisitionCategoryName { get; set; }
        public DateTime? RequisitionDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? NoOfDays { get; set; }
        public double? ConversionRate { get; set; }
        public string Currency { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string RecommendedBy { get; set; }
        public long? AdvanceRequisitionStatusId { get; set; }
        public string RequisitionStatusName { get; set; }
        public string VerifiedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string PlaceOfVisit { get; set; }
        public string SourceOfFund { get; set; }
        public bool? IsSponsorFinanced { get; set; }
        public string SponsorName { get; set; }
        public string CostItemName { get; set; }
        public long? TravelCostItemId { get; set; }
        public decimal? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal? RankID { get; set; }
        public string RankName { get; set; }
    }
}

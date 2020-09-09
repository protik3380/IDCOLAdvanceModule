using System;

namespace IDCOLAdvanceModule.Model.SearchModels
{
    public class AdvanceRequisitionSearchCriteria
    {
        public decimal? DepartmentId { get; set; }
        public decimal? RankId { get; set; }
        public string RequesterUserName { get; set; }
        public string CurrencyName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string EmployeeName { get; set; }
        public string Remarks { get; set; }
        public long BaseAdvanceCategoryId { get; set; }
        public long AdvanceCategoryId { get; set; }
    }
}

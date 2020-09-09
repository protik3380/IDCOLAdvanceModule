using System;

namespace IDCOLAdvanceModule.Model.SearchModels
{
    public class ReportSearchCriteria
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal? DepartmentId { get; set; }
        public decimal? SubDepartmentId { get; set; }
        public long? BaseCategoryId { get; set; }
        public long? CategoryId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsAdjusted { get; set; }
        public string RequisitionNo { get; set; }
        public string ExpenseNo { get; set; }
        public long? ApprovalPanelId { get; set; }
        public long? ApprovalLevelId { get; set; }
    }
}

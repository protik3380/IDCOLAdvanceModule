using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public abstract class RequisitionHistoryHeader
    {

        public long Id { get; set; }
        public long AdvanceRequisitionHeaderId { get; set; }
        public string RequesterUserName { get; set; }
        public Decimal? RequesterDepartmentId { get; set; }
        public Decimal? RequesterRankId { get; set; }
        public decimal? RequesterSupervisorId { get; set; }
        public string Purpose { get; set; }
        public long AdvanceCategoryId { get; set; }
        public DateTime RequisitionDate { get; set; }
        public DateTime FromDate { get; set; }
        public double NoOfDays { get; set; }
        public DateTime ToDate { get; set; }
        public string Currency { get; set; }
        public double ConversionRate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long AdvanceRequisitionStatusId { set; get; }
        public string RecommendedBy { get; set; }
        public DateTime? RecommendedOn { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public bool IsSourceOfFundVerified { get; set; }
        public bool IsFundAvailable { get; set; }
        public DateTime? AdvanceIssueDate { get; set; }
        public DateTime? RejectedOn { get; set; }
        public string RejectedBy { get; set; }
        public bool IsPaid { get; set; }
        public string PaidBy { get; set; }
        public string RequisitionNo { get; set; }
        public int SerialNo { get; set; }
        public long HistoryModeId { get; set; }

        public HistoryMode HistoryMode { get; set; }
        public AdvanceCategory AdvanceCategory { get; set; }
        public AdvanceStatus AdvanceRequisitionStatus { set; get; }
        public AdvanceRequisitionHeader AdvanceRequisitionHeader { get; set; }
        public ICollection<RequisitionHistoryDetail> RequisitionHistoryDetails { get; set; }

    }
}

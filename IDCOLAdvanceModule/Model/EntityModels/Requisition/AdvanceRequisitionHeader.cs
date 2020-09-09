using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public abstract class AdvanceRequisitionHeader : IAudit
    {
        public AdvanceRequisitionHeader()
        {
            AdvanceRequisitionStatusId = (long)AdvanceStatusEnum.Draft;
        }
        public long Id { get; set; }
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
        public long? AdvanceExpenseHeaderId { get; set; }
        public DateTime? AdvanceIssueDate { get; set; }
        public DateTime? RejectedOn { get; set; }
        public string RejectedBy { get; set; }
        public bool IsPaid { get; set; }
        public string PaidBy { get; set; }
        public bool IsReceived { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public string RequisitionNo { get; set; }
        public int SerialNo { get; set; }
        public AdvanceCategory AdvanceCategory { get; set; }
        public AdvanceStatus AdvanceRequisitionStatus { set; get; }
        public virtual ICollection<AdvanceRequisitionDetail> AdvanceRequisitionDetails { get; set; }
        public virtual ICollection<RequisitionApprovalTicket> RequisitionApprovalTickets { get; set; }
        public virtual AdvanceExpenseHeader AdvanceExpenseHeader { get; set; }
        //public virtual ICollection<AdvanceExpenseHeader> AdvanceExpenseHeaders { get; set; }
        public virtual ICollection<RequisitionFile> RequisitionFiles { get; set; }
        public virtual ICollection<RequisitionHistoryHeader> RequisitionHistoryHeaders { get; set; }

        public decimal GetTotalAdvanceAmount()
        {
            decimal totalAdvanceAmount = 0;
            if (AdvanceRequisitionDetails != null)
            {
                totalAdvanceAmount = AdvanceRequisitionDetails.Sum(c => c.GetAdvanceAmountInBdt());
            }
            return totalAdvanceAmount;
        }

        public void GenerateRequisitionNo()
        {
            string formatedSerialNo = string.Empty;
            if (SerialNo > 0 && SerialNo < 10)
                formatedSerialNo = "000" + SerialNo;
            else if (SerialNo >= 10 && SerialNo < 100)
                formatedSerialNo = "00" + SerialNo;
            else if (SerialNo >= 100 && SerialNo < 1000)
                formatedSerialNo = "0" + SerialNo;
            else if (SerialNo >= 1000 && SerialNo < 10000)
                formatedSerialNo = SerialNo.ToString();
            RequisitionNo = "R" + CreatedOn.ToString("yyyyMM") + formatedSerialNo;
        }

        public abstract RequisitionHistoryHeader GenerateRequisitionHistoryHeaderFromRequisition(
            AdvanceRequisitionHeader requisitionHeader, HistoryModeEnum historyModeEnum);

        public string GetNotificationMessage(UserTable requesterInfo,ApprovalStatusEnum approvalStatusEnum)
        {
            var messege = string.Empty;
            if (approvalStatusEnum == ApprovalStatusEnum.WaitingForApproval)
            {
                messege = "Requisition#" + RequisitionNo + " Requested By " + requesterInfo.FullName + " is waiting for your approval.";
            }
            else if (approvalStatusEnum == ApprovalStatusEnum.Approved)
            {
                messege = "Requisition#" + RequisitionNo + " is Approved By " + requesterInfo.FullName + ".";
            }
            else if(approvalStatusEnum == ApprovalStatusEnum.Rejected)
            {
                messege = "Requisition#" + RequisitionNo + " is rejected By " + requesterInfo.FullName + ".";   
            }
            else if (approvalStatusEnum == ApprovalStatusEnum.Reverted)
            {
                messege = "Requisition#" + RequisitionNo + " is reverted By " + requesterInfo.FullName + ".";
            }
            return messege;
        }
    }
}

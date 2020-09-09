using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.EntityModels.Expense
{
    public abstract class AdvanceExpenseHeader
    {
        protected AdvanceExpenseHeader()
        {
            AdvanceExpenseStatusId = (long)AdvanceExpenseTypeEnum.New;
        }
        public long Id { get; set; }
        public string RequesterUserName { get; set; }
        public Decimal? RequesterDepartmentId { get; set; }
        public Decimal? RequesterRankId { get; set; }
        public decimal? RequesterSupervisorId { get; set; }
        public string Purpose { get; set; }
        public long AdvanceCategoryId { get; set; }
        public DateTime ExpenseEntryDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double NoOfDays { get; set; }
        public string Currency { get; set; }
        public double ConversionRate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long AdvanceExpenseStatusId { set; get; }
        public string RecommendedBy { get; set; }
        public DateTime? RecommendedOn { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime? RejectedOn { get; set; }
        public string RejectedBy { get; set; }
        public bool IsPaid { get; set; }
        public string PaidBy { get; set; }
        public bool IsReceived { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public DateTime? ExpenseIssueDate { get; set; }
        public bool IsSourceOfEntered { get; set; }
        public bool IsSourceOfFundVerified { get; set; }
        public string ExpenseNo { get; set; }
        public int SerialNo { get; set; }

        public AdvanceCategory AdvanceCategory { get; set; }
        public AdvanceStatus AdvanceExpenseStatus { set; get; }
        public virtual ICollection<AdvanceExpenseDetail> AdvanceExpenseDetails { get; set; }
        public virtual ICollection<ExpenseApprovalTicket> ExpenseApprovalTickets { get; set; }
        public virtual ICollection<AdvanceRequisitionHeader> AdvanceRequisitionHeaders { get; set; }
        public virtual ICollection<ExpenseFile> ExpenseFiles { get; set; }
        //public virtual AdvanceRequisitionHeader AdvanceRequisitionHeader { get; set; }
        public virtual ICollection<ExpenseHistoryHeader>  ExpenseHistoryHeaders { get; set; }

        public decimal GetTotalExpenseAmount()
        {
            decimal totalExpenseAmount = 0;
            if (AdvanceExpenseDetails != null)
            {
                totalExpenseAmount = AdvanceExpenseDetails.Sum(c => c.GetExpenseAmountInBdt());
            }
            return totalExpenseAmount;
        }

        public decimal GetTotalAdvanceAmount()
        {
            decimal totalAdvancAmount = 0;
            if (AdvanceExpenseDetails != null)
            {
                totalAdvancAmount = AdvanceExpenseDetails.Sum(c => c.GetAdvanceAmountInBdt());
            }
            return totalAdvancAmount;
        }

        public decimal GetTotalActualExpenseAmount()
        {
            return GetTotalExpenseAmount() - AdvanceExpenseDetails.Sum(c => c.GetSponsorAmount());
        }

        public decimal GetReimbursementAmount()
        {
            return GetTotalActualExpenseAmount() - GetTotalAdvanceAmount();
        }

        public string GetFormattedReimbursementAmount()
        {
            var reimbursementAmount = GetReimbursementAmount();
            if (reimbursementAmount >= 0)
                return reimbursementAmount.ToString("N");
            return "(" + Math.Abs(reimbursementAmount).ToString("N") + ")";
        }
        public void GenerateExpenseNo()
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
            ExpenseNo = "E" + CreatedOn.ToString("yyyyMM") + formatedSerialNo;
        }
        public abstract ExpenseHistoryHeader GenerateExpenseHistoryHeaderFromExpense(
            AdvanceExpenseHeader expenseHeader, HistoryModeEnum historyModeEnum);
        public string GetMessageForWaitingForApproval(UserTable requesterInfo)
        {
            return "Expense#" + ExpenseNo + " Requested By " + requesterInfo.FullName + " is waiting for your approval.";
        }
    }
}

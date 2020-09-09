using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory
{
    public abstract class ExpenseHistoryHeader
    {
        public long Id { get; set; }
        public long AdvanceExpenseHeaderId { get; set; }
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
        public DateTime? ExpenseIssueDate { get; set; }
        public bool IsSourceOfEntered { get; set; }
        public bool IsSourceOfFundVerified { get; set; }
        public string ExpenseNo { get; set; }
        public int SerialNo { get; set; }
        public long HistoryModeId { get; set; }

        public HistoryMode HistoryMode { get; set; }

        public AdvanceCategory AdvanceCategory { get; set; }
        public AdvanceStatus AdvanceExpenseStatus { set; get; }
        public AdvanceExpenseHeader AdvanceExpenseHeader { get; set; }
        public virtual ICollection<ExpenseHistoryDetail>  ExpenseHistoryDetails{ get; set; }
        
        public decimal GetTotalExpenseAmount()
        {
            decimal totalExpenseAmount = 0;
            if (ExpenseHistoryDetails != null)
            {
                totalExpenseAmount = ExpenseHistoryDetails.Sum(c => c.GetExpenseAmountInBdt());
            }
            return totalExpenseAmount;
        }

        public decimal GetTotalAdvanceAmount()
        {
            decimal totalAdvancAmount = 0;
            if (ExpenseHistoryDetails != null)
            {
                totalAdvancAmount = ExpenseHistoryDetails.Sum(c => c.GetAdvanceAmountInBdt());
            }
            return totalAdvancAmount;
        }

        public decimal GetTotalActualExpenseAmount()
        {
            return GetTotalExpenseAmount() - ExpenseHistoryDetails.Sum(c => c.GetSponsorAmount());
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
    }
}

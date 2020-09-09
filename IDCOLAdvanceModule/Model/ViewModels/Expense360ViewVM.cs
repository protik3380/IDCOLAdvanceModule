using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;

namespace IDCOLAdvanceModule.Model.ViewModels
{
    public class Expense360ViewVM
    {
        public Expense360ViewVM(AdvanceExpenseHeader expenseHeader, EmployeeVM employeeVm)
        {
            AdvanceExpenseHeader = expenseHeader;
            this.employeeVm = employeeVm;

            if (expenseHeader!=null && expenseHeader.AdvanceRequisitionHeaders!=null)
            {
                AdvanceRequisitionHeaders = expenseHeader.AdvanceRequisitionHeaders;
            }
        }

        public AdvanceExpenseHeader AdvanceExpenseHeader { get; set; }
        public ICollection<AdvanceRequisitionHeader> AdvanceRequisitionHeaders { get; set; }

        public List<AdvanceExpenseDetail>  AdvanceExpenseDetails
        {
            get
            {
                if (AdvanceExpenseHeader == null)
                {
                    return new List<AdvanceExpenseDetail>();
                }
                return AdvanceExpenseHeader.AdvanceExpenseDetails.ToList();
            }
        }

        public List<AdvanceRequisitionDetail> AdvanceRequisitionDetails
        {
            get
            {
                if (AdvanceRequisitionHeaders == null)
                {
                    return new List<AdvanceRequisitionDetail>();
                }
                return AdvanceRequisitionHeaders.SelectMany(c=>c.AdvanceRequisitionDetails).ToList();
            }
        }

        public ExpenseApprovalTicket ExpenseApprovalTicket
        {
            get
            {
                var ticket  = new ExpenseApprovalTicket();
                if (AdvanceExpenseHeader == null)
                {
                    return new ExpenseApprovalTicket();
                }

                if (AdvanceExpenseHeader.ExpenseApprovalTickets!=null && AdvanceExpenseHeader.ExpenseApprovalTickets.Any())
                {
                    ticket = AdvanceExpenseHeader.ExpenseApprovalTickets.FirstOrDefault();
                }

                return ticket;
            }
        }

        public IList<ExpenseApprovalTracker> ExpenseApprovalTrackers
        {
            get
            {
                if (ExpenseApprovalTicket == null)
                {
                    return new List<ExpenseApprovalTracker>();
                }
                if (ExpenseApprovalTicket != null && ExpenseApprovalTicket.ExpenseApprovalTrackers == null)
                {
                    return new List<ExpenseApprovalTracker>();
                }
                return ExpenseApprovalTicket.ExpenseApprovalTrackers.ToList();
            }
        } 
             

        //public RequisitionApprovalTicket RequisitionApprovalTicket
        //{
        //    get
        //    {
        //        var ticket = new RequisitionApprovalTicket();
        //        if (AdvanceRequisitionHeader == null)
        //        {
        //            return new RequisitionApprovalTicket();
        //        }

        //        if (AdvanceRequisitionHeader.RequisitionApprovalTickets != null && AdvanceRequisitionHeader.RequisitionApprovalTickets.Any())
        //        {
        //            ticket = AdvanceRequisitionHeader.RequisitionApprovalTickets.FirstOrDefault();
        //        }
        //        return ticket;

        //    }
        //}


        //public List<RequisitionApprovalTracker> RequisitionApprovalTrackers
        //{
        //    get
        //    {
        //        if (RequisitionApprovalTicket == null)
        //        {
        //            return new List<RequisitionApprovalTracker>();
        //        }
        //        if (RequisitionApprovalTicket != null && RequisitionApprovalTicket.RequisitionApprovalTrackers == null)
        //        {
        //            return new List<RequisitionApprovalTracker>();
        //        }
        //        return RequisitionApprovalTicket.RequisitionApprovalTrackers.ToList();
        //    }
        //}

        public ApprovalLevel ApprovalLevel
        {
            get
            {
                if (ExpenseApprovalTicket == null)
                {
                    return new ApprovalLevel();
                }
                return ExpenseApprovalTicket.ApprovalLevel;
            }
        }

        public ApprovalStatus ApprovalStatus
        {
            get
            {
                if (ExpenseApprovalTicket == null)
                {
                    return new ApprovalStatus();
                }
                return ExpenseApprovalTicket.ApprovalStatus;
            }
        }

        public ApprovalPanel ApprovalPanel
        {
            get
            {
                if (ExpenseApprovalTicket == null)
                {
                    return new ApprovalPanel();
                }
                return ExpenseApprovalTicket.ApprovalPanel;
            }
        }

        public List<ApprovalLevelMember> ApprovalLevelMembers
        {
            get
            {
                if (ApprovalLevel == null)
                {
                    return new List<ApprovalLevelMember>();
                }
                if (ApprovalLevel.ApprovalLevelMembers == null)
                {
                    return new List<ApprovalLevelMember>();
                }
                return ApprovalLevel.ApprovalLevelMembers.ToList();
            }
        }

        //public AdvanceStatus AdvanceStatus
        //{
        //    get
        //    {
        //        if (AdvanceRequisitionHeaders == null)
        //        {
        //            return new AdvanceStatus();
        //        }
        //        return AdvanceRequisitionHeaders.AdvanceRequisitionStatus;
        //    }
        //}

        public AdvanceCategory AdvanceCategory
        {
            get
            {
                if (AdvanceExpenseHeader == null)
                {
                    return new AdvanceCategory();
                }
                return AdvanceExpenseHeader.AdvanceCategory;
            }
        }

        private EmployeeVM employeeVm;

        public string EmployeeName
        {
            get { return employeeVm.EmployeeName; }
        }

        public string DepartmentName
        {
            get { return employeeVm.Department; }
        }

        public string Designation
        {
            get { return employeeVm.Designation; }
        }

        public string EmployeeId
        {
            get { return employeeVm.EmployeeId; }
        }

        public decimal AdvanceRequisitionAmount
        {
            get
            {
                if (AdvanceRequisitionDetails == null)
                {
                    return 0;
                }
                return AdvanceRequisitionDetails.Sum(c => c.GetAdvanceAmountInBdt());
            }
        }

        public decimal ExpenseTotalAmount
        {
            get
            {
                if (AdvanceExpenseDetails == null)
                {
                    return 0;
                }
                return AdvanceExpenseDetails.Sum(c => c.GetExpenseAmountInBdt());
            }
        }

        public decimal ReimburseOrRefundAmount
        {
            get { return ExpenseTotalAmount - AdvanceRequisitionAmount; }
        }

        public string ReimburseOrRefundAmountText
        {
            get
            {
                if (ReimburseOrRefundAmount<0)
                {
                    return "(" + Math.Abs(ReimburseOrRefundAmount).ToString("N") + ")";
                }
                return ReimburseOrRefundAmount.ToString("N");
            }
        }

        public string FromDate
        {
            get
            {
                if (AdvanceExpenseHeader == null)
                {
                    return "N/A";
                }
                return AdvanceExpenseHeader.FromDate.Date.ToString("dd-MMM-yyyy");
            }
        }

        public string ToDate
        {
            get
            {
                if (AdvanceExpenseHeader == null)
                {
                    return "N/A";
                }
                return AdvanceExpenseHeader.ToDate.Date.ToString("dd-MMM-yyyy");
            }
        }

        public double ConversionRate
        {
            get
            {
                if (AdvanceRequisitionHeaders == null)
                {
                    return 0;
                }
                return Math.Round(AdvanceRequisitionHeaders.Average(c=>c.ConversionRate),2);
            }
        }

        public string ExpenseStatus
        {
            get
            {
                if (ApprovalStatus == null)
                {
                    return "N/A";
                }
                return ApprovalStatus.Name;
            }
        }

        public string CurrentApprovalLevel
        {
            get
            {
                if (ApprovalLevel == null)
                {
                    return "N/A";
                }
                return ApprovalLevel.Name;
            }
        }

        public List<Advance_VW_GetApprovalLevelMember> ApprovalLevelMemberList { get; set; }
    }
}

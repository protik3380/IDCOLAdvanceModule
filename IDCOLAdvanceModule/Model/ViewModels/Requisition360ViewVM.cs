
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using System.Collections.Generic;
using System.Linq;

namespace IDCOLAdvanceModule.Model.EntityModels.ViewModels
{
    public class Requisition360ViewVM
    {
        public Requisition360ViewVM(AdvanceRequisitionHeader advanceRequisitionHeader, EmployeeVM employeeVm)
        {
            this.AdvanceRequisitionHeader = advanceRequisitionHeader;
            this.employeeVm = employeeVm;
        }
        public AdvanceRequisitionHeader AdvanceRequisitionHeader { get; set; }

        public List<AdvanceRequisitionDetail> AdvanceRequisitionDetails
        {
            get
            {
                if (AdvanceRequisitionHeader == null)
                {
                    return new List<AdvanceRequisitionDetail>();
                }
                return AdvanceRequisitionHeader.AdvanceRequisitionDetails.ToList();
            }
        }

        public RequisitionApprovalTicket RequisitionApprovalTicket
        {
            get
            {
                var ticket = new RequisitionApprovalTicket();
                if (AdvanceRequisitionHeader == null)
                {
                    return new RequisitionApprovalTicket();
                }

                if (AdvanceRequisitionHeader.RequisitionApprovalTickets != null && AdvanceRequisitionHeader.RequisitionApprovalTickets.Any())
                {
                    ticket = AdvanceRequisitionHeader.RequisitionApprovalTickets.FirstOrDefault();
                }
                return ticket;

            }
        }


        public List<RequisitionApprovalTracker> RequisitionApprovalTrackers
        {
            get
            {
                if (RequisitionApprovalTicket == null)
                {
                    return new List<RequisitionApprovalTracker>();
                }
                if (RequisitionApprovalTicket != null && RequisitionApprovalTicket.RequisitionApprovalTrackers == null)
                {
                    return new List<RequisitionApprovalTracker>();
                }
                return RequisitionApprovalTicket.RequisitionApprovalTrackers.ToList();
            }
        }

        public ApprovalLevel ApprovalLevel
        {
            get
            {
                if (RequisitionApprovalTicket == null)
                {
                    return new ApprovalLevel();
                }
                return RequisitionApprovalTicket.ApprovalLevel;
            }
        }

        public ApprovalStatus ApprovalStatus
        {
            get
            {
                if (RequisitionApprovalTicket == null)
                {
                    return new ApprovalStatus();
                }
                return RequisitionApprovalTicket.ApprovalStatus;
            }
        }

        public ApprovalPanel ApprovalPanel
        {
            get
            {
                if (RequisitionApprovalTicket == null)
                {
                    return new ApprovalPanel();
                }
                return RequisitionApprovalTicket.ApprovalPanel;
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

        public AdvanceStatus AdvanceStatus
        {
            get
            {
                if (AdvanceRequisitionHeader == null)
                {
                    return new AdvanceStatus();
                }
                return AdvanceRequisitionHeader.AdvanceRequisitionStatus;
            }
        }

        public AdvanceCategory AdvanceCategory
        {
            get
            {
                if (AdvanceRequisitionHeader == null)
                {
                    return new AdvanceCategory();
                }
                return AdvanceRequisitionHeader.AdvanceCategory;
            }
        }

        public string RequisitionCategoryName
        {
            get { return AdvanceCategory.Name; }
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

        public string FromDate
        {
            get
            {
                if (AdvanceRequisitionHeader == null)
                {
                    return "N/A";
                }
                return AdvanceRequisitionHeader.FromDate.Date.ToString("dd-MMM-yyyy");
            }
        }
        public string ToDate
        {
            get
            {
                if (AdvanceRequisitionHeader == null)
                {
                    return "N/A";
                }
                return AdvanceRequisitionHeader.ToDate.Date.ToString("dd-MMM-yyyy");
            }
        }

        public double ConversionRate
        {
            get
            {
                if (AdvanceRequisitionHeader == null)
                {
                    return 0;
                }
                return AdvanceRequisitionHeader.ConversionRate;
            }
        }

        public string RequisitionStatus
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

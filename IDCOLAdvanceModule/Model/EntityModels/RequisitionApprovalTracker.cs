using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.Base;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class RequisitionApprovalTracker : ApprovalTracker
    {
        public RequisitionApprovalTracker()
        {
            TicketTypeId = (long)TicketTypeEnum.Requisition;
        }

        [NotMapped]
        public RequisitionApprovalTicket RequisitionApprovalTicket
        {
            get
            {
                return ApprovalTicket as RequisitionApprovalTicket;
            }
            set { ApprovalTicket = value; }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvancePettyCashRequisitionHeader : AdvanceRequisitionHeader
    {
        [NotMapped]
        public IEnumerable<AdvancePettyCashRequisitionDetail> AdvancePettyCashRequisitionDetails
        {
            get
            {
                return
                    base.AdvanceRequisitionDetails.Cast<AdvancePettyCashRequisitionDetail>();
            }
            set
            {
                base.AdvanceRequisitionDetails = new List<AdvanceRequisitionDetail>(value);
            }
        }

        [NotMapped]
        public AdvancePettyCashExpenseHeader AdvancePettyCashExpenseHeader
        {
            get { return base.AdvanceExpenseHeader as AdvancePettyCashExpenseHeader;}
            set { base.AdvanceExpenseHeader = value; }
        }
       
        public override RequisitionHistoryHeader GenerateRequisitionHistoryHeaderFromRequisition(AdvanceRequisitionHeader requisitionHeader, HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryHeader requisitionHistoryHeader = Mapper.Map<PettyCashRequisitionHistoryHeader>(requisitionHeader as AdvancePettyCashRequisitionHeader);
            if (requisitionHeader.AdvanceRequisitionDetails != null)
            {
                var detail = requisitionHeader.AdvanceRequisitionDetails.Select(c => (AdvancePettyCashRequisitionDetail)c).ToList();
                requisitionHistoryHeader.RequisitionHistoryDetails = Mapper.Map<List<AdvancePettyCashRequisitionDetail>, List<PettyCashRequisitionHistoryDetail>>(detail).Select(c => (RequisitionHistoryDetail)c).ToList();
            }
            requisitionHistoryHeader.HistoryModeId = (long)historyModeEnum;
            requisitionHistoryHeader.AdvanceRequisitionHeaderId = requisitionHistoryHeader.Id;
            if (requisitionHistoryHeader.RequisitionHistoryDetails != null)
                requisitionHistoryHeader.RequisitionHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return requisitionHistoryHeader;
        }
    }
}

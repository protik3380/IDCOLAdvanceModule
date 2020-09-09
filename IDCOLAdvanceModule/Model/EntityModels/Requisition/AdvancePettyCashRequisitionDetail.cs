using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvancePettyCashRequisitionDetail : AdvanceRequisitionDetail
    {
        [NotMapped]
        public AdvancePettyCashRequisitionHeader AdvancePettyCashRequisitionHeader
        {
            get { return base.AdvanceRequisitionHeader as AdvancePettyCashRequisitionHeader; }
            set { base.AdvanceRequisitionHeader = value; }
        }

        [NotMapped]
        public IEnumerable<AdvancePettyCashExpenseDetail> AdvacenPettyCashExpenseDetails
        {
            get { return base.AdvanceExpenseDetails.Cast<AdvancePettyCashExpenseDetail>(); }
            set
            {
                base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value);
            }
        }

        public override RequisitionHistoryDetail GenerateRequisitionHistoryDetail(AdvanceRequisitionDetail detail,
            long requisitionHistoryHeaderId, HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryDetail requisitionHistoryDetail = Mapper.Map<PettyCashRequisitionHistoryDetail>(detail);
            requisitionHistoryDetail.RequisitionHistoryHeaderId = requisitionHistoryHeaderId;
            requisitionHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return requisitionHistoryDetail;
        }
    }
}
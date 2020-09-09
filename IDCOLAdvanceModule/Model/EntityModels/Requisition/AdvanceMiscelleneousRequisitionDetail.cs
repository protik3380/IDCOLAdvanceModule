using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvanceMiscelleneousRequisitionDetail : AdvanceRequisitionDetail
    {
        public long? MiscelleneousCostItemId { get; set; }
        public CostItem MiscelleneousCostItem { get; set; }

        [NotMapped]
        public AdvanceMiscelleneousRequisitionHeader AdvanceMiscelleneousRequisitionHeader
        {
            get { return base.AdvanceRequisitionHeader as AdvanceMiscelleneousRequisitionHeader; } 
            set { base.AdvanceRequisitionHeader = value; }
        }

        [NotMapped]
        public IEnumerable<AdvanceMiscelleaneousExpenseDetail> AdvanceMiscelleaneousExpenseDetails
        {
            get { return base.AdvanceExpenseDetails.Cast<AdvanceMiscelleaneousExpenseDetail>(); }
            set
            {
                base.AdvanceExpenseDetails = new List<AdvanceExpenseDetail>(value);
            }
        } 

        public override RequisitionHistoryDetail GenerateRequisitionHistoryDetail(AdvanceRequisitionDetail detail,
            long requisitionHistoryHeaderId, HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryDetail requisitionHistoryDetail = Mapper.Map<MiscelleneousRequisitionHistoryDetail>(detail);
            requisitionHistoryDetail.RequisitionHistoryHeaderId = requisitionHistoryHeaderId;
            requisitionHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return requisitionHistoryDetail;
        }
    }
}
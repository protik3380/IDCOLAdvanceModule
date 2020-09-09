using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvanceMiscelleneousRequisitionHeader : AdvanceRequisitionHeader
    {
        public string PlaceOfEvent { get; set; }

        [NotMapped]
        public IEnumerable<AdvanceMiscelleneousRequisitionDetail> AdvanceMiscelleneousRequisitionDetails
        {
            get
            {
                return
                    base.AdvanceRequisitionDetails.Cast<AdvanceMiscelleneousRequisitionDetail>();
            }
            set
            {
                base.AdvanceRequisitionDetails = new List<AdvanceRequisitionDetail>(value);
            }
        }

        [NotMapped]
        public AdvanceMiscelleaneousExpenseHeader AdvanceMiscelleaneousExpenseHeader
        {
            get
            {
                return base.AdvanceExpenseHeader as AdvanceMiscelleaneousExpenseHeader;
            }
            set { base.AdvanceExpenseHeader = value; }
        } 

        public override RequisitionHistoryHeader GenerateRequisitionHistoryHeaderFromRequisition(AdvanceRequisitionHeader requisitionHeader, HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryHeader requisitionHistoryHeader = Mapper.Map<MiscelleneousRequisitionHistoryHeader>(requisitionHeader as AdvanceMiscelleneousRequisitionHeader);
            if (requisitionHeader.AdvanceRequisitionDetails != null)
            {
                var detail = requisitionHeader.AdvanceRequisitionDetails.Select(c => (AdvanceMiscelleneousRequisitionDetail)c).ToList();
                requisitionHistoryHeader.RequisitionHistoryDetails = Mapper.Map<List<AdvanceMiscelleneousRequisitionDetail>, List<MiscelleneousRequisitionHistoryDetail>>(detail).Select(c => (RequisitionHistoryDetail)c).ToList();
            }
            requisitionHistoryHeader.HistoryModeId = (long)historyModeEnum;
            requisitionHistoryHeader.AdvanceRequisitionHeaderId = requisitionHistoryHeader.Id;
            if (requisitionHistoryHeader.RequisitionHistoryDetails != null)
                requisitionHistoryHeader.RequisitionHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return requisitionHistoryHeader;
        }
    }
}

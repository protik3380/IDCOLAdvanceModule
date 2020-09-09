using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvanceTravelRequisitionHeader : AdvanceRequisitionHeader
    {
        public string PlaceOfVisit { get; set; }
        public string SourceOfFund { get; set; }
        public bool IsSponsorFinanced { get; set; }
        public string SponsorName { get; set; }

        [NotMapped]
        public IEnumerable<AdvanceTravelRequisitionDetail> AdvanceTravelRequisitionDetails
        {
            get
            {
                return
                    base.AdvanceRequisitionDetails.Cast<AdvanceTravelRequisitionDetail>();
            }
            set
            {
                base.AdvanceRequisitionDetails = new List<AdvanceRequisitionDetail>(value);
            }
        }

        [NotMapped]
        public AdvanceTravelExpenseHeader AdvanceTravelExpenseHeader
        {
            get
            {
                return
                    base.AdvanceExpenseHeader as AdvanceTravelExpenseHeader;
            }
            set { base.AdvanceExpenseHeader = value; }
        }

        public override RequisitionHistoryHeader GenerateRequisitionHistoryHeaderFromRequisition(AdvanceRequisitionHeader requisitionHeader,HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryHeader requisitionHistoryHeader = Mapper.Map<TravelRequisitionHistoryHeader>(requisitionHeader as AdvanceTravelRequisitionHeader);
            if (requisitionHeader.AdvanceRequisitionDetails != null)
            {
                var detail = requisitionHeader.AdvanceRequisitionDetails.Select(c => (AdvanceTravelRequisitionDetail)c).ToList();
                requisitionHistoryHeader.RequisitionHistoryDetails = Mapper.Map<List<AdvanceTravelRequisitionDetail>, List<TravelRequisitionHistoryDetail>>(detail).Select(c => (RequisitionHistoryDetail)c).ToList();
            }
            requisitionHistoryHeader.HistoryModeId = (long)historyModeEnum;
            requisitionHistoryHeader.AdvanceRequisitionHeaderId = requisitionHistoryHeader.Id;
            if (requisitionHistoryHeader.RequisitionHistoryDetails != null)
                requisitionHistoryHeader.RequisitionHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return requisitionHistoryHeader;
        }
    }
}

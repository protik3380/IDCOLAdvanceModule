using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvanceCorporateAdvisoryRequisitionHeader : AdvanceRequisitionHeader
    {
        public string CorporateAdvisoryPlaceOfEvent { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? TotalRevenue { get; set; }
        public string AdvanceCorporateRemarks { get; set; }

        [NotMapped]
        public IEnumerable<AdvanceCorporateAdvisoryRequisitionDetail> AdvanceCorporateAdvisoryRequisitionDetails
        {
            get
            {
                return
                    base.AdvanceRequisitionDetails.Cast<AdvanceCorporateAdvisoryRequisitionDetail>();
            }
            set
            {
                base.AdvanceRequisitionDetails = new List<AdvanceRequisitionDetail>(value);
            }
        }

        public override RequisitionHistoryHeader GenerateRequisitionHistoryHeaderFromRequisition(AdvanceRequisitionHeader requisitionHeader,
            HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryHeader requisitionHistoryHeader = Mapper.Map<CorporateAdvisoryRequisitionHistoryHeader>(requisitionHeader as AdvanceCorporateAdvisoryRequisitionHeader);
            if (requisitionHeader.AdvanceRequisitionDetails != null)
            {
                var detail = requisitionHeader.AdvanceRequisitionDetails.Select(c => (AdvanceCorporateAdvisoryRequisitionDetail)c).ToList();
                requisitionHistoryHeader.RequisitionHistoryDetails = Mapper.Map<List<AdvanceCorporateAdvisoryRequisitionDetail>, List<CorporateAdvisoryRequisitionHistoryDetail>>(detail).Select(c => (RequisitionHistoryDetail)c).ToList();
            }
            requisitionHistoryHeader.HistoryModeId = (long)historyModeEnum;
            requisitionHistoryHeader.AdvanceRequisitionHeaderId = requisitionHistoryHeader.Id;
            if (requisitionHistoryHeader.RequisitionHistoryDetails != null)
                requisitionHistoryHeader.RequisitionHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return requisitionHistoryHeader;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using AutoMapper;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.Enums;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public class AdvanceOverseasTravelRequisitionHeader : AdvanceRequisitionHeader
    {
        public long PlaceOfVisitId { get; set; }
        public string OverseasSourceOfFund { get; set; }
        public bool IsOverseasSponsorFinanced { get; set; }
        public string OverseasSponsorName { get; set; }
        public string CountryName { get; set; }
        
        public PlaceOfVisit PlaceOfVisit { get; set; }

        [NotMapped]
        public IEnumerable<AdvanceOverseasTravelRequisitionDetail> AdvanceOverseasTravelRequisitionDetails
        {
            get
            {
                return
                    base.AdvanceRequisitionDetails.Cast<AdvanceOverseasTravelRequisitionDetail>();
            }
            set
            {
                base.AdvanceRequisitionDetails = new List<AdvanceRequisitionDetail>(value);
            }
        }

        [NotMapped]
        public AdvanceOverseasTravelExpenseHeader AdvanceOverseasTravelExpenseHeader
        {
            get { return base.AdvanceExpenseHeader as AdvanceOverseasTravelExpenseHeader; }
            set { base.AdvanceExpenseHeader = value; }
        }

        public override RequisitionHistoryHeader GenerateRequisitionHistoryHeaderFromRequisition(AdvanceRequisitionHeader requisitionHeader,HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryHeader requisitionHistoryHeader = Mapper.Map<OverseasTravelRequisitionHistoryHeader>(requisitionHeader as AdvanceOverseasTravelRequisitionHeader);

            if (requisitionHeader.AdvanceRequisitionDetails != null)
            {
                var detail = requisitionHeader.AdvanceRequisitionDetails.Select(c => (AdvanceOverseasTravelRequisitionDetail)c).ToList();
                requisitionHistoryHeader.RequisitionHistoryDetails = Mapper.Map<List<AdvanceOverseasTravelRequisitionDetail>, List<OverseasTravelRequisitionHistoryDetail>>(detail).Select(c => (RequisitionHistoryDetail)c).ToList();
            }
            requisitionHistoryHeader.HistoryModeId = (long)historyModeEnum;
            requisitionHistoryHeader.AdvanceRequisitionHeaderId = requisitionHistoryHeader.Id;
            if (requisitionHistoryHeader.RequisitionHistoryDetails != null)
                requisitionHistoryHeader.RequisitionHistoryDetails.ToList().ForEach(c => { c.HistoryModeId = (long)historyModeEnum; });
            return requisitionHistoryHeader;
        }
    }
}

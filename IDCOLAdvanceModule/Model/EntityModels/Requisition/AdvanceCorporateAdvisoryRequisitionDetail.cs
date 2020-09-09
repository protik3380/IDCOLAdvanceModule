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
    public class AdvanceCorporateAdvisoryRequisitionDetail : AdvanceRequisitionDetail
    {
        [NotMapped]
        public AdvanceCorporateAdvisoryRequisitionHeader AdvanceCorporateAdvisoryRequisitionHeader
        {
            get { return base.AdvanceRequisitionHeader as AdvanceCorporateAdvisoryRequisitionHeader; }
            set { base.AdvanceRequisitionHeader = value; }
        }
        public override RequisitionHistoryDetail GenerateRequisitionHistoryDetail(AdvanceRequisitionDetail detail,
            long requisitionHistoryHeaderId, HistoryModeEnum historyModeEnum)
        {
            RequisitionHistoryDetail requisitionHistoryDetail = Mapper.Map<CorporateAdvisoryRequisitionHistoryDetail>(detail);
            requisitionHistoryDetail.RequisitionHistoryHeaderId = requisitionHistoryHeaderId;
            requisitionHistoryDetail.HistoryModeId = (long)historyModeEnum;
            return requisitionHistoryDetail;
        }
    }
}

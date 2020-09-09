using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public class CorporateAdvisoryRequisitionHistoryDetail :RequisitionHistoryDetail
    {

        [NotMapped]
        public CorporateAdvisoryRequisitionHistoryHeader CorporateAdvisoryRequisitionHistoryHeader
        {
            get { return RequisitionHistoryHeader as CorporateAdvisoryRequisitionHistoryHeader; }
            set { RequisitionHistoryHeader = value; }
        }
    }
}

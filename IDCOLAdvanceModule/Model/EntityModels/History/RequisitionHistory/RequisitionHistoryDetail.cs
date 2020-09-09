using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory
{
    public abstract class RequisitionHistoryDetail
    {
        public long Id { get; set; }
        public long RequisitionHistoryHeaderId { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public string Purpose { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string Remarks { get; set; }
        public string ReceipientOrPayeeName { get; set; }
        public bool IsThirdPartyReceipient { get; set; }
        public long? VatTaxTypeId { get; set; }
        public long HistoryModeId { get; set; }

        public HistoryMode HistoryMode { get; set; }

        public virtual RequisitionHistoryHeader RequisitionHistoryHeader { get; set; }
        public virtual VatTaxType VatTaxType { get; set; }

        public bool HasVendor()
        {
            return ReceipientOrPayeeName != null;
        }
        public decimal GetAdvanceAmountInBDT(decimal conversionRate)
        {
            return conversionRate * AdvanceAmount;
        }

        
        public virtual decimal GetAdvanceAmountInBdt()
        {
            return (decimal)RequisitionHistoryHeader.ConversionRate * AdvanceAmount;
        }


    }
}

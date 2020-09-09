using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IModel;

namespace IDCOLAdvanceModule.Model.EntityModels.Requisition
{
    public abstract class AdvanceRequisitionDetail
    {
        public long Id { get; set; }
        public long AdvanceRequisitionHeaderId { get; set; }
        public double? NoOfUnit { get; set; }
        public decimal? UnitCost { get; set; }
        public string Purpose { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string Remarks { get; set; }
        public string ReceipientOrPayeeName { get; set; }
        public bool IsThirdPartyReceipient { get; set; }
        public long? VatTaxTypeId { get; set; }
        public long? RequisitionVoucherDetailId { get; set; }
        public string Justification { get; set; }

        public virtual AdvanceRequisitionHeader AdvanceRequisitionHeader { get; set; }
        public virtual VatTaxType VatTaxType { get; set; }
        public virtual RequisitionVoucherDetail RequisitionVoucherDetail { get; set; }

        public bool HasVendor()
        {
            return ReceipientOrPayeeName != null;
        }

        public decimal GetAdvanceAmountInBDT(decimal conversionRate)
        {
            return conversionRate * AdvanceAmount;
        }

        public ICollection<AdvanceExpenseDetail> AdvanceExpenseDetails { get; set; }

        public virtual decimal GetAdvanceAmountInBdt()
        {
            return (decimal)AdvanceRequisitionHeader.ConversionRate * AdvanceAmount;
        }

        public abstract RequisitionHistoryDetail GenerateRequisitionHistoryDetail(AdvanceRequisitionDetail detail, long requisitionHistoryHeaderId, HistoryModeEnum historyModeEnum);
    }
}

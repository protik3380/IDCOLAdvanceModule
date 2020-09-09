using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition
{
    public class RequisitionVoucherHeader
    {
        public long Id { get; set; }
        public long RequisitionHeaderId { get; set; }
        public DateTime VoucherEntryDate { get; set; }
        public string ChequeNo { get; set; }
        public long? BankId { get; set; }
        public long? BranchId { get; set; }
        public string RecipientName { get; set; }
        public string VoucherDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long VoucherStatusId { get; set; }
        public string Currency { get; set; }
        public decimal ConversionRate { get; set; }
        public long? VoucherTypeId { get; set; }
        public DateTime? SentDate { get; set; }

        public AdvanceRequisitionHeader RequisitionHeader { get; set; }
        public VoucherStatus VoucherStatus { get; set; }
        public ICollection<RequisitionVoucherDetail> RequisitionVoucherDetails { get; set; }

        public decimal? GetTotalDrAmount()
        {
            return RequisitionVoucherDetails.Sum(c => c.DrAmount)??0;
        }

        public decimal? GetTotalCrAmount()
        {
            return RequisitionVoucherDetails.Sum(c => c.CrAmount) ?? 0;
        }
    }
}

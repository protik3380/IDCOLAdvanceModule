using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition
{
    public class RequisitionVoucherDetail
    {
        public long Id { get; set; }
        public long VoucherHeaderId { get; set; }
        public string AccountCode { get; set; }
        public decimal? DrAmount { get; set; }
        public decimal? CrAmount { get; set; }
        public string Description { get; set; }
        public bool IsNet { get; set; }
        public int? VendorId { get; set; }
        public int? VatTaxCategoryId { get; set; }
        public string Percentage { get; set; }

        public RequisitionVoucherHeader VoucherHeader { get; set; }
        public ICollection<AdvanceRequisitionDetail> RequisitionDetails { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IRequisitionVoucherHeaderManager : IManager<RequisitionVoucherHeader>
    {
        string GetVoucherStatusByHeaderIdAndRecipientName(long headerId, string recipient);
        RequisitionVoucherHeader GetByHeaderIdAndRecipientName(long headerId, string recipient);
        ICollection<RequisitionVoucherHeader> GetAllDraftVoucher();
        ICollection<RequisitionVoucherHeader> GetAllSentVoucher();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IRequisitionHistoryHeaderManager : IManager<RequisitionHistoryHeader>
    {
        ICollection<RequisitionHistoryHeader> GetAllByRequisitionHeaderId(long requisitionHeaderId);
    }
}

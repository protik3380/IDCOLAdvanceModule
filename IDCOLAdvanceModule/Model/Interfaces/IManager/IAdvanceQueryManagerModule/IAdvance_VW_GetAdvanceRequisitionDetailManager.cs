﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetAdvanceRequisitionDetailManager : IQueryManager<Advance_VW_GetAdvanceRequisitionDetail>
    {
        ICollection<Advance_VW_GetAdvanceRequisitionDetail> GetByHeader(long id);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.EntityModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IOverseasTravelWiseCostItemSettingManager : IManager<OverseasTravelWiseCostItemSetting>
    {
        OverseasTravelWiseCostItemSetting Get(long overseasTravelGroupId);
    }
}

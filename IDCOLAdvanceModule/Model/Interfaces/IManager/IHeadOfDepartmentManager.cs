﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IHeadOfDepartmentManager:IManager<HeadOfDepartment>
    {
        HeadOfDepartment GetByDepartmentId(decimal id);
        List<decimal> GetDepartmentIdByUserName(string memberUserName);
    }
}

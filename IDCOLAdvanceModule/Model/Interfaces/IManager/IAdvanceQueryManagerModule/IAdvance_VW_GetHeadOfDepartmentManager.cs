using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetHeadOfDepartmentManager : IQueryManager<Advance_VW_GetHeadOfDepartment>
    {
        Advance_VW_GetHeadOfDepartment GetHeadOfDepartmentOfAnEmployee(string employeeUserName);
    }
}

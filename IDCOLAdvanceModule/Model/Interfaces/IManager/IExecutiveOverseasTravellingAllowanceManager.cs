using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.EntityModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IExecutiveOverseasTravellingAllowanceManager : IManager<ExecutiveOverseasTravellingAllowance>
    {
        bool IsExecutiveOverseasTravellingAllowanceAvailable(
            ExecutiveOverseasTravellingAllowance executiveOverseasTravellingAllowance);
        ExecutiveOverseasTravellingAllowance GetByLocationGroupIdCostItemAndEmployeeCategoryId(long locationGroupId,
            long costItemId, long employeeCategoryId);
    }
}

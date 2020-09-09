using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.EntityModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IRequisitionSourceOfFundManager : IManager<RequisitionSourceOfFund>
    {
        ICollection<RequisitionSourceOfFund> GetAllByAdvanceRequisitionHeaderId(long id);
        bool Insert(ICollection<RequisitionSourceOfFund> entityCollection, long requisitionHeaderId);
    }
}

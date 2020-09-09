using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.Repository.BaseRepository;
using IDCOLAdvanceModule.Model.SearchModels;

namespace IDCOLAdvanceModule.Model.Interfaces.IModuleRepository
{
    public interface IAdvanceRequisitionHeaderRepository:IRepository<AdvanceRequisitionHeader>
    {
        ICollection<AdvanceRequisitionSearchCriteriaVM> GetBySearchCriteria(AdvanceRequisitionSearchCriteria criteria);
    }
}

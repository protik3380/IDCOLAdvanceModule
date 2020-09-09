using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetRequisitionSignatoryManager : IAdvance_VW_GetRequisitionSignatoryManager
    {
        private readonly IAdvance_VW_GetRequisitionSignatoryRepository _advanceVwGetRequisitionSignatoryRepository;

        public AdvanceVwGetRequisitionSignatoryManager()
        {
            _advanceVwGetRequisitionSignatoryRepository = new AdvanceVwGetRequisitionSignatoryRepository();
        }

        public AdvanceVwGetRequisitionSignatoryManager(IAdvance_VW_GetRequisitionSignatoryRepository advanceVwGetRequisitionSignatoryRepository)
        {
            _advanceVwGetRequisitionSignatoryRepository = advanceVwGetRequisitionSignatoryRepository;
        }

        public ICollection<Advance_VW_GetRequisitionSignatory> GetAll(params Expression<Func<Advance_VW_GetRequisitionSignatory, object>>[] includes)
        {
            return _advanceVwGetRequisitionSignatoryRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetRequisitionSignatory> Get(Expression<Func<Advance_VW_GetRequisitionSignatory, bool>> predicate, params Expression<Func<Advance_VW_GetRequisitionSignatory, object>>[] includes)
        {
            return _advanceVwGetRequisitionSignatoryRepository.Get(predicate, includes);
        }

        public Advance_VW_GetRequisitionSignatory GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetRequisitionSignatory, bool>> predicate, params Expression<Func<Advance_VW_GetRequisitionSignatory, object>>[] includes)
        {
            return _advanceVwGetRequisitionSignatoryRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetRequisitionSignatory> GetByRequisition(long id)
        {
            return Get(c => c.RequisitionId == id);
        }
    }
}

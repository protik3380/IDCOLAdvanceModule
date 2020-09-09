using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetRequisitionSourceOfFundManager : IAdvance_VW_GetRequisitionSourceOfFundManager
    {
        private readonly IAdvance_VW_GetRequisitionSourceOfFundRepository _advanceVwGetRequisitionSourceOfFundRepository;

        public AdvanceVwGetRequisitionSourceOfFundManager()
        {
            _advanceVwGetRequisitionSourceOfFundRepository = new AdvanceVwGetRequisitionSourceOfFundRepository();
        }

        public AdvanceVwGetRequisitionSourceOfFundManager(IAdvance_VW_GetRequisitionSourceOfFundRepository advanceVwGetRequisitionSourceOfFundRepository)
        {
            _advanceVwGetRequisitionSourceOfFundRepository = advanceVwGetRequisitionSourceOfFundRepository;
        }

        public ICollection<Advance_VW_GetRequisitionSourceOfFund> GetAll(params Expression<Func<Advance_VW_GetRequisitionSourceOfFund, object>>[] includes)
        {
            return _advanceVwGetRequisitionSourceOfFundRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetRequisitionSourceOfFund> Get(Expression<Func<Advance_VW_GetRequisitionSourceOfFund, bool>> predicate, params Expression<Func<Advance_VW_GetRequisitionSourceOfFund, object>>[] includes)
        {
            return _advanceVwGetRequisitionSourceOfFundRepository.Get(predicate, includes);
        }

        public Advance_VW_GetRequisitionSourceOfFund GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetRequisitionSourceOfFund, bool>> predicate, params Expression<Func<Advance_VW_GetRequisitionSourceOfFund, object>>[] includes)
        {
            return _advanceVwGetRequisitionSourceOfFundRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetRequisitionSourceOfFund> GetByRequisition(long id)
        {
            return Get(c => c.RequisitionHeaderId.Value == id);
        }
    }
}

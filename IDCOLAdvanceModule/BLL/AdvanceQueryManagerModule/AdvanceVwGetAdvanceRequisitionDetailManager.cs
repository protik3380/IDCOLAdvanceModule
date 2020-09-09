using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetAdvanceRequisitionDetailManager : IAdvance_VW_GetAdvanceRequisitionDetailManager
    {
        private readonly IAdvance_VW_GetAdvanceRequisitionDetailRepository
            _advanceVwGetAdvanceRequisitionDetailRepository;

        public AdvanceVwGetAdvanceRequisitionDetailManager()
        {
            _advanceVwGetAdvanceRequisitionDetailRepository = new AdvanceVwGetAdvanceRequisitionDetailRepository();
        }

        public AdvanceVwGetAdvanceRequisitionDetailManager(IAdvance_VW_GetAdvanceRequisitionDetailRepository advanceVwGetAdvanceRequisitionDetailRepository)
        {
            _advanceVwGetAdvanceRequisitionDetailRepository = advanceVwGetAdvanceRequisitionDetailRepository;
        }

        public ICollection<Advance_VW_GetAdvanceRequisitionDetail> GetAll(params Expression<Func<Advance_VW_GetAdvanceRequisitionDetail, object>>[] includes)
        {
            return _advanceVwGetAdvanceRequisitionDetailRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetAdvanceRequisitionDetail> Get(Expression<Func<Advance_VW_GetAdvanceRequisitionDetail, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceRequisitionDetail, object>>[] includes)
        {
            return _advanceVwGetAdvanceRequisitionDetailRepository.Get(predicate, includes);
        }

        public Advance_VW_GetAdvanceRequisitionDetail GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetAdvanceRequisitionDetail, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceRequisitionDetail, object>>[] includes)
        {
            return _advanceVwGetAdvanceRequisitionDetailRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetAdvanceRequisitionDetail> GetByHeader(long id)
        {
            return Get(c => c.HeaderId == id);
        }
    }
}

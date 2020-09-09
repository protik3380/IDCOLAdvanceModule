using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;

namespace IDCOLAdvanceModule.Repository.AdvanceQueryRepository.SP
{
    public class AdvanceSPRepository :IDisposable
    {
        private readonly AdvanceQueryContext _db;
        public AdvanceSPRepository()
        {
            _db = new AdvanceQueryContext();
        }

        public ICollection<Advance_SP_GetLocalTravelReport> Advance_SP_GetLocalTravelReport(Expression<Func<Advance_SP_GetLocalTravelReport,bool>> predicate = null)
        {
            if (predicate ==null)
            {
                return _db.Advance_SP_GetLocalTravelReport().ToList();
            }
            return _db.Advance_SP_GetLocalTravelReport().AsQueryable().Where(predicate).ToList();
        }

        public ICollection<Advance_SP_GetOverseasTravelReport> Advance_SP_GetOverseasTravelReport(Expression<Func<Advance_SP_GetOverseasTravelReport, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _db.Advance_SP_GetOverseasTravelReport().ToList();
            }
            return _db.Advance_SP_GetOverseasTravelReport().AsQueryable().Where(predicate).ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}

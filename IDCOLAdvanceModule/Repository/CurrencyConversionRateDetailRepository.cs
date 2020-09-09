using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IDCOLAdvanceModule.Repository
{
    public class CurrencyConversionRateDetailRepository : BaseRepository<CurrencyConversionRateDetail>, ICurrencyConversionRateDetailRepository, IDisposable
    {
        public CurrencyConversionRateDetailRepository()
            : base(new AdvanceContext())
        {
        }

        public AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public ICollection<CurrencyConversionRateDetail> GetByExpense(long expenseHeaderId)
        {
            return AdvanceContext.CurrencyConversionRateDetails.Where(
                c => c.AdvanceOverseasTravelExpenseHeaderId == expenseHeaderId).Include(c => c.AdvanceOverseasTravelExpenseHeader).ToList();
        }
    }
}

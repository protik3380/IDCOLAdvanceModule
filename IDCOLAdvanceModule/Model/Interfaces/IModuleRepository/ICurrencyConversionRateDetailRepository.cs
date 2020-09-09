using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.Repository.BaseRepository;
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.Interfaces.IModuleRepository
{
    public interface ICurrencyConversionRateDetailRepository : IRepository<CurrencyConversionRateDetail>
    {
        ICollection<CurrencyConversionRateDetail> GetByExpense(long expenseHeaderId);
    }
}

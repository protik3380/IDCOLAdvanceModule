using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository.MISRepository;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class CurrencyManager : ICurrencyManager
    {
        private ICurrencyRepository _currencyRepository;

        public CurrencyManager()
        {
            _currencyRepository = new CurrencyRepository();
        }

        public ICollection<Solar_CurrencyInfo> GetAll()
        {
            return _currencyRepository.GetAll();
        }

        public ICollection<Solar_CurrencyInfo> GetAllForAdvanceRequisitionAndExpense()
        {
            return _currencyRepository.Get(c => c.ShortName.Equals("TK") || c.ShortName.Equals("USD"));
        }

        public ICollection<Solar_CurrencyInfo> Get(Expression<Func<Solar_CurrencyInfo, bool>> predicate)
        {
            return _currencyRepository.Get(predicate);
        }

        public ICollection<Solar_CurrencyInfo> GetAdvanceRequsitionCategoryWiseCurrencyInfo(long id)
        {
            if (id == (long)AdvanceCategoryEnum.OversearTravel)
            {
                return Get(c => c.ShortName.Equals("USD"));
            }
            return Get(c => c.ShortName.Equals("TK"));
        }
    }
}

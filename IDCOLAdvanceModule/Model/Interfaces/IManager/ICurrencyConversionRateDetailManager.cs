﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface ICurrencyConversionRateDetailManager  :IManager<CurrencyConversionRateDetail>
    {
        ICollection<CurrencyConversionRateDetail> GetByExpense(long expenseHeaderId);
        bool Insert(ICollection<CurrencyConversionRateDetail> conversionRateDetails, long expenseHeaderId);
    }
}

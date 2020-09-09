using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class CurrencyConversionRateDetailManager : ICurrencyConversionRateDetailManager
    {
        private ICurrencyConversionRateDetailRepository _currencyConversionRateDetailRepository;

        public CurrencyConversionRateDetailManager()
        {
            _currencyConversionRateDetailRepository = new CurrencyConversionRateDetailRepository();
        }
        public CurrencyConversionRateDetailManager(ICurrencyConversionRateDetailRepository currencyConversionRateDetailRepository)
        {
            _currencyConversionRateDetailRepository = currencyConversionRateDetailRepository;
        }
        public bool Insert(CurrencyConversionRateDetail entity)
        {
            return _currencyConversionRateDetailRepository.Insert(entity);
        }

        public bool Insert(ICollection<CurrencyConversionRateDetail> entityCollection)
        {
            return _currencyConversionRateDetailRepository.Insert(entityCollection);
        }

        public bool Edit(CurrencyConversionRateDetail entity)
        {
            return _currencyConversionRateDetailRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            CurrencyConversionRateDetail entity = GetById(id);
            return _currencyConversionRateDetailRepository.Delete(entity);
        }

        public CurrencyConversionRateDetail GetById(long id)
        {
            return _currencyConversionRateDetailRepository.GetFirstOrDefaultBy(c => c.Id == id,
                c => c.AdvanceOverseasTravelExpenseHeader);
        }

        public ICollection<CurrencyConversionRateDetail> GetAll()
        {
            return _currencyConversionRateDetailRepository.GetAll(c => c.AdvanceOverseasTravelExpenseHeader);
        }

        public ICollection<CurrencyConversionRateDetail> GetByExpense(long expenseHeaderId)
        {
            return _currencyConversionRateDetailRepository.GetByExpense(expenseHeaderId);
        }

        public bool Insert(ICollection<CurrencyConversionRateDetail> entityCollection, long expenseHeaderId)
        {
            if (entityCollection == null)
            {
                throw new Exception("No data found for conversation rate details");
            }

            var existingCurrencyConversationDetails =
                _currencyConversionRateDetailRepository.Get(c => c.AdvanceOverseasTravelExpenseHeaderId == expenseHeaderId)
                    .ToList();

            var updatedCurrencyConversationDetails = entityCollection.ToList();

            var updateableItems = updatedCurrencyConversationDetails.Where(c => c.AdvanceOverseasTravelExpenseHeaderId > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            var deleteableItems = existingCurrencyConversationDetails.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedCurrencyConversationDetails.Where(c => c.Id == 0).ToList();

            using (var ts = new TransactionScope())
            {

                int deleteCount = 0, updateCount = 0;
                bool isDeleted = false, isUpdated = true;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var sourceOfFund in deleteableItems)
                    {
                        isDeleted = _currencyConversionRateDetailRepository.Delete(sourceOfFund);
                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    addeableItems.ForEach(c => { c.AdvanceOverseasTravelExpenseHeaderId = expenseHeaderId; });
                    _currencyConversionRateDetailRepository.Insert(addeableItems);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var item in updateableItems)
                    {
                        isUpdated = _currencyConversionRateDetailRepository.Edit(item);
                        if (isUpdated)
                        {
                            updateCount++;
                        }
                    }
                }

                ts.Complete();

                return isUpdated || isDeleted;
            }
        }
    }
}

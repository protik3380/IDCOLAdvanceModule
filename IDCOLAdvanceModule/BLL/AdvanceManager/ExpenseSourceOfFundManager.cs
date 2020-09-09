using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExpenseSourceOfFundManager : IExpenseSourceOfFundManager
    {
        private IExpenseSourceOfFundRepository _expenseSourceOfFundRepository;
        private IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;

        public ExpenseSourceOfFundManager()
        {
            _expenseSourceOfFundRepository = new ExpenseSourceOfFundRepository();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
        }

        public ExpenseSourceOfFundManager(IExpenseSourceOfFundRepository expenseSourceOfFundRepository, IAdvanceExpenseHeaderManager advanceExpenseHeaderManager)
        {
            _expenseSourceOfFundRepository = expenseSourceOfFundRepository;
            _advanceExpenseHeaderManager = advanceExpenseHeaderManager;
        }
        public bool Insert(ExpenseSourceOfFund entity)
        {
            return _expenseSourceOfFundRepository.Insert(entity);
        }

        public bool Insert(ICollection<ExpenseSourceOfFund> entityCollection)
        {
            return _expenseSourceOfFundRepository.Insert(entityCollection);

        }

        public bool Insert(ICollection<ExpenseSourceOfFund> entityCollection, long expenseHeaderId)
        {
            var expenseHeader = _advanceExpenseHeaderManager.GetById(expenseHeaderId);
            var existingSourceOfFunds =
                _expenseSourceOfFundRepository.Get(c => c.AdvanceExpenseHeaderId == expenseHeaderId)
                    .ToList();

            var updatedSourceOfFunds = entityCollection.ToList();

            var updateableItems = updatedSourceOfFunds.Where(c => c.Id > 0).ToList();

            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            var deleteableItems = existingSourceOfFunds.Where(c => !itemIdList.Contains(c.Id)).ToList();

            var addeableItems = updatedSourceOfFunds.Where(c => c.Id == 0).ToList();

            using (var ts = new TransactionScope())
            {

                int deleteCount = 0, updateCount = 0, addCount = 0;
                bool isDeleted = false, isUpdated = true, isAdded = false;
                if (deleteableItems != null && deleteableItems.Any())
                {
                    foreach (var sourceOfFund in deleteableItems)
                    {
                        isDeleted = _expenseSourceOfFundRepository.Delete(sourceOfFund);
                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableItems == null ? 0 : deleteableItems.Count());
                }

                if (addeableItems != null && addeableItems.Any())
                {
                    foreach (ExpenseSourceOfFund addeableItem in addeableItems)
                    {
                        addeableItem.SourceOfFund = null;
                    }
                    addeableItems.ForEach(c => { c.AdvanceExpenseHeaderId = expenseHeaderId; });
                    isAdded = _expenseSourceOfFundRepository.Insert(addeableItems);
                    if (isAdded)
                    {
                        addCount++;
                    }
                }
                isAdded = addCount == (addeableItems == null ? 0 : addeableItems.Count());


                if (updateableItems != null && updateableItems.Any())
                {
                    foreach (var item in updateableItems)
                    {
                        isUpdated = _expenseSourceOfFundRepository.Edit(item);
                        if (isUpdated)
                        {
                            updateCount++;
                        }
                    }
                }
                if (!expenseHeader.IsSourceOfEntered)
                {
                    expenseHeader.IsSourceOfEntered = true;
                    _advanceExpenseHeaderManager.Edit(expenseHeader);
                }
                ts.Complete();

                return isUpdated || isDeleted || isAdded;

            }
        }

        public bool Edit(ExpenseSourceOfFund entity)
        {
            return _expenseSourceOfFundRepository.Edit(entity);            
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _expenseSourceOfFundRepository.Delete(entity);
        }

        public ExpenseSourceOfFund GetById(long id)
        {
            return _expenseSourceOfFundRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.AdvanceExpenseHeader);
        }

        public ICollection<ExpenseSourceOfFund> GetAll()
        {
            return _expenseSourceOfFundRepository.GetAll(c => c.AdvanceExpenseHeader);
        }
        public ICollection<ExpenseSourceOfFund> GetAllByExpenseHeaderId(long expenseHeaderId)
        {
            return _expenseSourceOfFundRepository.Get(c=>c.AdvanceExpenseHeaderId == expenseHeaderId,c => c.AdvanceExpenseHeader,c=>c.SourceOfFund).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExpenseHistoryManager : IExpenseHistoryHeaderManager
    {
        private IExpenseHistoryHeaderRepository _expenseHistoryHeaderRepository;

        public ExpenseHistoryManager()
        {
            _expenseHistoryHeaderRepository = new ExpenseHistoryHeaderRepository();
        }

        public ExpenseHistoryManager(IExpenseHistoryHeaderRepository expenseHistoryHeaderRepository)
        {
            _expenseHistoryHeaderRepository = expenseHistoryHeaderRepository;
        }
        public bool Insert(ExpenseHistoryHeader entity)
        {
            return _expenseHistoryHeaderRepository.Insert(entity);
        }

        public bool Insert(ICollection<ExpenseHistoryHeader> entityCollection)
        {
            return _expenseHistoryHeaderRepository.Insert(entityCollection);
        }

        public bool Edit(ExpenseHistoryHeader entity)
        {
            return _expenseHistoryHeaderRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _expenseHistoryHeaderRepository.Delete(entity);
        }

        public ExpenseHistoryHeader GetById(long id)
        {
            return _expenseHistoryHeaderRepository.GetFirstOrDefaultBy(c => c.Id == id, 
                c => c.ExpenseHistoryDetails,
                c => c.HistoryMode, 
                c => c.AdvanceExpenseHeader, 
                c => c.AdvanceExpenseStatus);
        }

        public ICollection<ExpenseHistoryHeader> GetAll()
        {
            return _expenseHistoryHeaderRepository.GetAll(
               c => c.ExpenseHistoryDetails,
               c => c.HistoryMode,
               c => c.AdvanceExpenseHeader,
               c => c.AdvanceExpenseStatus);
        }
        public ICollection<ExpenseHistoryHeader> GetAllBy(long expenseHeaderId)
        {
            return _expenseHistoryHeaderRepository.Get(c=>c.AdvanceExpenseHeaderId == expenseHeaderId,
               c => c.ExpenseHistoryDetails,
               c => c.HistoryMode,
               c => c.AdvanceExpenseHeader,
               c => c.AdvanceExpenseStatus,
               c=>c.ExpenseHistoryDetails.Select(d=>d.HistoryMode));
        }
    }
}

using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExpenseFileManager : IExpenseFileManager
    {
        private readonly IExpenseFileRepository _expenseFileRepository;

        public ExpenseFileManager()
        {
            _expenseFileRepository = new ExpenseFileRepository();
        }

        public ExpenseFileManager(IExpenseFileRepository expenseFileRepository)
        {
            _expenseFileRepository = expenseFileRepository;
        }

        public bool Insert(ExpenseFile entity)
        {
            return _expenseFileRepository.Insert(entity);
        }

        public bool Insert(ICollection<ExpenseFile> entityCollection)
        {
            return _expenseFileRepository.Insert(entityCollection);
        }

        public bool Edit(ExpenseFile entity)
        {
            return _expenseFileRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _expenseFileRepository.Delete(entity);
        }

        public ExpenseFile GetById(long id)
        {
            return _expenseFileRepository.GetFirstOrDefaultBy(c => c.Id == id && !c.IsDeleted);
        }

        public ICollection<ExpenseFile> GetAll()
        {
            return _expenseFileRepository.GetAll(c => !c.IsDeleted);
        }

        public ICollection<ExpenseFile> GetByHeaderId(long headerId)
        {
            return _expenseFileRepository.Get(c => c.AdvanceExpenseHeaderId == headerId && !c.IsDeleted);
        }
    }
}

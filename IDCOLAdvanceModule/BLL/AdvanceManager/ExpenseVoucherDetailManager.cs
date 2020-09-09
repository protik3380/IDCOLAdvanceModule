using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExpenseVoucherDetailManager : IExpenseVoucherDetailManager
    {
        private readonly IExpenseVoucherDetailRepository _expenseVoucherDetailRepository;

        public ExpenseVoucherDetailManager()
        {
            _expenseVoucherDetailRepository = new ExpenseVoucherDetailRepository();
        }

        public ExpenseVoucherDetailManager(IExpenseVoucherDetailRepository expenseVoucherDetailRepository)
        {
            _expenseVoucherDetailRepository = expenseVoucherDetailRepository;
        }

        public bool Insert(ExpenseVoucherDetail entity)
        {
            return _expenseVoucherDetailRepository.Insert(entity);
        }

        public bool Insert(ICollection<ExpenseVoucherDetail> entityCollection)
        {
            return _expenseVoucherDetailRepository.Insert(entityCollection);
        }

        public bool Edit(ExpenseVoucherDetail entity)
        {
            return _expenseVoucherDetailRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _expenseVoucherDetailRepository.Delete(entity);
        }

        public ExpenseVoucherDetail GetById(long id)
        {
            return _expenseVoucherDetailRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<ExpenseVoucherDetail> GetAll()
        {
            return _expenseVoucherDetailRepository.GetAll();
        }
    }
}

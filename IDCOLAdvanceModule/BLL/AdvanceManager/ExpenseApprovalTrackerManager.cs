using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExpenseApprovalTrackerManager : IExpenseApprovalTrackerManager
    {
        private readonly IExpenseApprovalTrackerRepository _expenseApprovalTrackerRepository;

        public ExpenseApprovalTrackerManager()
        {
            _expenseApprovalTrackerRepository = new ExpenseApprovalTrackerRepository();
        }

        public ExpenseApprovalTrackerManager(IExpenseApprovalTrackerRepository expenseApprovalTrackerRepository)
        {
            _expenseApprovalTrackerRepository = expenseApprovalTrackerRepository;
        }
        public bool Insert(ExpenseApprovalTracker entity)
        {
            return _expenseApprovalTrackerRepository.Insert(entity);
        }

        public bool Insert(ICollection<ExpenseApprovalTracker> entityCollection)
        {
            return _expenseApprovalTrackerRepository.Insert(entityCollection);
        }

        public bool Edit(ExpenseApprovalTracker entity)
        {
            return _expenseApprovalTrackerRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _expenseApprovalTrackerRepository.Delete(entity);
        }

        public ExpenseApprovalTracker GetById(long id)
        {
            return _expenseApprovalTrackerRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.ApprovalTicket,
                c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.ApprovalStatus);

        }

        public ICollection<ExpenseApprovalTracker> GetAll()
        {
            return _expenseApprovalTrackerRepository.GetAll(c => c.ApprovalTicket,
                c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.ApprovalStatus);
        }

        public ICollection<ExpenseApprovalTracker> GetByAuthorizedBy(string username)
        {
            return _expenseApprovalTrackerRepository.Get(c => c.AuthorizedBy == username && c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved || c.ApprovalStatusId == (long)ApprovalStatusEnum.Rejected || c.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted && (DbFunctions.DiffDays(c.AuthorizedOn, DateTime.Now) <= Utility.Utility.TimeDuration), c => c.ApprovalTicket);
        }
    }
}

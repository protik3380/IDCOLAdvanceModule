using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Repository
{
    public class AdvanceExpenseHeaderRepository : BaseRepository<AdvanceExpenseHeader>, IAdvanceExpenseHeaderRepository, IDisposable
    {
        public AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public AdvanceExpenseHeaderRepository()
            : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool IsExpenseAlreadyEntryForRequisition(long requisitionId)
        {
            var requisitionHeaders =
                AdvanceContext.AdvanceExpenseHeaders.SelectMany(c => c.AdvanceRequisitionHeaders).AsQueryable();
            return requisitionHeaders.Any(c => c.Id == requisitionId);
        }

        public ICollection<AdvanceExpenseHeader> GetByCriteria(AdvanceExpenseSearchCriteria criteria)
        {
            return
                AdvanceContext.AdvanceExpenseHeaders.Where(
                    c =>
                        (criteria.AdvanceCategoryId == null || c.AdvanceCategoryId == criteria.AdvanceCategoryId) &&
                        (criteria.FromDate == null || DbFunctions.TruncateTime(criteria.FromDate.Value) <= DbFunctions.TruncateTime(c.ExpenseEntryDate))
                        && (criteria.ToDate == null || DbFunctions.TruncateTime(criteria.ToDate.Value) >= DbFunctions.TruncateTime(c.ExpenseEntryDate)))
                        .Include(d => d.AdvanceExpenseDetails)
                        .Include(d => d.AdvanceExpenseStatus)
                        .Include(d => d.AdvanceCategory)
                        .Include(d => d.AdvanceRequisitionHeaders)
                        .Include(d => d.AdvanceRequisitionHeaders.Select(c => c.AdvanceRequisitionDetails))
                        .Include(d => d.ExpenseApprovalTickets)
                        .ToList();
        }
    }
}

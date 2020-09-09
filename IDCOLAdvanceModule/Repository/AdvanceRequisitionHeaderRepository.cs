using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Linq;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;

namespace IDCOLAdvanceModule.Repository
{
    public class AdvanceRequisitionHeaderRepository : BaseRepository<AdvanceRequisitionHeader>, IAdvanceRequisitionHeaderRepository, IDisposable
    {
        public AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public AdvanceRequisitionHeaderRepository()
            : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public ICollection<AdvanceRequisitionSearchCriteriaVM> GetBySearchCriteria(AdvanceRequisitionSearchCriteria criteria)
        {
            return
                AdvanceContext.AdvanceRequisitionSearchCriteriaVms.Where(
                    c => (criteria.DepartmentId == null || c.DepartmentID == criteria.DepartmentId)
                    && (criteria.RankId == null || c.RankID == criteria.RankId) && String.IsNullOrEmpty(criteria.RequesterUserName) || c.EmployeeUserName.Equals(criteria.RequesterUserName)
                    && (String.IsNullOrEmpty(criteria.CurrencyName) || c.Currency.Equals(criteria.CurrencyName))).ToList();
        }
    }
}

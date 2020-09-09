using System.Linq;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
﻿using IDCOLAdvanceModule.Model.EntityModels.Requisition;
﻿using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Model.SearchModels;
using IDCOLAdvanceModule.Repository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;
using IDCOLAdvanceModule.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager;


namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetAdvanceRequisitionManager : IAdvance_VW_GetAdvanceRequisitionManager
    {
        private readonly IAdvance_VW_GetAdvanceRequisitionHeaderRepository _advanceRequisitionQueryRepository;
        private readonly IAdvance_VW_GetAdvanceExpenseRepository _advanceExpenseQueryRepository;
        private readonly IAdvanceRequisitionHeaderRepository _advanceRequisitionHeaderRepository;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;

        public AdvanceVwGetAdvanceRequisitionManager()
        {
            _advanceRequisitionQueryRepository = new AdvanceVwGetAdvanceRequisitionHeaderRepository();
            _advanceRequisitionHeaderRepository = new AdvanceRequisitionHeaderRepository();
            _advanceExpenseQueryRepository = new AdvanceVwGetAdvanceExpenseRepository();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
        }

        public AdvanceVwGetAdvanceRequisitionManager(IAdvance_VW_GetAdvanceRequisitionHeaderRepository advanceRequisitionQueryRepository, IAdvance_VW_GetAdvanceExpenseRepository advanceExpenseQueryRepository, IAdvanceRequisitionHeaderRepository advanceRequisitionHeaderRepository, IAdvanceExpenseHeaderManager advanceExpenseHeaderManager)
        {
            _advanceRequisitionQueryRepository = advanceRequisitionQueryRepository;
            _advanceExpenseQueryRepository = advanceExpenseQueryRepository;
            _advanceRequisitionHeaderRepository = advanceRequisitionHeaderRepository;
            _advanceExpenseHeaderManager = advanceExpenseHeaderManager;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetAll(params Expression<Func<Advance_VW_GetAdvanceRequisition, object>>[] includes)
        {
            return _advanceRequisitionQueryRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> Get(Expression<Func<Advance_VW_GetAdvanceRequisition, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceRequisition, object>>[] includes)
        {
            return _advanceRequisitionQueryRepository.Get(predicate, includes);
        }

        public Advance_VW_GetAdvanceRequisition GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetAdvanceRequisition, bool>> predicate, params Expression<Func<Advance_VW_GetAdvanceRequisition, object>>[] includes)
        {
            return _advanceRequisitionQueryRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetBySearchCriteria(AdvanceRequisitionSearchCriteria criteria)
        {
            Expression<Func<AdvanceRequisitionHeader, bool>> query = c => true;

            if (criteria.AdvanceCategoryId > 0)
            {
                query = query.AndAlso(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId);
            }
            if (criteria.DepartmentId > 0)
            {
                query = query.AndAlso(c => c.RequesterDepartmentId == criteria.DepartmentId);
            }
            if (criteria.CurrencyName != DefaultItem.Text)
            {
                query = query.AndAlso(c => c.Currency.Equals(criteria.CurrencyName));
            }
            if (criteria.RankId != DefaultItem.Value && criteria.RankId != null)
            {
                query = query.AndAlso(c => c.RequesterRankId == criteria.RankId);
            }
            if (!string.IsNullOrEmpty(criteria.RequesterUserName))
            {
                query = query.AndAlso(c => c.RequesterUserName.Equals(criteria.RequesterUserName));
            }
            if (criteria.FromDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.FromDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ToDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }

            if (!String.IsNullOrEmpty(criteria.Remarks))
            {
                query = query.AndAlso(
                     c => c.AdvanceRequisitionDetails.Any(d => d.Remarks.ToLower().Contains(criteria.Remarks.ToLower())));
            }

            var headerIdList = _advanceRequisitionHeaderRepository.Get(query, c => c.AdvanceRequisitionDetails).Select(c => c.Id);

            var headerVwList = _advanceRequisitionQueryRepository.Get(c => headerIdList.Contains(c.HeaderId));

            return headerVwList.ToList();
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetBySearchCriteria(EmployeeRequisitionSearchCriteria criteria)
        {
            Expression<Func<Advance_VW_GetAdvanceRequisition, bool>> query = c => true;
            if (criteria.IsRequisitionForLoggedInUser)
            {
                query = query.AndAlso(c => c.RequesterUserName.Equals(criteria.EmployeeUserName));
            }
            if (!criteria.IsRequisitionForLoggedInUser)
            {
                query = query.AndAlso(c => c.CreatedBy.Equals(criteria.EmployeeUserName) && !c.RequesterUserName.Equals(criteria.EmployeeUserName));
            }
            if (criteria.AdvanceCategoryId > 0)
            {
                query = query.AndAlso(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId);
            }
            if (criteria.CurrencyName != DefaultItem.Text)
            {
                query = query.AndAlso(c => c.Currency.Equals(criteria.CurrencyName));
            }
            if (criteria.FromDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.FromDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ToDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }
            return _advanceRequisitionQueryRepository.Get(query);
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetRequisitionByExpendetureEntry(AdvanceRequisitionSearchCriteria criteria, string userName)
        {
            Expression<Func<Advance_VW_GetAdvanceRequisition, bool>> query = c => true;
            query = query.AndAlso(c => c.AdvanceRequisitionStatusId == (long)AdvanceStatusEnum.Approved && c.RequesterUserName.Equals(userName) && c.AdvanceExpenseHeaderId == null);
            if (criteria.FromDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.FromDate) >= DbFunctions.TruncateTime(criteria.FromDate.Value));
            }
            if (criteria.ToDate.HasValue)
            {
                query = query.AndAlso(c => DbFunctions.TruncateTime(c.ToDate) <= DbFunctions.TruncateTime(criteria.ToDate.Value));
            }
            if (criteria.AdvanceCategoryId > 0)
            {
                query = query.AndAlso(c => c.AdvanceCategoryId == criteria.AdvanceCategoryId);
            }
            if (criteria.BaseAdvanceCategoryId > 0)
            {
                query = query.AndAlso(c => c.BaseAdvanceCategoryId == criteria.BaseAdvanceCategoryId);
            }
            return _advanceRequisitionQueryRepository.Get(query);
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetAllUnadjustedRequisitionsForRequisitionApproval(string userName, long requisitionHeaderId)
        {
            var requisitions =
               _advanceRequisitionQueryRepository
               .Get(c => c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Rejected
                   && c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Draft
                   && c.AdvanceExpenseHeaderId != null
                   && c.RequesterUserName.Equals(userName)
                   && c.HeaderId != requisitionHeaderId)
               .ToList();
            var expenses = _advanceExpenseQueryRepository
                .Get(c => c.AdvanceExpenseStatusId != (long)AdvanceStatusEnum.Approved
                    && c.RequesterUserName.Equals(userName))
                .ToList();
            List<Advance_VW_GetAdvanceRequisition> advanceVwGetAdvanceRequisitions = _advanceRequisitionQueryRepository
               .Get(c => c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Rejected
                   && c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Draft
                   && c.AdvanceExpenseHeaderId == null
                   && c.RequesterUserName.Equals(userName)
                   && c.HeaderId != requisitionHeaderId)
               .ToList();
            foreach (Advance_VW_GetAdvanceExpense expense in expenses)
            {
                if (requisitions.Select(c => c.AdvanceExpenseHeaderId).Contains(expense.HeaderId))
                {
                    var expense1 = expense;
                    var requisition = _advanceRequisitionQueryRepository.Get(c => c.AdvanceExpenseHeaderId == expense1.HeaderId);
                    advanceVwGetAdvanceRequisitions.AddRange(requisition);
                }
            }

            return advanceVwGetAdvanceRequisitions;
        }

        public ICollection<Advance_VW_GetAdvanceRequisition> GetAllUnadjustedRequisitionsForExpenseApproval(string userName, long expenseHeaderId)
        {
            var existingExpense = _advanceExpenseHeaderManager.GetById(expenseHeaderId);
            var requisitions =
               _advanceRequisitionQueryRepository
               .Get(c => c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Rejected
                   && c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Draft
                   && c.AdvanceExpenseHeaderId != null
                   && c.RequesterUserName.Equals(userName))
               .ToList();
            var expenses = _advanceExpenseQueryRepository
                .Get(c => c.AdvanceExpenseStatusId == (long)AdvanceStatusEnum.Approved &&
                          c.RequesterUserName.Equals(userName))
                .ToList();
            List<Advance_VW_GetAdvanceRequisition> advanceVwGetAdvanceRequisitions = _advanceRequisitionQueryRepository
               .Get(c => c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Rejected
                   && c.AdvanceRequisitionStatusId != (long)AdvanceStatusEnum.Draft
                   && c.AdvanceExpenseHeaderId == null
                   && c.RequesterUserName.Equals(userName))
               .ToList();
            foreach (Advance_VW_GetAdvanceExpense expense in expenses)
            {
                if (requisitions.Select(c => c.AdvanceExpenseHeaderId).Contains(expense.HeaderId))
                {
                    var expense1 = expense;
                    var requisition = _advanceRequisitionQueryRepository.Get(c => c.AdvanceExpenseHeaderId == expense1.HeaderId);
                    advanceVwGetAdvanceRequisitions.AddRange(requisition);
                }
            }
            if (existingExpense.AdvanceRequisitionHeaders != null)
            {
                var requisitionHeaders = existingExpense.AdvanceRequisitionHeaders;
                advanceVwGetAdvanceRequisitions = advanceVwGetAdvanceRequisitions.Where(c => requisitionHeaders.Any(d => d.Id != c.HeaderId)).ToList();
            }

            return advanceVwGetAdvanceRequisitions;
        }

        public Advance_VW_GetAdvanceRequisition GetByRequisitionHeaderId(long id)
        {
            return GetFirstOrDefaultBy(c => c.HeaderId == id);
        }
    }
}

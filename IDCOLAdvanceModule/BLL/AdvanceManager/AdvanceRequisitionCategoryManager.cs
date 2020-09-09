using System;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class AdvanceRequisitionCategoryManager : IAdvanceRequisitionCategoryManager
    {
        private readonly IAdvanceRequisitionCategoryRepository _advanceRequisitionCategoryRepository;
        private readonly IEmployeeCategorySettingManager _employeeCategorySettingManager;

        public AdvanceRequisitionCategoryManager()
        {
            _advanceRequisitionCategoryRepository = new AdvanceRequisitionCategoryRepository();
            _employeeCategorySettingManager = new EmployeeCategorySettingManager();
        }

        public bool Insert(AdvanceCategory entity)
        {
            return _advanceRequisitionCategoryRepository.Insert(entity);
        }

        public bool Insert(ICollection<AdvanceCategory> entityCollection)
        {
            return _advanceRequisitionCategoryRepository.Insert(entityCollection);
        }

        public bool Edit(AdvanceCategory entity)
        {
            return _advanceRequisitionCategoryRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _advanceRequisitionCategoryRepository.Delete(entity);
        }

        public AdvanceCategory GetById(long id)
        {
            return _advanceRequisitionCategoryRepository
                .GetFirstOrDefaultBy
                   (
                    c => c.Id == id,
                    c => c.BaseAdvanceCategory,
                    c => c.RequisitionApprovalPanel.ApprovalLevels.Select(d => d.ApprovalLevelMembers),
                    c => c.ExpenseApprovalPanel.ApprovalLevels.Select(d => d.ApprovalLevelMembers)
                   );
        }

        public ICollection<AdvanceCategory> GetAll()
        {
            return _advanceRequisitionCategoryRepository.GetAll();
        }

        public ICollection<AdvanceCategory> GetBy(long baseCategoryId)
        {
            return _advanceRequisitionCategoryRepository.Get(c => c.BaseAdvanceCategoryId == baseCategoryId).OrderBy(c=>c.DisplaySerial).ToList();
        }

        public ICollection<AdvanceCategory> GetBy(long baseCategoryId, decimal rankId)
        {
            var categoryList = GetBy(baseCategoryId);
            bool isExecutive = _employeeCategorySettingManager.IsEmployeeExecutive(rankId);
            if (!isExecutive && baseCategoryId == (long)BaseAdvanceCategoryEnum.Travel)
            {
                categoryList = categoryList.Where(c => c.Id == (long)AdvanceCategoryEnum.LocalTravel).ToList();
            }
            return categoryList;
        }

        public ICollection<AdvanceCategory> GetAllWithApprovalPanel()
        {
            return _advanceRequisitionCategoryRepository.Get(c => c.RequisitionApprovalPanelId != null, c => c.RequisitionApprovalPanel);
        }

        public ICollection<AdvanceCategory> GetCategoriesForExpenseApprovalPanel()
        {
            return _advanceRequisitionCategoryRepository.Get(c => c.ExpenseApprovalPanelId != null, c => c.ExpenseApprovalPanel);
        }
    }
}

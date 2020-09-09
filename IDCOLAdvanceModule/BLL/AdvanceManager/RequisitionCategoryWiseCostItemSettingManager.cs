using System;
using System.Collections.Generic;
﻿using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class RequisitionCategoryWiseCostItemSettingManager : IRequisitionCategoryWiseCostItemSettingManager
    {
        private IRequisitionCategoryWiseCostItemSettingRepository _requisitionCategoryWiseCostItemSettingRepository;
        public RequisitionCategoryWiseCostItemSettingManager()
        {
            _requisitionCategoryWiseCostItemSettingRepository = new RequisitionCategoryWiseCostItemSettingRepository();
        }

        public RequisitionCategoryWiseCostItemSettingManager(IRequisitionCategoryWiseCostItemSettingRepository requisitionCategoryWiseCostItemSettingRepository)
        {
            _requisitionCategoryWiseCostItemSettingRepository = requisitionCategoryWiseCostItemSettingRepository;
        }
        public bool Insert(AdvanceCategoryWiseCostItemSetting entity)
        {
            if (IsCategoryWiseCostItemSettingAlreadyExist(entity))
            {
                throw new BllException("Settings already exist");
            }
            return _requisitionCategoryWiseCostItemSettingRepository.Insert(entity);
        }

        public bool Insert(ICollection<AdvanceCategoryWiseCostItemSetting> entityCollection)
        {
            return _requisitionCategoryWiseCostItemSettingRepository.Insert(entityCollection);
        }

        public bool Edit(AdvanceCategoryWiseCostItemSetting entity)
        {
            return _requisitionCategoryWiseCostItemSettingRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _requisitionCategoryWiseCostItemSettingRepository.Delete(entity);
        }

        public AdvanceCategoryWiseCostItemSetting GetById(long id)
        {
            return _requisitionCategoryWiseCostItemSettingRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<AdvanceCategoryWiseCostItemSetting> GetAll()
        {
            return _requisitionCategoryWiseCostItemSettingRepository.GetAll(c => c.AdvanceCategory,
                c => c.CostItem);
        }

        public bool IsCategoryWiseCostItemSettingAlreadyExist(AdvanceCategoryWiseCostItemSetting categoryWiseCostItemSetting)
        {
            var categoryWiseCostItemSettings = GetAll();
            bool isExist = categoryWiseCostItemSettings.Any(
                c =>
                    c.AdvanceCategoryId == categoryWiseCostItemSetting.AdvanceCategoryId &&
                    c.CostItemId == categoryWiseCostItemSetting.CostItemId);
            return isExist;
        }

        public ICollection<AdvanceCategoryWiseCostItemSetting> GetByAdvanceCategory(long categoryId)
        {
            return
                _requisitionCategoryWiseCostItemSettingRepository.Get(c => c.AdvanceCategoryId == categoryId, c => c.AdvanceCategory, c => c.CostItem).ToList();
        }

        public ICollection<AdvanceCategoryWiseCostItemSetting> Get(Expression<Func<AdvanceCategoryWiseCostItemSetting, bool>> predicate)
        {
            return _requisitionCategoryWiseCostItemSettingRepository.Get(predicate);
        }
    }
}

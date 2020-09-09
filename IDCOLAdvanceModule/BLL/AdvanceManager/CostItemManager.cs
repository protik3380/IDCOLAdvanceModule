using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;
using System.Collections.Generic;
using System.Linq;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class CostItemManager : ICostItemManager
    {
        private readonly ICostItemRepository _costItemRepository;
        private readonly IRequisitionCategoryWiseCostItemSettingManager _requisitionCategoryWiseCostItemSettingManager;
        private readonly IBaseAdvanceRequisitionCategoryManager _baseAdvanceRequisitionCategoryManager;
        public CostItemManager()
        {
            _costItemRepository = new CostItemRepository();
            _requisitionCategoryWiseCostItemSettingManager = new RequisitionCategoryWiseCostItemSettingManager();
            _baseAdvanceRequisitionCategoryManager = new BaseAdvanceRequisitionCategoryManager();
        }

        public CostItemManager(ICostItemRepository costItemRepository, IRequisitionCategoryWiseCostItemSettingManager requisitionCategoryWiseCostItemSettingManager, IBaseAdvanceRequisitionCategoryManager baseAdvanceRequisitionCategoryManager)
        {
            _costItemRepository = costItemRepository;
            _requisitionCategoryWiseCostItemSettingManager = requisitionCategoryWiseCostItemSettingManager;
            _baseAdvanceRequisitionCategoryManager = baseAdvanceRequisitionCategoryManager;
        }
        public bool Insert(CostItem entity)
        {
            return _costItemRepository.Insert(entity);
        }

        public bool Insert(ICollection<CostItem> entityCollection)
        {
            return _costItemRepository.Insert(entityCollection);
        }

        public bool Edit(CostItem entity)
        {
            return _costItemRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _costItemRepository.Delete(entity);
        }

        public CostItem GetById(long id)
        {
            return _costItemRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<CostItem> GetAll()
        {
            return _costItemRepository.GetAll();
        }

        public ICollection<CostItem> GetByAdvanceCategory(long categoryId)
        {
            return
                _costItemRepository.Get(
                    c => c.RequisitionCategoryWiseCostItemSetting.Any(d=>d.AdvanceCategoryId == categoryId && d.IsEntitlementMandatory ==true),
                        c => c.RequisitionCategoryWiseCostItemSetting).ToList();
        }

        public bool IsEntitlementMandatoryFor(long categoryId, long costItemId)
        {
            var requisitionCategoryWiseSettings =
                _requisitionCategoryWiseCostItemSettingManager.Get(
                    c => c.CostItemId == costItemId && c.AdvanceCategoryId == categoryId).FirstOrDefault();

            if (requisitionCategoryWiseSettings!=null && requisitionCategoryWiseSettings.IsEntitlementMandatory)
            {
                return true;
            }

            return false;
        }
        public ICollection<CostItem> GetByBaseAdvanceCategory(long baseCategoryId)
        {
            var advanceCategoryList =
                _baseAdvanceRequisitionCategoryManager.GetAll().Select(c=>c.AdvanceCategories).ToList();
            List<CostItem> costItems = new List<CostItem>();
            foreach (var advanceCategories in advanceCategoryList)
            {
                foreach (AdvanceCategory advanceCategory in advanceCategories)
                {
                    var advanceCategoryWiseCostItems = _requisitionCategoryWiseCostItemSettingManager.GetByAdvanceCategory(advanceCategory.Id).ToList();
                    foreach (var advanceCategoryWiseCostItem in advanceCategoryWiseCostItems)
                    {
                        CostItem costItem =
                            _costItemRepository.GetFirstOrDefaultBy(c => c.Id == advanceCategoryWiseCostItem.CostItemId);
                        costItems.Add(costItem);
                    }

                }
            }
            return costItems;
        }
    }
}

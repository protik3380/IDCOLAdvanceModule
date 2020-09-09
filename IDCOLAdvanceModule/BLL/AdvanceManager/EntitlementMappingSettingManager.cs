using System;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class EntitlementMappingSettingManager : IEntitlementMappingSettingManager
    {
        private IEntitlementMappingSettingRepository _entitlementMappingSettingRepository;
        private ICostItemManager _costItemManager;

        public EntitlementMappingSettingManager()
        {
            _entitlementMappingSettingRepository = new EntitlementMappingSettingRepository();
            _costItemManager = new CostItemManager();
        }

        public EntitlementMappingSettingManager(IEntitlementMappingSettingRepository entitlementMappingSettingRepository,ICostItemManager costItemManager)
        {
            _entitlementMappingSettingRepository = entitlementMappingSettingRepository;
            _costItemManager = costItemManager;
        }
        public bool Insert(EntitlementMappingSettingHeader entity)
        {
            var entitlementMapping = _entitlementMappingSettingRepository.GetFirstOrDefaultBy(
                c =>
                    c.AdvanceCategoryId == entity.AdvanceCategoryId &&
                    c.CostItemId == entity.CostItemId);
            if (entitlementMapping != null)
            {
                foreach (var detail in entity.EntitlementMappingSettingDetails)
                {
                    bool isExist = GetEntitlementMappingSettingByCriteria(entitlementMapping, detail);
                    if (isExist)
                    {
                        throw new Exception("Provided Setting is already mapped for " + detail.RankName);
                    }
                    detail.EntitlementMappingSettingHeaderId = entitlementMapping.Id;
                }
                return
                    _entitlementMappingSettingRepository.InsertEntitlementMappingSettingDetails(
                        entity.EntitlementMappingSettingDetails);
            }

            return _entitlementMappingSettingRepository.Insert(entity);
        }

        public bool Insert(ICollection<EntitlementMappingSettingHeader> entityCollection)
        {
            return _entitlementMappingSettingRepository.Insert(entityCollection);
        }

        public bool Edit(EntitlementMappingSettingHeader entity)
        {
            return _entitlementMappingSettingRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _entitlementMappingSettingRepository.Delete(entity);
        }

        public EntitlementMappingSettingHeader GetById(long id)
        {
            return _entitlementMappingSettingRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<EntitlementMappingSettingHeader> GetAll()
        {
            return _entitlementMappingSettingRepository.GetAll(c => c.EntitlementMappingSettingDetails);
        }

        public bool GetEntitlementMappingSettingByCriteria(
            EntitlementMappingSettingHeader entitlementMappingSettingHeader,
            EntitlementMappingSettingDetail entitlementMappingSettingDetail)
        {

            var entitlementSettings = GetAll();
            bool isExist =
                entitlementSettings.Any(
                    c =>
                        c.EntitlementMappingSettingDetails.Any(
                            d =>
                                d.EntitlementMappingSettingHeaderId ==
                                entitlementMappingSettingHeader.Id &&
                                d.RankID == entitlementMappingSettingDetail.RankID));

            return isExist;
        }

        public bool InsertEntitlementMappingSettingDetails(ICollection<EntitlementMappingSettingDetail> details)
        {
            return _entitlementMappingSettingRepository.InsertEntitlementMappingSettingDetails(details);
        }

        public ICollection<EntitlementMappingSettingVM> GetEntitlementMappingSettingVm()
        {
            return _entitlementMappingSettingRepository.GetEntitlementMappingSettingVm();
        }

        public bool DeleteEntitlementMappingSetting(long id)
        {
            return _entitlementMappingSettingRepository.DeleteEntitlementMappingSetting(id);
        }

        public EntitlementMappingSettingVM GetEntitlementSettingByCriteria(long rankId, long advanceCategoryId, long costItemId)
        {
            

            var entitlementSettings =  _entitlementMappingSettingRepository.GetEntitlementSettingByCriteria(rankId, advanceCategoryId,
                costItemId);
            
            if (entitlementSettings == null)
            {
                bool isMandatory = _costItemManager.IsEntitlementMandatoryFor(advanceCategoryId, costItemId);
                if (isMandatory)
                {
                    throw new BllException("Entitlement is mandatory but entitlement amount is not set for this cost item!");
                }
            }
            

            return entitlementSettings;
        }
    }
}

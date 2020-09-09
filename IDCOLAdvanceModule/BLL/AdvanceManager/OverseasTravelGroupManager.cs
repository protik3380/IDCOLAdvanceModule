using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    class OverseasTravelGroupManager : IOverseasTravelGroupManager
    {
        private IOverseasTravelGroupRepository _overseasTravelGroupRepository;
        IOverseasTravelGroupMappingSettingRepository _overseasTravelGroupMappingSettingRepository;

        public OverseasTravelGroupManager()
        {
            _overseasTravelGroupRepository = new OverseasTravelGroupRepository();
            _overseasTravelGroupMappingSettingRepository = new OverseasTravelGroupMappingSettingRepository();
        }

        public OverseasTravelGroupManager(IOverseasTravelGroupRepository overseasTravelGroupRepository, IOverseasTravelGroupMappingSettingRepository overseasTravelGroupMappingSettingRepository)
        {
            _overseasTravelGroupRepository = overseasTravelGroupRepository;
            _overseasTravelGroupMappingSettingRepository = overseasTravelGroupMappingSettingRepository;
        }
        public bool Insert(OverseasTravelGroup entity)
        {
            return _overseasTravelGroupRepository.Insert(entity);
        }

        public bool Insert(ICollection<OverseasTravelGroup> entityCollection)
        {
            throw new NotImplementedException();
        }

        public bool Edit(OverseasTravelGroup entity)
        {
            var existingOverseasTravel = GetById(entity.Id);
            var existingOverseasTravelMapping = existingOverseasTravel.OverseasTravelGroupMappingSettings.ToList();
            var updatedOverseasTravelMapping = entity.OverseasTravelGroupMappingSettings.ToList();

            foreach (OverseasTravelGroupMappingSetting updatedMappingSetting in updatedOverseasTravelMapping)
            {
                foreach (var existedMappingSetting in existingOverseasTravelMapping)
                {
                    if (updatedMappingSetting.RankId == existedMappingSetting.RankId)
                    {
                        updatedMappingSetting.OverseasTravelGroupId = existedMappingSetting.OverseasTravelGroupId;
                        updatedMappingSetting.Id = existedMappingSetting.Id;
                    }
                }
            }

            var updateableItems = updatedOverseasTravelMapping.Where(c => c.Id > 0).ToList();
            var itemIdList = updateableItems.Select(c => c.Id).ToList();

            //var deleteableItems = existingOverseasTravelMapping.Where(c => !itemIdList.Contains(c.Id)).ToList();
            var deleteableIdList = existingOverseasTravelMapping.Where(c => !itemIdList.Contains(c.Id)).Select(c=>c.Id).ToList();
            var addeableItems = updatedOverseasTravelMapping.Where(c => c.Id == 0).ToList();


            using (var ts = new TransactionScope())
            {
                entity.OverseasTravelGroupMappingSettings = null;
                bool isUpdatedOverseasTravelGroup = _overseasTravelGroupRepository.Edit(entity);

                
                bool isDeleted = false, isUpdated = false, isAdded = false;
                if (deleteableIdList != null && deleteableIdList.Any())
                {
                    int deleteCount = 0;
                    foreach (var id in deleteableIdList)
                    {
                        var overseasTravelMapping =
                            _overseasTravelGroupMappingSettingRepository.GetFirstOrDefaultBy(c => c.Id == id);
                        isDeleted = _overseasTravelGroupMappingSettingRepository.Delete(overseasTravelMapping);
                        if (isDeleted)
                        {
                            deleteCount++;
                        }
                    }
                    isDeleted = deleteCount == (deleteableIdList == null ? 0 : deleteableIdList.Count());
                }
                if (addeableItems != null && addeableItems.Any())
                {
                    addeableItems.ForEach(c => { c.OverseasTravelGroupId = entity.Id; });
                    isAdded = _overseasTravelGroupMappingSettingRepository.Insert(addeableItems);
                }

                if (updateableItems != null && updateableItems.Any())
                {
                    int updateCount = 0;
                    foreach (var item in updateableItems)
                    {
                        _overseasTravelGroupMappingSettingRepository.Edit(item);
                    }
                    
                }

                ts.Complete();

                return isUpdatedOverseasTravelGroup || isDeleted || isAdded;
            }
            
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public OverseasTravelGroup GetById(long id)
        {
            return _overseasTravelGroupRepository.GetFirstOrDefaultBy(c => c.Id == id,
                c => c.OverseasTravelGroupMappingSettings, c => c.LocationGroup,c=>c.OverseasTravelWiseCostItemSettings);
        }

        public ICollection<OverseasTravelGroup> GetAll()
        {
            return _overseasTravelGroupRepository.GetAll(c=>c.OverseasTravelGroupMappingSettings,c=>c.LocationGroup,c=>c.OverseasTravelWiseCostItemSettings);
        }
    }
}

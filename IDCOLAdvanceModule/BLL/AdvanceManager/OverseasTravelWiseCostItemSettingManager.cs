using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class OverseasTravelWiseCostItemSettingManager : IOverseasTravelWiseCostItemSettingManager
    {
        public IOverseasTravelWiseCostItemSettingRepository _OverseasTravelWiseCostItemSettingRepository;

        public OverseasTravelWiseCostItemSettingManager()
        {
            _OverseasTravelWiseCostItemSettingRepository = new OverseasTravelWiseCostItemSettingRepository();
        }

        public OverseasTravelWiseCostItemSettingManager(IOverseasTravelWiseCostItemSettingRepository overseasTravelWiseCostItemSettingRepository)
        {
            _OverseasTravelWiseCostItemSettingRepository = overseasTravelWiseCostItemSettingRepository;
        }

        public bool Insert(OverseasTravelWiseCostItemSetting entity)
        {
            return _OverseasTravelWiseCostItemSettingRepository.Insert(entity);
        }

        public bool Insert(ICollection<OverseasTravelWiseCostItemSetting> entityCollection)
        {
            throw new NotImplementedException();
        }

        public bool Edit(OverseasTravelWiseCostItemSetting entity)
        {
            return _OverseasTravelWiseCostItemSettingRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public OverseasTravelWiseCostItemSetting GetById(long id)
        {
            return _OverseasTravelWiseCostItemSettingRepository.GetFirstOrDefaultBy(c => c.Id == id,
                c => c.OverseasTravelGroup, c => c.CostItem);
        }

        public ICollection<OverseasTravelWiseCostItemSetting> GetAll()
        {
            return _OverseasTravelWiseCostItemSettingRepository.GetAll(c => c.OverseasTravelGroup, c => c.CostItem);
        }

        public OverseasTravelWiseCostItemSetting Get(long overseasTravelGroupId)
        {
            return _OverseasTravelWiseCostItemSettingRepository.GetFirstOrDefaultBy(c => c.OverseasTravelGroupId == overseasTravelGroupId,c=>c.OverseasTravelGroup, c => c.CostItem);
        }
    }
}

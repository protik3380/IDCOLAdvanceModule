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
    public class OverseasTravelGroupMappingSettingManager : IOverseasTravelGroupMappingSettingManager
    {
        private IOverseasTravelGroupMappingSettingRepository _overseasTravelGroupMappingSettingRepository;

        public OverseasTravelGroupMappingSettingManager()
        {
            _overseasTravelGroupMappingSettingRepository = new OverseasTravelGroupMappingSettingRepository();
        }

        public OverseasTravelGroupMappingSettingManager(IOverseasTravelGroupMappingSettingRepository overseasTravelGroupMappingSettingRepository)
        {
            _overseasTravelGroupMappingSettingRepository = overseasTravelGroupMappingSettingRepository;
        }
        public bool Insert(OverseasTravelGroupMappingSetting entity)
        {
            return _overseasTravelGroupMappingSettingRepository.Insert(entity);
        }

        public bool Insert(ICollection<OverseasTravelGroupMappingSetting> entityCollection)
        {
            return _overseasTravelGroupMappingSettingRepository.Insert(entityCollection);
        }

        public bool Edit(OverseasTravelGroupMappingSetting entity)
        {
            return _overseasTravelGroupMappingSettingRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _overseasTravelGroupMappingSettingRepository.Delete(entity);
        }

        public OverseasTravelGroupMappingSetting GetById(long id)
        {
            return _overseasTravelGroupMappingSettingRepository.GetFirstOrDefaultBy(c => c.Id == id,
                c => c.OverseasTravelGroup);
        }

        public ICollection<OverseasTravelGroupMappingSetting> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

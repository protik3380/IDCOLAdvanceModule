using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IEntitlementMappingSettingManager:IManager<EntitlementMappingSettingHeader>
    {
        bool GetEntitlementMappingSettingByCriteria(EntitlementMappingSettingHeader entitlementMappingSettingHeader, EntitlementMappingSettingDetail entitlementMappingSettingDetail);
        bool InsertEntitlementMappingSettingDetails(ICollection<EntitlementMappingSettingDetail> details);
        ICollection<EntitlementMappingSettingVM> GetEntitlementMappingSettingVm();
        bool DeleteEntitlementMappingSetting(long id);
        EntitlementMappingSettingVM GetEntitlementSettingByCriteria(long rankId, long advanceCategoryId, long costItemId);
    }
}

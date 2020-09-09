using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;

namespace IDCOLAdvanceModule.Repository
{
    public class EntitlementMappingSettingRepository : BaseRepository<EntitlementMappingSettingHeader>, IEntitlementMappingSettingRepository, IDisposable
    {
        public AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public EntitlementMappingSettingRepository()
            : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool InsertEntitlementMappingSettingDetails(ICollection<EntitlementMappingSettingDetail> details)
        {
            AdvanceContext.EntitlementMappingSettingDetails.AddRange(details);
            return AdvanceContext.SaveChanges() > 0;
        }

        public ICollection<EntitlementMappingSettingVM> GetEntitlementMappingSettingVm()
        {

            return AdvanceContext.EntitlementMappingSettingVms.ToList();
            
        }

        public bool DeleteEntitlementMappingSetting(long id)
        {

            var entity = GetDetailById(id);
            AdvanceContext.EntitlementMappingSettingDetails.Remove(entity);
            return AdvanceContext.SaveChanges() > 0;

        }

        public EntitlementMappingSettingDetail GetDetailById(long id)
        {

            return AdvanceContext.EntitlementMappingSettingDetails.FirstOrDefault(c => c.Id == id);

        }

        public EntitlementMappingSettingVM GetEntitlementSettingByCriteria(long rankId, long advanceCategoryId, long costItemId)
        {
            return
                AdvanceContext.EntitlementMappingSettingVms.FirstOrDefault(
                    c => c.RankId == rankId && c.CategoryId == advanceCategoryId && c.CostItemId == costItemId);
            
        }
    }
}

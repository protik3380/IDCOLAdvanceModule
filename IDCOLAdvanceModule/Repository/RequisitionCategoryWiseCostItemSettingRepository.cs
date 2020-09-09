using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;
using System;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;

namespace IDCOLAdvanceModule.Repository
{
    public class RequisitionCategoryWiseCostItemSettingRepository : BaseRepository<AdvanceCategoryWiseCostItemSetting>, IRequisitionCategoryWiseCostItemSettingRepository, IDisposable
    {
        public RequisitionCategoryWiseCostItemSettingRepository()
            : base(new AdvanceContext())
        {

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

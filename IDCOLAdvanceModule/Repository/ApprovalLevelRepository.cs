using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;

namespace IDCOLAdvanceModule.Repository
{
    public class ApprovalLevelRepository : BaseRepository<ApprovalLevel>, IApprovalLevelRepository, IDisposable
    {
        private AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public ApprovalLevelRepository()
            : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool UpdateDisplaySerial(List<ApprovalLevel> currentLevelItems)
        {
            foreach (ApprovalLevel currentLevelItem in currentLevelItems)
            {
                if (currentLevelItem.Id > 0)
                {
                    AdvanceContext.ApprovalLevels.AddOrUpdate(currentLevelItem);
                }
            }
            return AdvanceContext.SaveChanges() > 0;
        }
    }
}

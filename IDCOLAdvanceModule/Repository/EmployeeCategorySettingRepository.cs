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
    public class EmployeeCategorySettingRepository : BaseRepository<EmployeeCategorySetting>, IEmployeeCategorySettingRepository, IDisposable
    {
        private AdvanceContext AdvanceContext
        {
            get { return db as AdvanceContext; }
        }

        public EmployeeCategorySettingRepository()
            : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public ICollection<EmployeeCategoryDesignationVM> GetEmployeeCategoryDesignationVm()
        {

            return AdvanceContext.EmployeeCategoryDesignationVms.ToList();


        }
    }
}

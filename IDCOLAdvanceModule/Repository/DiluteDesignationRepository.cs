using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository
{
    public class DiluteDesignationRepository:BaseRepository<DiluteDesignation>,IDiluteDesignationRepository
    {
        public AdvanceContext Context
        {
            get
            {
                return db as AdvanceContext;
                
            }
        }

        public DiluteDesignationRepository() : base(new AdvanceContext())
        {
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}

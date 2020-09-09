using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Context.AdvanceModuleContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository.Base;

namespace IDCOLAdvanceModule.Repository
{
    public class ApprovalLevelMemberRepository : BaseRepository<ApprovalLevelMember>, IApprovalLevelMemberRepository, IDisposable
    {
        public ApprovalLevelMemberRepository()
            : base(new AdvanceContext())
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

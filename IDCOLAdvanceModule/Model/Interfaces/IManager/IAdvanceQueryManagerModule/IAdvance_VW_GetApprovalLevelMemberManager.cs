using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule
{
    public interface IAdvance_VW_GetApprovalLevelMemberManager : IQueryManager<Advance_VW_GetApprovalLevelMember>
    {
        ICollection<Advance_VW_GetApprovalLevelMember> GetByApprovalLevel(long id);

        ICollection<Advance_VW_GetApprovalLevelMember> GetApprovalLevelMembers(long ticketId,
            string approvalUserName = null);

        bool IsValidForMember(ApplicationNotification notification, string approvalAuthorityUserName);

        bool CheckLevelMemberExists(long nextLevelId, long ticketId);
    }
}

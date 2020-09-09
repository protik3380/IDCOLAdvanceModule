using System.Collections.Generic;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IApprovalLevelMemberManager : IManager<ApprovalLevelMember>
    {
        ICollection<ApprovalLevelMember> GetByLevelId(long id);
        bool CheckAndInsert(ICollection<ApprovalLevelMember> entityCollection, long designationId, ICollection<Advance_VW_GetApprovalLevelMember> existingLevelMembers);
        int GetMaxValueInPriorityOrder(long approvalLevelId);
        bool UpdatePrioritySerial(List<ApprovalLevelMember> currentLevelItems);
        void ReArrangePriorityOrder(long approvalLevelId);
        ApprovalLevelMember GetByLevelAndPriority(long approvalLevelId, int priority = 1);
    }
}

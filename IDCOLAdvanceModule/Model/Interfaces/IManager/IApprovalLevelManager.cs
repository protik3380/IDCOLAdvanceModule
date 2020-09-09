using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IApprovalLevelManager : IManager<ApprovalLevel>
    {
        ICollection<ApprovalLevel> GetByPanelId(long panelId);
        bool IsLevelOrderExist(ApprovalLevel entity);
        bool IsLevelNameExist(ApprovalLevel entity);
        bool UpdateDisplaySerial(List<ApprovalLevel> currentLevelItems);
        ICollection<ApprovalLevel> GetLevelsOfMember(string memberUserName);
        bool ValidateSourceOfFundEntry(ApprovalLevel entity);
        bool ValidateSourceOfFundVerify(ApprovalLevel entity);
        ApprovalLevel GetLevelWithApprovalLevelMembersForRequsition(long approvalPanelId, long approvalLevelId, long requisitionHeaderId);
        ApprovalLevel GetLevelWithApprovalLevelMembersForExpense(long approvalPanelId, long approvalLevelId, long expenseHeaderId);

    }
}

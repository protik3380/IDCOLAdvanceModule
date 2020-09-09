using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IApprovalPanelManager : IManager<ApprovalPanel>
    {
        ICollection<ApprovalPanel> GetAllByPanelTypeId(int panelTypeId);

        ApprovalPanel GetByApprovalPanelTypeIdAndApprovalPanelName(long approvalPanelTypeId,
            string approvalPanelName);
        bool IsApprovalPanelNameAvailable(ApprovalPanel entity);


        ApprovalPanel GetApprovalPanel(AdvanceCategory category);
        ApprovalPanel GetApprovalPanel(long categoryId);


        ICollection<ApprovalPanel> GetAllForTimeLagRequisition();
    }
}

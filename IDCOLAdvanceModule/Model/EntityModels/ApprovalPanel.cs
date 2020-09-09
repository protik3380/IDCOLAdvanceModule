using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IModel;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ApprovalPanel : IAudit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ApprovalPanelTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual ApprovalPanelType ApprovalPanelType { get; set; }
        public ICollection<RequisitionApprovalTicket> RequisitionApprovalTickets { get; set; }
        public ICollection<RequisitionApprovalTracker> RequisitionApprovalTrackers { get; set; }
        public ICollection<AdvanceCategory> AdvanceRequisitionCategories { get; set; }
        public ICollection<AdvanceCategory> AdvanceExpenseCategories { get; set; }
        public ICollection<ApprovalLevel> ApprovalLevels { get; set; }

        public ApprovalLevel GetStartingLevel()
        {
            ApprovalLevel approvalLevel = null;
            if (ApprovalLevels != null && ApprovalLevels.Any())
            {
                approvalLevel = ApprovalLevels.FirstOrDefault(c => c.LevelOrder == Utility.Utility.StartingLevelNo);
            }
            return approvalLevel;
        }

        public ApprovalLevel GetFinalApprovalLevel()
        {
            ApprovalLevel approvalLevel = null;
            if (ApprovalLevels != null && ApprovalLevels.Any())
            {
                approvalLevel = ApprovalLevels.FirstOrDefault(c => c.LevelOrder == ApprovalLevels.Max(d => d.LevelOrder));
            }

            return approvalLevel;
        }


        public ApprovalLevel GetNextLevel(long approvalLevelId)
        {
            ApprovalLevel nextLevel = null;
            if (ApprovalLevels != null && ApprovalLevels.Any())
            {
                var currentLevel = ApprovalLevels.FirstOrDefault(c => c.Id == approvalLevelId);
                if (currentLevel != null)
                {
                    nextLevel = ApprovalLevels.OrderBy(c => c.LevelOrder).FirstOrDefault(c => c.LevelOrder > currentLevel.LevelOrder);
                }
            }
            return nextLevel;
        }

        public bool IsTopLevel(long approvalLevelId)
        {
            bool isTopLevel = false;

            if (ApprovalLevels != null && ApprovalLevels.Any())
            {
                var currentLevel = ApprovalLevels.FirstOrDefault(c => c.Id == approvalLevelId);

                if (currentLevel != null)
                {
                    isTopLevel = ApprovalLevels.Max(c => c.LevelOrder) == currentLevel.LevelOrder;
                }
            }

            return isTopLevel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ApprovalPanelManager : IApprovalPanelManager
    {
        private IApprovalPanelRepository _approvalPanelRepository;
        private IApprovalLevelRepository _approvalLevelRepository;

        public ApprovalPanelManager()
        {
            _approvalPanelRepository = new ApprovalPanelRepository();
            _approvalLevelRepository = new ApprovalLevelRepository();
        }

        public ApprovalPanelManager(IApprovalPanelRepository approvalPanelRepository,IApprovalLevelRepository approvalLevelRepository)
        {
            _approvalPanelRepository = approvalPanelRepository;
            _approvalLevelRepository = approvalLevelRepository;
        }

        public bool Insert(ApprovalPanel entity)
        {
            entity.CreatedBy = Session.LoginUserName;
            entity.CreatedOn = DateTime.UtcNow.AddHours(6);
            IsApprovalPanelNameAvailable(entity);
            return _approvalPanelRepository.Insert(entity);
        }

        public bool Insert(ICollection<ApprovalPanel> entityCollection)
        {
            entityCollection.ToList().ForEach(c => c.CreatedOn = DateTime.UtcNow.AddHours(6));
            return _approvalPanelRepository.Insert(entityCollection);
        }

        public bool Edit(ApprovalPanel entity)
        {
            entity.LastModifiedBy = Session.LoginUserName;
            entity.LastModifiedOn = DateTime.UtcNow.AddHours(6);
            IsApprovalPanelNameAvailable(entity);
            return _approvalPanelRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _approvalPanelRepository.Delete(entity);
        }

        public ApprovalPanel GetById(long id)
        {
            return _approvalPanelRepository.GetFirstOrDefaultBy(c => c.Id == id,c=>c.ApprovalLevels.Select(d=>d.ApprovalLevelMembers));
        }

        public ICollection<ApprovalPanel> GetAll()
        {
            return _approvalPanelRepository.GetAll(c => c.ApprovalPanelType, c => c.RequisitionApprovalTickets, c => c.RequisitionApprovalTrackers,c=>c.ApprovalLevels);
        }

        public ICollection<ApprovalPanel> GetAllByPanelTypeId(int panelTypeId)
        {
            return _approvalPanelRepository.Get(c => c.ApprovalPanelTypeId == panelTypeId, c => c.ApprovalPanelType, c => c.RequisitionApprovalTickets, c => c.RequisitionApprovalTrackers,c=>c.ApprovalLevels).ToList();
        }

        public ApprovalPanel GetByApprovalPanelTypeIdAndApprovalPanelName(long approvalPanelTypeId,
            string approvalPanelName)
        {
            return _approvalPanelRepository.GetFirstOrDefaultBy(
                    c => c.ApprovalPanelTypeId == approvalPanelTypeId && c.Name.Equals(approvalPanelName),
                    c => c.ApprovalPanelType);
        }

        public bool IsApprovalPanelNameAvailable(ApprovalPanel entity)
        {
            if (entity.Id > 0)
            {
                var approvalPanel = GetByApprovalPanelTypeIdAndApprovalPanelName(entity.ApprovalPanelTypeId, entity.Name);
                if (approvalPanel == null)
                    return true;
                if (entity.Id == approvalPanel.Id && entity.Name == approvalPanel.Name)
                    return true;
                throw new BllException("Approval panel already exist.");
            }
            else
            {
                var approvalPanel = GetByApprovalPanelTypeIdAndApprovalPanelName(entity.ApprovalPanelTypeId, entity.Name);
                if (approvalPanel != null)
                {
                    throw new BllException("Approval panel already exist.");
                }
                return true;
            }
            
        }

        


        public ApprovalPanel GetApprovalPanel(AdvanceCategory category)
        {
            if (category ==null || category.Id<=0)
            {
                throw new BllException("No Category found while trying to get approval panel!");
            }

            var approvalPanel = GetApprovalPanel(category.Id);
                

            return approvalPanel;


        }

        
        public ApprovalPanel GetApprovalPanel(long categoryId)
        {
            if (categoryId  == 0)
            {
                throw new BllException("No Category found while trying to get approval panel!");
            }

           var approvalPanel=  _approvalPanelRepository.GetFirstOrDefaultBy(
                    c => c.AdvanceRequisitionCategories.Any(d => d.Id == categoryId),c=>c.ApprovalLevels);

            return approvalPanel;
        }

        public ICollection<ApprovalPanel> GetAllForTimeLagRequisition()
        {
            return _approvalPanelRepository.GetAll(c=>c.ApprovalLevels.Select(d=>d.ApprovalLevelMembers), c=>c.ApprovalLevels.Select(d=>d.RequisitionApprovalTrackers));
        }
    }
}

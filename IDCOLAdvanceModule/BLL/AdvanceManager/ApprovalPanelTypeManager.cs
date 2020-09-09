using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ApprovalPanelTypeManager : IApprovalPanelTypeManager
    {
        private IApprovalPanelTypeRepository _approvalPanelTypeRepository;

        public ApprovalPanelTypeManager()
        {
            _approvalPanelTypeRepository = new ApprovalPanelTypeRepository();
        }

        public ApprovalPanelTypeManager(IApprovalPanelTypeRepository approvalPanelTypeRepository)
        {
            _approvalPanelTypeRepository = approvalPanelTypeRepository;
        }
        public bool Insert(ApprovalPanelType entity)
        {
            entity.CreatedBy = Session.LoginUserName;
            entity.CreatedOn = DateTime.UtcNow.AddHours(6);
            IsApprovalPanelTypeNameAvailable(entity);
            return _approvalPanelTypeRepository.Insert(entity);
        }

        public bool Insert(ICollection<ApprovalPanelType> entityCollection)
        {
            entityCollection.ToList().ForEach(c => c.CreatedOn = DateTime.UtcNow.AddHours(6));
            return _approvalPanelTypeRepository.Insert(entityCollection);
        }

        public bool Edit(ApprovalPanelType entity)
        {
            entity.LastModifiedBy = Session.LoginUserName;
            entity.LastModifiedOn = DateTime.UtcNow.AddHours(6);
            IsApprovalPanelTypeNameAvailable(entity);
            return _approvalPanelTypeRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _approvalPanelTypeRepository.Delete(entity);
        }

        public ApprovalPanelType GetById(long id)
        {
            return _approvalPanelTypeRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<ApprovalPanelType> GetAll()
        {
            return _approvalPanelTypeRepository.GetAll();
        }

        public ApprovalPanelType GetByApprovalPanelTypeName(string approvalPanelTypeName)
        {
            return _approvalPanelTypeRepository.GetFirstOrDefaultBy(c => c.Name.Equals(approvalPanelTypeName));
        }

        public bool IsApprovalPanelTypeNameAvailable(ApprovalPanelType entity)
        {
            if (entity.Id > 0)
            {
                var approvalPanelType = GetByApprovalPanelTypeName(entity.Name);
                if (approvalPanelType == null)
                    return true;
                if (approvalPanelType.Id == entity.Id)
                    return true;
                throw new BllException("Panel type already exists.");
            }
            else
            {
                var approvalPanelType = GetByApprovalPanelTypeName(entity.Name);
                if (approvalPanelType != null)
                {
                    throw new BllException("Panel type already exists.");
                }
                return true;
            }
           
        }
    }
}

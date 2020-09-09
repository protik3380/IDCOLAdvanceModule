using System;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;
using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ApprovalLevelManager : IApprovalLevelManager
    {
        private readonly IApprovalLevelRepository _approvalLevelRepository;
        private readonly IApprovalPanelManager _approvalPanelManager;
        private readonly IAdvance_VW_GetHeadOfDepartmentManager _advanceVwGetHeadOfDepartmentManager;
        private readonly IAdvance_VW_GetApprovalLevelMemberManager _advanceVwGetApprovalLevelMemberManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        
        public ApprovalLevelManager()
        {
            _approvalLevelRepository = new ApprovalLevelRepository();
            _approvalPanelManager = new ApprovalPanelManager();
            _advanceVwGetHeadOfDepartmentManager = new AdvanceVwGetHeadOfDepartmentManager();
            _advanceVwGetApprovalLevelMemberManager = new AdvanceVwGetApprovalLevelMemberManager();
            _employeeManager = new EmployeeManager();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
        }

        public ApprovalLevelManager(IApprovalLevelRepository approvalLevelRepository, IApprovalPanelManager approvalPanelManager, IAdvance_VW_GetHeadOfDepartmentManager advanceVwGetHeadOfDepartmentManager, IAdvance_VW_GetApprovalLevelMemberManager advanceVwGetApprovalLevelMemberManager, IEmployeeManager employeeManager, IAdvanceRequisitionHeaderManager advanceRequisitionHeaderManager, IAdvanceExpenseHeaderManager advanceExpenseHeaderManager)
        {
            _approvalLevelRepository = approvalLevelRepository;
            _approvalPanelManager = approvalPanelManager;
            _advanceVwGetHeadOfDepartmentManager = advanceVwGetHeadOfDepartmentManager;
            _advanceVwGetApprovalLevelMemberManager = advanceVwGetApprovalLevelMemberManager;
            _employeeManager = employeeManager;
            _advanceRequisitionHeaderManager = advanceRequisitionHeaderManager;
            _advanceExpenseHeaderManager = advanceExpenseHeaderManager;
        }

        public bool Insert(ApprovalLevel entity)
        {
            IsLevelNameExist(entity);
            IsLevelOrderExist(entity);
            ValidateSourceOfFundEntry(entity);
            ValidateSourceOfFundVerify(entity);
            return _approvalLevelRepository.Insert(entity);
        }

        public bool Insert(ICollection<ApprovalLevel> entityCollection)
        {
            entityCollection.ToList().ForEach(c => c.CreatedOn = DateTime.UtcNow.AddHours(6));
            return _approvalLevelRepository.Insert(entityCollection);
        }

        public bool Edit(ApprovalLevel entity)
        {
            IsLevelNameExist(entity);
            IsLevelOrderExist(entity);
            ValidateSourceOfFundEntry(entity);
            ValidateSourceOfFundVerify(entity);
            return _approvalLevelRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            ApprovalLevel entity = GetById(id);
            return _approvalLevelRepository.Delete(entity);
        }

        public ApprovalLevel GetById(long id)
        {
            return _approvalLevelRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.ApprovalLevelMembers);
        }

        public ICollection<ApprovalLevel> GetAll()
        {
            return _approvalLevelRepository.GetAll(c => c.ApprovalLevelMembers);
        }

        public ICollection<ApprovalLevel> GetByPanelId(long panelId)
        {
            return _approvalLevelRepository.Get(c => c.ApprovalPanelId == panelId, c => c.ApprovalLevelMembers).OrderBy(c=>c.LevelOrder).ToList();
        }

        public bool IsLevelOrderExist(ApprovalLevel entity)
        {
            ApprovalLevel approvalLevel =
                _approvalLevelRepository.GetFirstOrDefaultBy(
                    c => c.ApprovalPanelId == entity.ApprovalPanelId && c.LevelOrder == entity.LevelOrder);
            if (entity.Id > 0)
            {
                if (approvalLevel == null)
                    return true;
                if (entity.Id == approvalLevel.Id && entity.LevelOrder == approvalLevel.LevelOrder)
                    return true;
                throw new BllException("This specific level order against this panel already exists.");
            }
            else
            {
                if (approvalLevel != null)
                {
                    throw new BllException("This specific level order against this panel already exists.");
                }
                return true;
            }
        }

        public bool IsLevelNameExist(ApprovalLevel entity)
        {
            ApprovalLevel approvalLevel =
                _approvalLevelRepository.GetFirstOrDefaultBy(c => c.ApprovalPanelId == entity.ApprovalPanelId && c.Name == entity.Name);
            if (entity.Id > 0)
            {
                if (approvalLevel == null)
                    return true;
                if (entity.Id == approvalLevel.Id && entity.Name.Equals(approvalLevel.Name))
                    return true;
                throw new BllException("This specific level name against this panel already exists.");
            }
            else
            {
                if (approvalLevel != null)
                {
                    throw new BllException("This specific level name against this panel already exists.");
                }
                return true;
            }
        }

        public bool UpdateDisplaySerial(List<ApprovalLevel> currentLevelItems)
        {
            return _approvalLevelRepository.UpdateDisplaySerial(currentLevelItems);
        }

        public ICollection<ApprovalLevel> GetLevelsOfMember(string memberUserName)
        {
            // find the levels which has this member
            var levelsAsMembers =
                _approvalLevelRepository.Get(
                    c => c.ApprovalLevelMembers.Where(d=>d.IsDeleted==false).Select(d => d.EmployeeUserName).Contains(memberUserName),
                    c => c.ApprovalLevelMembers).Distinct().ToList();

            return levelsAsMembers;
        }

        public ApprovalLevel GetLevelWithApprovalLevelMembersForRequsition(long approvalPanelId, long approvalLevelId, long requisitionHeaderId)
        {
            // find the levels which has this member
            var header = _advanceRequisitionHeaderManager.GetById(requisitionHeaderId);
            var approvalLevel = _approvalLevelRepository.GetFirstOrDefaultBy(c => c.Id == approvalLevelId && c.ApprovalPanelId == approvalPanelId, c => c.ApprovalLevelMembers, c => c.ApprovalPanel);
            var approvalLevelMembers = approvalLevel.ApprovalLevelMembers.Where(c=>c.IsDeleted==false).ToList();
           
            if (approvalLevel.IsLineSupervisor)
            {
                UserTable employee = new UserTable();
                employee = _employeeManager.GetById((long)header.RequesterSupervisorId);
                ApprovalLevelMember supervisor = new ApprovalLevelMember()
                {
                    EmployeeUserName = employee.UserName,
                };
                approvalLevelMembers.Add(supervisor);
            }
            if (approvalLevel.IsHeadOfDepartment)
            {
                Advance_VW_GetHeadOfDepartment departmentHeadOfDepartment = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(header.RequesterUserName);
                UserTable employee = new UserTable();
                if (departmentHeadOfDepartment == null)
                {
                    throw new Exception("No Department Head is set, please talk to IT Admin");
                }
                employee = _employeeManager.GetByUserName(departmentHeadOfDepartment.HeadOfDepartmentUserName);
                ApprovalLevelMember supervisor = new ApprovalLevelMember()
                {
                    EmployeeUserName = employee.UserName,
                };
                approvalLevelMembers.Add(supervisor);
            }
            approvalLevel.ApprovalLevelMembers = approvalLevelMembers;
            return approvalLevel;
        }

        public ApprovalLevel GetLevelWithApprovalLevelMembersForExpense(long approvalPanelId, long approvalLevelId, long expenseHeaderId)
        {
            // find the levels which has this member
            var header = _advanceExpenseHeaderManager.GetById(expenseHeaderId);
            var approvalLevel = _approvalLevelRepository.GetFirstOrDefaultBy(c => c.Id == approvalLevelId && c.ApprovalPanelId == approvalPanelId, c => c.ApprovalLevelMembers, c => c.ApprovalPanel);
            var approvalLevelMembers = approvalLevel.ApprovalLevelMembers.Where(c => c.IsDeleted == false).ToList();


            if (approvalLevel.IsLineSupervisor)
            {
                UserTable employee = new UserTable();
                employee = _employeeManager.GetById((long)header.RequesterSupervisorId);
                ApprovalLevelMember supervisor = new ApprovalLevelMember()
                {
                    EmployeeUserName = employee.UserName,
                };
                approvalLevelMembers.Add(supervisor);
            }
            if (approvalLevel.IsHeadOfDepartment)
            {
                Advance_VW_GetHeadOfDepartment departmentHeadOfDepartment = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(header.RequesterUserName);
                UserTable employee = new UserTable();
                if (departmentHeadOfDepartment == null)
                {
                    throw new Exception("No Department Head is set, please talk to IT Admin");
                }
                employee = _employeeManager.GetByUserName(departmentHeadOfDepartment.HeadOfDepartmentUserName);
                ApprovalLevelMember supervisor = new ApprovalLevelMember()
                {
                    EmployeeUserName = employee.UserName,
                };
                approvalLevelMembers.Add(supervisor);
            }
            approvalLevel.ApprovalLevelMembers = approvalLevelMembers;
            return approvalLevel;
        }

        public bool ValidateSourceOfFundEntry(ApprovalLevel entity)
        {
            if (entity.IsSourceOfFundEntry)
            {
                ICollection<ApprovalLevel> approvalLevels = GetByPanelId(entity.ApprovalPanelId);
                if (approvalLevels != null && approvalLevels.Any(c => c.IsSourceOfFundEntry))
                {
                    if (entity.Id > 0)
                    {
                        var approvalLevel = approvalLevels.FirstOrDefault(c => c.IsSourceOfFundEntry);
                        if (approvalLevel == null)
                        {
                            throw new BllException("Approval level null. Contact with admin."); 
                        }
                        if (approvalLevel.Id == entity.Id && approvalLevel.IsSourceOfFundEntry == entity.IsSourceOfFundEntry)
                        {
                            return true;
                        }
                        throw new BllException("Source of fund entry level is already set for this panel.");
                    }
                    else
                    {
                        throw new BllException("Source of fund entry level is already set for this panel.");
                    }
                }
            }
            return true;
        }

        public bool ValidateSourceOfFundVerify(ApprovalLevel entity)
        {
            var approvalLevels = GetByPanelId(entity.ApprovalPanelId).AsEnumerable();
            if (approvalLevels != null)
            {
                approvalLevels = approvalLevels.Where(c => c.LevelOrder <= entity.LevelOrder);
                if (!approvalLevels.Any(c => c.IsSourceOfFundEntry) && entity.IsSourceOfFundVerify && !entity.IsSourceOfFundEntry)
                {
                    throw new BllException("Source of fund entry level is not set yet for this panel.");
                }
            }
            return true;
        }
    }
}
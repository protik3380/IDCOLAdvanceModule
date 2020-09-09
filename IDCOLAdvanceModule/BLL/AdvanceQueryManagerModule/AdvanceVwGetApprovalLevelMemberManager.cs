using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.EntityModels.ViewModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IAdvanceQueryRepository;
using IDCOLAdvanceModule.Repository;
using IDCOLAdvanceModule.Repository.AdvanceQueryRepository;

namespace IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule
{
    public class AdvanceVwGetApprovalLevelMemberManager : IAdvance_VW_GetApprovalLevelMemberManager
    {
        private readonly IAdvance_VW_GetApprovalLevelMemberRepository _approvalLevelMemberRepository;
        private readonly IAdvanceRequisitionHeaderManager _advanceRequisitionHeaderManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAdvance_VW_GetHeadOfDepartmentManager _advanceVwGetHeadOfDepartmentManager;
        private readonly IDestinationUserForTicketManager _destinationUserForTicketManager;
        private readonly IApprovalTicketManager _approvalTicketManager;
        private readonly IAdvanceExpenseHeaderManager _advanceExpenseHeaderManager;
        private readonly IApprovalTicketRepository _approvalTicketRepository;

        public AdvanceVwGetApprovalLevelMemberManager()
        {
            _approvalLevelMemberRepository = new AdvanceVwGetApprovalLevelMember();
            _advanceRequisitionHeaderManager = new AdvanceRequistionManager();
            _advanceVwGetHeadOfDepartmentManager = new AdvanceVwGetHeadOfDepartmentManager();
            _destinationUserForTicketManager = new DestinationUserForTicketManager();
            _approvalTicketManager = new ApprovalTicketManager();
            _advanceExpenseHeaderManager = new AdvanceExpenseManager();
            _employeeManager = new EmployeeManager();
            _approvalTicketRepository = new ApprovalTicketRepository();
        }

        public AdvanceVwGetApprovalLevelMemberManager(IAdvance_VW_GetApprovalLevelMemberRepository approvalLevelMemberRepository, IAdvanceRequisitionHeaderManager advanceRequisitionHeaderManager, IEmployeeManager employeeManager, IAdvance_VW_GetHeadOfDepartmentManager advanceVwGetHeadOfDepartmentManager, IDestinationUserForTicketManager destinationUserForTicketManager, IApprovalTicketManager approvalTicketManager, IAdvanceExpenseHeaderManager advanceExpenseHeaderManager, IApprovalTicketRepository approvalTicketRepository)
        {
            _approvalLevelMemberRepository = approvalLevelMemberRepository;
            _advanceRequisitionHeaderManager = advanceRequisitionHeaderManager;
            _employeeManager = employeeManager;
            _advanceVwGetHeadOfDepartmentManager = advanceVwGetHeadOfDepartmentManager;
            _destinationUserForTicketManager = destinationUserForTicketManager;
            _approvalTicketManager = approvalTicketManager;
            _advanceExpenseHeaderManager = advanceExpenseHeaderManager;
            _approvalTicketRepository = approvalTicketRepository;
        }

        public ICollection<Advance_VW_GetApprovalLevelMember> GetAll(params Expression<Func<Advance_VW_GetApprovalLevelMember, object>>[] includes)
        {
            return _approvalLevelMemberRepository.GetAll(includes);
        }

        public ICollection<Advance_VW_GetApprovalLevelMember> Get(Expression<Func<Advance_VW_GetApprovalLevelMember, bool>> predicate, params Expression<Func<Advance_VW_GetApprovalLevelMember, object>>[] includes)
        {
            return _approvalLevelMemberRepository.Get(predicate, includes);
        }

        public Advance_VW_GetApprovalLevelMember GetFirstOrDefaultBy(Expression<Func<Advance_VW_GetApprovalLevelMember, bool>> predicate, params Expression<Func<Advance_VW_GetApprovalLevelMember, object>>[] includes)
        {
            return _approvalLevelMemberRepository.GetFirstOrDefaultBy(predicate, includes);
        }

        public ICollection<Advance_VW_GetApprovalLevelMember> GetByApprovalLevel(long id)
        {
            return _approvalLevelMemberRepository.Get(c => c.ApprovalLevelId == id && !c.IsDeleted);
        }

        public ICollection<Advance_VW_GetApprovalLevelMember> GetApprovalLevelMembers(long ticketId, string approvalUserName = null)
        {
            var approvalTicket = _approvalTicketManager.GetById(ticketId);
            if (approvalTicket == null)
            {
                throw new BllException("Approval ticket not found.");
            }
            string requesterUserName;
            decimal? requesterSupervisorId = null;

            //getting requesterUserName and requesterSupervisorId
            if (approvalTicket.TicketTypeId == (long)TicketTypeEnum.Requisition)
            {
                var requisitionApprovalTicket = approvalTicket as RequisitionApprovalTicket;
                if (requisitionApprovalTicket == null)
                {
                    throw new BllException("Requisition ticket not found.");
                }
                var requisitionHeader = _advanceRequisitionHeaderManager.GetById(requisitionApprovalTicket.AdvanceRequisitionHeaderId);
                requesterUserName = requisitionHeader.RequesterUserName;
                if (requisitionHeader.RequesterSupervisorId != null)
                {
                    requesterSupervisorId = (decimal)requisitionHeader.RequesterSupervisorId;
                }
            }
            else
            {
                var expenseApprovalTicket = approvalTicket as ExpenseApprovalTicket;
                if (expenseApprovalTicket == null)
                {
                    throw new BllException("Expense ticket not found.");
                }
                var advanceExpenseHeader = _advanceExpenseHeaderManager.GetById(expenseApprovalTicket.AdvanceExpenseHeaderId);
                requesterUserName = advanceExpenseHeader.RequesterUserName;
                if (advanceExpenseHeader.RequesterSupervisorId != null)
                {
                    requesterSupervisorId = (decimal)advanceExpenseHeader.RequesterSupervisorId;
                }
            }

            EmployeeVM employeeVm = _employeeManager.GetDetailInformation(requesterUserName);

            var approvalLevel = approvalTicket.ApprovalLevel;
            var approvalLevelMembers = GetByApprovalLevel(approvalLevel.Id);
            //var approvalLevelMemberList = approvalLevelMembers == null ? new List<Advance_VW_GetApprovalLevelMember>() : approvalLevelMembers.ToList();
            var approvalLevelMemberList = new List<Advance_VW_GetApprovalLevelMember>();

            //getting line supervisor information

            //if (approvalLevel.IsLineSupervisor)
            //{
            //    if (requesterSupervisorId == null)
            //        throw new BllException("Supervisor id not found.");
            //    UserTable employee = _employeeManager.GetById((long)requesterSupervisorId);
            //    Advance_VW_GetApprovalLevelMember supervisor = new Advance_VW_GetApprovalLevelMember()
            //    {
            //        EmployeeID = employee.EmployeeID,
            //        EmployeeFullName = employee.FullName,
            //        RankName = employee.Admin_Rank.RankName,
            //        EmployeeUserName = employee.UserName,
            //        EmployeeEmail = employee.PrimaryEmail
            //    };
            //    approvalLevelMemberList.Add(supervisor);
            //}
            //getting department head information
            //if (approvalLevel.IsHeadOfDepartment)
            //{
            //    Advance_VW_GetHeadOfDepartment departmentHeadOfDepartment = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(employeeVm.EmployeeUserName);

            //    if (departmentHeadOfDepartment == null)
            //    {
            //        throw new Exception("No Department Head is set, please talk to IT Admin");
            //    }
            //    UserTable employee = _employeeManager.GetByUserName(departmentHeadOfDepartment.HeadOfDepartmentUserName);
            //    Advance_VW_GetApprovalLevelMember deptOfHead = new Advance_VW_GetApprovalLevelMember
            //    {
            //        EmployeeID = employee.EmployeeID,
            //        EmployeeFullName = employee.FullName,
            //        RankName = employee.Admin_Rank.RankName,
            //        EmployeeUserName = employee.UserName,
            //        EmployeeEmail =  employee.PrimaryEmail
            //    };
            //    approvalLevelMemberList.Add(deptOfHead);
            //}
            //getting dilute member information
            //var diluteMembers = _destinationUserForTicketManager.GetBy(approvalTicket.Id, approvalTicket.ApprovalLevelId,
            //    approvalTicket.ApprovalPanelId);
            var diluteMembers = new List<DestinationUserForTicket>();
            var destinationUser = _destinationUserForTicketManager.GetLastDestinationUser(approvalTicket.Id, approvalTicket.ApprovalLevelId, approvalTicket.ApprovalPanelId);

            if (destinationUser != null)
            {
                diluteMembers.Add(destinationUser);
            }
            foreach (DestinationUserForTicket userForTicket in diluteMembers)
            {
                var employee = _employeeManager.GetByUserName(userForTicket.DestinationUserName);
                Advance_VW_GetApprovalLevelMember diluteMember = new Advance_VW_GetApprovalLevelMember
                {
                    EmployeeID = employee.EmployeeID,
                    EmployeeFullName = employee.FullName,
                    RankName = employee.Admin_Rank.RankName,
                    EmployeeUserName = employee.UserName,
                    EmployeeEmail = employee.PrimaryEmail
                };
                approvalLevelMemberList.Add(diluteMember);
            }

            //excluding if requester is also a approval level member
            if (!string.IsNullOrEmpty(requesterUserName))
            {
                approvalLevelMemberList =
                    approvalLevelMemberList.Where(c => !c.EmployeeUserName.Equals(requesterUserName)).ToList();
            }
            return approvalLevelMemberList;
        }

        public bool IsValidForMember(ApplicationNotification notification, string approvalAuthorityUserName)
        {
            var ticket = _approvalTicketRepository.GetFirstOrDefaultBy(c => c.Id == notification.ApprovalTicketId,
                c => c.ApprovalLevel, c => c.ApprovalStatus, c => c.ApprovalPanel);
            if (notification.ApprovalLevelId != ticket.ApprovalLevelId)
            {
                return false;
            }
            var approvalLevelMembers = GetApprovalLevelMembers(ticket.Id,
                    ticket.AuthorizedBy).ToList();
            if (approvalLevelMembers.Any(c => c.EmployeeUserName.ToLower().Contains(approvalAuthorityUserName.ToLower())))
            {
                return true;
            }
            return false;
        }

        public bool CheckLevelMemberExists(long nextLevelId, long ticketId)
        {
            ApprovalLevelManager approvalLevelManager = new ApprovalLevelManager();
            var approvalLevel = approvalLevelManager.GetById(nextLevelId);
            if (approvalLevel == null)
            {
                throw new BllException("Next approval level not found.");
            }
            var approvalTicket = _approvalTicketManager.GetById(ticketId);
            if (approvalTicket == null)
            {
                throw new BllException("Approval ticket not found.");
            }

            string requesterUsername = null;
            decimal? requesterSupervisorId = null;

            if (approvalTicket.TicketTypeId == (long)TicketTypeEnum.Requisition)
            {
                var requisitionApprovalTicket = approvalTicket as RequisitionApprovalTicket;
                if (requisitionApprovalTicket == null)
                {
                    throw new BllException("Requisition ticket not found.");
                }
                var requisitionHeader = _advanceRequisitionHeaderManager.GetById(requisitionApprovalTicket.AdvanceRequisitionHeaderId);
                requesterUsername = requisitionHeader.RequesterUserName;
                if (requisitionHeader.RequesterSupervisorId != null)
                {
                    requesterSupervisorId = requisitionHeader.RequesterSupervisorId;
                }
            }
            else
            {
                var expenseApprovalTicket = approvalTicket as ExpenseApprovalTicket;
                if (expenseApprovalTicket == null)
                {
                    throw new BllException("Expense ticket not found.");
                }
                var expenseHeader = _advanceExpenseHeaderManager.GetById(expenseApprovalTicket.AdvanceExpenseHeaderId);
                requesterUsername = expenseHeader.RequesterUserName;
                if (expenseHeader.RequesterSupervisorId != null)
                {
                    requesterSupervisorId = expenseHeader.RequesterSupervisorId;
                }
            }

            if (approvalLevel.IsLineSupervisor)
            {
                if (requesterSupervisorId == null)
                {
                    throw new BllException("Supervisor id not found.");
                }
            }

            else if (approvalLevel.IsHeadOfDepartment)
            {
                Advance_VW_GetHeadOfDepartment headOfDepartment = _advanceVwGetHeadOfDepartmentManager.GetHeadOfDepartmentOfAnEmployee(requesterUsername);

                if (headOfDepartment == null)
                {
                    throw new Exception("No Department Head is set, please talk to IT Admin");
                }
            }
            else
            {
                if (approvalLevel.ApprovalLevelMembers == null || approvalLevel.ApprovalLevelMembers.All(c => c.IsDeleted))
                {
                    throw new BllException("Sorry! No level member is set in the level " + approvalLevel.Name + ". Please contact with admin.");
                }
            }
            return true;
        }
    }
}

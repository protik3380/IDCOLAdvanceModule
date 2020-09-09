using System.Collections.Generic;
using System.Linq;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository.IMISRepository;
using IDCOLAdvanceModule.Repository;
using IDCOLAdvanceModule.Repository.MISRepository;
using IDCOLAdvanceModule.Context.AdvanceModuleQueryContext;
using IDCOLAdvanceModule.Context.MISContext;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Base;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Extension;
using IDCOLAdvanceModule.Model.Interfaces.IManager;

namespace IDCOLAdvanceModule.BLL.AdvanceManager.NotificationManagment
{
    public class NotificationManager
    {
        private readonly IApprovalLevelRepository _levelRepository;
        private readonly IAdvanceRequisitionHeaderRepository _requisitionHeaderRepository;
        private readonly IAdvanceExpenseHeaderRepository _expenseHeaderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IApplicationNotificationRepository _applicationNotificationRepository;
        private readonly IAdvance_VW_GetApprovalLevelMemberManager _advanceVwGetApprovalLevelMemberManager;
        private readonly IRequisitionApprovalTicketRepository _requiistionApprovalTicketRepository;
        private readonly IExpenseApprovalTicketRepository _expenseApprovalTicketRepository;
        private readonly IApplicationNotificationManager _applicationNotificationManager;
        private readonly IEmailNotificationManager _emailNotificationManager;

        public NotificationManager()
        {
            _levelRepository = new ApprovalLevelRepository();
            _requisitionHeaderRepository = new AdvanceRequisitionHeaderRepository();
            _employeeRepository = new EmployeeRepository();
            _applicationNotificationRepository = new ApplicationNotificationRepository();
            _advanceVwGetApprovalLevelMemberManager = new AdvanceVwGetApprovalLevelMemberManager();
            _expenseHeaderRepository = new AdvanceExpenseHeaderRepository();
            _requiistionApprovalTicketRepository = new RequisitionApprovalTicketRepository();
            _expenseApprovalTicketRepository = new ExpenseApprovalTicketRepository();
            _applicationNotificationManager = new ApplicationNotificationManager();
            _emailNotificationManager = new EmailNotificationManager();
        }

        public NotificationManager(IApprovalLevelRepository levelRepository, IAdvanceRequisitionHeaderRepository requisitionHeaderRepository, IAdvanceExpenseHeaderRepository expenseHeaderRepository, IEmployeeRepository employeeRepository, IApplicationNotificationRepository applicationNotificationRepository, IAdvance_VW_GetApprovalLevelMemberManager advanceVwGetApprovalLevelMemberManager, IRequisitionApprovalTicketRepository requiistionApprovalTicketRepository, IExpenseApprovalTicketRepository expenseApprovalTicketRepository, IApplicationNotificationManager applicationNotificationManager, IEmailNotificationManager emailNotificationManager)
        {
            _levelRepository = levelRepository;
            _requisitionHeaderRepository = requisitionHeaderRepository;
            _expenseHeaderRepository = expenseHeaderRepository;
            _employeeRepository = employeeRepository;
            _applicationNotificationRepository = applicationNotificationRepository;
            _advanceVwGetApprovalLevelMemberManager = advanceVwGetApprovalLevelMemberManager;
            _requiistionApprovalTicketRepository = requiistionApprovalTicketRepository;
            _expenseApprovalTicketRepository = expenseApprovalTicketRepository;
            _applicationNotificationManager = applicationNotificationManager;
            _emailNotificationManager = emailNotificationManager;
        }

        public ICollection<ApplicationNotification> GetApplicationNotifications(ApprovalTicket approvalTicket)
        {
            if (approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Approved || approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval || approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted || approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Rejected)
            {
                var requesterInfo =
                     _employeeRepository.GetFirstOrDefaultBy(c => c.UserName.ToLower().Equals(approvalTicket.RequesterUserName.ToLower()));
                var approvalAuthorityInfo =
                  _employeeRepository.GetFirstOrDefaultBy(c => c.UserName.ToLower().Equals(approvalTicket.AuthorizedBy.ToLower()));
                ICollection<ApplicationNotification> notificationList = new List<ApplicationNotification>();

                var notifyingUserList = GetNotifyingUserList(approvalTicket, requesterInfo);

                if (notifyingUserList.Any())
                {
                    notificationList = approvalTicket.GetNotification(notifyingUserList.Distinct(), requesterInfo, approvalAuthorityInfo);
                }

                return notificationList;
            }

            return null;
        }

        private ICollection<Advance_VW_GetApprovalLevelMember> GetNotifyingUserList(ApprovalTicket approvalTicket, UserTable requesterInfo)
        {      
            List<Advance_VW_GetApprovalLevelMember> notifyingUsers = new List<Advance_VW_GetApprovalLevelMember>();

            var creatorUserName = approvalTicket.GetCreatorUserName();
            if (creatorUserName == null)
            {
                throw new BllException("Creator information not found.");
            }
            var creatorInfo = _employeeRepository.GetFirstOrDefaultBy(c => c.UserName.ToLower().Equals(creatorUserName));
            if (creatorInfo == null)
            {
                throw new BllException("Creator information not found in user table.");
            }

            var creatorNotifyingUser = new Advance_VW_GetApprovalLevelMember()
            {
                EmployeeID = creatorInfo.EmployeeID,
                EmployeeFullName = creatorInfo.FullName,
                RankName = creatorInfo.Admin_Rank.RankName,
                EmployeeUserName = creatorInfo.UserName,
                EmployeeEmail = creatorInfo.PrimaryEmail
            };
            notifyingUsers.Add(creatorNotifyingUser);

            if (approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval)
            {
                notifyingUsers = _advanceVwGetApprovalLevelMemberManager.GetApprovalLevelMembers(approvalTicket.Id,
                    approvalTicket.AuthorizedBy).ToList();
                notifyingUsers =
                    notifyingUsers.Where(c => !c.EmployeeUserName.ToLower().Contains(requesterInfo.UserName.ToLower()))
                        .ToList();
            }

            if (approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Approved)
            {
                var notifyingUser = new Advance_VW_GetApprovalLevelMember()
                {
                    EmployeeID = requesterInfo.EmployeeID,
                    EmployeeFullName = requesterInfo.FullName,
                    RankName = requesterInfo.Admin_Rank.RankName,
                    EmployeeUserName = requesterInfo.UserName,
                    EmployeeEmail = requesterInfo.PrimaryEmail
                };
                notifyingUsers.Add(notifyingUser);
            }
            if (approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Rejected)
            {
                notifyingUsers.AddRange(GetAuthorizedNotifyingUserList(approvalTicket, requesterInfo));
            }
            if (approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted)
            {
                notifyingUsers.AddRange(GetAuthorizedNotifyingUserList(approvalTicket, requesterInfo));
            }
            return notifyingUsers.DistinctBy(c=>c.EmployeeUserName).ToList();
        }  

        private ICollection<Advance_VW_GetApprovalLevelMember> GetAuthorizedNotifyingUserList(ApprovalTicket ticket, UserTable requesterInfo)
        {
            ICollection<Advance_VW_GetApprovalLevelMember> notifyingUsers = new List<Advance_VW_GetApprovalLevelMember>();

            var authorizeduserList = ticket.GetAuthorizedUserName();
            if (authorizeduserList.Any())
            {
                foreach (string userName in authorizeduserList)
                {
                    UserTable employee =
                        _employeeRepository.GetFirstOrDefaultBy(c => c.UserName.ToLower().Equals(userName.ToLower()));
                    if (employee == null)
                    {
                        continue;
                    }
                    var notifyingUser = new Advance_VW_GetApprovalLevelMember()
                    {
                        EmployeeID = employee.EmployeeID,
                        EmployeeFullName = employee.FullName,
                        RankName = employee.Admin_Rank.RankName,
                        EmployeeUserName = employee.UserName,
                        EmployeeEmail = employee.PrimaryEmail
                    };
                    notifyingUsers.Add(notifyingUser);
                }
            }
            var requesterUser = new Advance_VW_GetApprovalLevelMember()
            {
                EmployeeID = requesterInfo.EmployeeID,
                EmployeeFullName = requesterInfo.FullName,
                RankName = requesterInfo.Admin_Rank.RankName,
                EmployeeUserName = requesterInfo.UserName,
                EmployeeEmail = requesterInfo.PrimaryEmail
            };
            notifyingUsers.Add(requesterUser);
            return notifyingUsers;
        }

        public bool Notify(ApprovalTicket approvalTicket)
        {
            bool isApplicationNotificationCreated = false;
            bool isEmailNotificationCreated = false;

            if (approvalTicket == null)
            {
                return false;
            }
            ApprovalTicket ticket = null;

            if (approvalTicket.TicketTypeId == (long)TicketTypeEnum.Requisition)
            {
                ticket = _requiistionApprovalTicketRepository.GetFirstOrDefaultBy(c => c.Id == approvalTicket.Id,
                    c => c.AdvanceRequisitionHeader, c => c.ApprovalTrackers, c => c.ApprovalLevel, c => c.ApprovalPanel,
                    c => c.ApprovalStatus);
            }

            if (approvalTicket.TicketTypeId == (long)TicketTypeEnum.Expense)
            {
                ticket = _expenseApprovalTicketRepository.GetFirstOrDefaultBy(c => c.Id == approvalTicket.Id,
                  c => c.AdvanceExpenseHeader, c => c.ApprovalTrackers, c => c.ApprovalLevel, c => c.ApprovalPanel,
                  c => c.ApprovalStatus);
            }

            var applicationNotifications = GetApplicationNotifications(ticket);
            var emailNotifications = GetEmailNotifications(ticket);

            if (applicationNotifications != null && applicationNotifications.Any())
            {
                isApplicationNotificationCreated = _applicationNotificationManager.Insert(applicationNotifications);
            }

            if (emailNotifications != null && emailNotifications.Any())
            {
                isEmailNotificationCreated = _emailNotificationManager.Insert(emailNotifications);
            }

            _emailNotificationManager.SendEmail();

            return isApplicationNotificationCreated || isEmailNotificationCreated;
        }

        private ICollection<EmailNotification> GetEmailNotifications(ApprovalTicket approvalTicket)
        {
            if (approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Approved || approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.WaitingForApproval || approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted || approvalTicket.ApprovalStatusId == (long)ApprovalStatusEnum.Rejected)
            {
                var requesterInfo =
                    _employeeRepository.GetFirstOrDefaultBy(c => c.UserName == approvalTicket.RequesterUserName);
                var approvalAuthorityInfo =
                  _employeeRepository.GetFirstOrDefaultBy(c => c.UserName == approvalTicket.AuthorizedBy);
                ICollection<EmailNotification> notificationList = new List<EmailNotification>();

                var notifyingUserList = GetNotifyingUserList(approvalTicket, requesterInfo);

                if (notifyingUserList.Any())
                {
                    notificationList = approvalTicket.GetEmail(notifyingUserList.Distinct(), requesterInfo, approvalAuthorityInfo);
                }

                return notificationList;
            }

            return null;
        }
    }
}

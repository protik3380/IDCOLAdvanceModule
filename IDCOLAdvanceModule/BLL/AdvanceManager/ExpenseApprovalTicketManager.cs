using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using IDCOLAdvanceModule.BLL.AdvanceManager.NotificationManagment;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ExpenseApprovalTicketManager : IExpenseApprovalTicketManager
    {
        private readonly IHeadOfDepartmentManager _headOfDepartmentManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IExpenseApprovalTicketRepository _expenseApprovalTicketRepository;
        private readonly IExpenseApprovalTrackerManager _expenseApprovalTrackerManager;
        private readonly IApprovalLevelManager _approvalLevelManager;
        private readonly IDestinationUserForTicketRepository _destinationUserForTicketRepository;
        private NotificationManager _notificationManager;


        public ExpenseApprovalTicketManager()
        {
            _expenseApprovalTicketRepository = new ExpenseApprovalTicketRepository();
            _expenseApprovalTrackerManager = new ExpenseApprovalTrackerManager();
            _approvalLevelManager = new ApprovalLevelManager();
            _headOfDepartmentManager = new HeadOfDepartmentManager();
            _employeeManager = new EmployeeManager();
            _destinationUserForTicketRepository = new DestinationUserForTicketRepository();
            _notificationManager = new NotificationManager();
        }

        public ExpenseApprovalTicketManager(IHeadOfDepartmentManager headOfDepartmentManager, IEmployeeManager employeeManager, IExpenseApprovalTicketRepository expenseApprovalTicketRepository, IExpenseApprovalTrackerManager expenseApprovalTrackerManager, IApprovalLevelManager approvalLevelManager, IDestinationUserForTicketRepository destinationUserForTicketRepository)
        {
            _headOfDepartmentManager = headOfDepartmentManager;
            _employeeManager = employeeManager;
            _expenseApprovalTicketRepository = expenseApprovalTicketRepository;
            _expenseApprovalTrackerManager = expenseApprovalTrackerManager;
            _approvalLevelManager = approvalLevelManager;
            _destinationUserForTicketRepository = destinationUserForTicketRepository;
        }

        public bool Insert(ExpenseApprovalTicket entity)
        {
            var trackers = entity.ExpenseApprovalTrackers.Where(c => c.Id == 0).ToList();
            entity.ExpenseApprovalTrackers = null;
            using (var ts = new TransactionScope())
            {
                bool isInsert = _expenseApprovalTicketRepository.Insert(entity);
                if (isInsert && trackers != null && trackers.Any())
                {
                    trackers.ForEach(c => c.ApprovalTicketId = entity.Id);
                    _expenseApprovalTrackerManager.Insert(trackers);  
                }

                ts.Complete();
                return isInsert;  
            }
        }

        public bool Insert(ICollection<ExpenseApprovalTicket> entityCollection)
        {
            int successCount = 0;
            if (entityCollection != null && entityCollection.Any())
            {
                foreach (var expenseApprovalTicket in entityCollection)
                {
                    bool isInserted = Insert(expenseApprovalTicket);
                    if (isInserted)
                    {
                        successCount++;
                    }
                }

                return successCount > 0 && successCount == entityCollection.Count;

            }
            return false;

        }

        public bool Edit(ExpenseApprovalTicket entity)
        {
            ICollection<ExpenseApprovalTracker> trackers = null;
            if (entity.ExpenseApprovalTrackers != null)
            {
                trackers = entity.ExpenseApprovalTrackers.Where(c => c.Id == 0).ToList();
            }
            if (entity.DestinationUserForTickets == null)
            {
                entity.DestinationUserForTickets = new List<DestinationUserForTicket>();
            }
            var updatedDesignationForTickets = entity.DestinationUserForTickets.ToList();
            var destinationUserForTickets = entity.DestinationUserForTickets;

            entity.ExpenseApprovalTrackers = null;
            entity.DestinationUserForTickets = null;
            var existingDesignationUserForTicket = _destinationUserForTicketRepository.Get(c => c.ApprovalLevelId == entity.ApprovalLevelId && c.ApprovalPanelId == entity.ApprovalPanelId && c.ApprovalTicketId == entity.Id);

            var addeableItems = updatedDesignationForTickets.Where(c => c.Id == 0
                                                                        &&
                                                                        !existingDesignationUserForTicket.Select(
                                                                            d => d.DestinationUserName)
                                                                            .Contains(c.DestinationUserName)).ToList();
                //&& !existingDesignationUserForTicket.Select(d => d.ApprovalLevelId).Contains(c.ApprovalLevelId)
                //&& !existingDesignationUserForTicket.Select(d => d.ApprovalPanelId).Contains(c.ApprovalPanelId)).ToList();
            //using (TransactionScope ts = new TransactionScope())
            //{
            bool isTicketUpdated = _expenseApprovalTicketRepository.Edit(entity);

            if (isTicketUpdated && trackers != null && trackers.Any())
            {
                bool isTrackerUpdated = _expenseApprovalTrackerManager.Insert(trackers);
            }

            int deleteCount = 0, updateCount = 0;
            bool isDeleted = false, isAdded = false, isUpdated = false;  

            if (addeableItems != null && addeableItems.Any())
            {
                isAdded = _destinationUserForTicketRepository.Insert(addeableItems);
            }
            bool isNotificationSent = _notificationManager.Notify(entity);

            //ts.Complete();

            return isTicketUpdated || isAdded || isNotificationSent;
            //}
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _expenseApprovalTicketRepository.Delete(entity);
        }

        public ExpenseApprovalTicket GetById(long id)
        {
            return _expenseApprovalTicketRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.ApprovalTrackers,
                c => c.AdvanceExpenseHeader, c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.ApprovalStatus,
                        c => c.DestinationUserForTickets);
        }

        public ICollection<ExpenseApprovalTicket> GetAll()
        {
            return _expenseApprovalTicketRepository.GetAll(c => c.ExpenseApprovalTrackers,
                c => c.AdvanceExpenseHeader, c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.ApprovalStatus,
                        c => c.DestinationUserForTickets);
        }

        public ExpenseApprovalTicket GetByExpenseHeaderId(long id)
        {
            return _expenseApprovalTicketRepository.GetFirstOrDefaultBy(c => c.AdvanceExpenseHeaderId == id,
                c => c.ApprovalTrackers,
                c => c.AdvanceExpenseHeader, c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.ApprovalStatus,
                        c => c.DestinationUserForTickets);
        }

        public ICollection<ExpenseApprovalTicket> GetExpenseTicketsForRequester(string requesterUserName)
        {
            throw new NotImplementedException();
        }

        public ICollection<ExpenseApprovalTicket> GetTicketsForUnitHead(string memberUserName, ApprovalStatusEnum approvalStatus)
        {
            var userInfo = _employeeManager.GetByUserName(memberUserName);

            return
                _expenseApprovalTicketRepository.Get(
                    c =>
                        c.ApprovalLevel.IsLineSupervisor &&
                        c.AdvanceExpenseHeader.RequesterSupervisorId == userInfo.UserId &&
                        c.ApprovalStatusId == (long)approvalStatus, c => c.ApprovalLevel.ApprovalLevelMembers,
                    c => c.AdvanceExpenseHeader.AdvanceExpenseDetails, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }
        public ICollection<ExpenseApprovalTicket> GetRevertedTicketsForRequester(string requesterUserName)
        {
            return
                _expenseApprovalTicketRepository.Get(
                    c => c.RequesterUserName.Equals(requesterUserName) && c.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted,
                    c => c.DestinationUserForTickets, c => c.AdvanceExpenseHeader.AdvanceExpenseDetails,
                    c => c.ApprovalLevel.ApprovalLevelMembers, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ICollection<ExpenseApprovalTicket> GetTicketsForDeptHead(string memberUserName, ApprovalStatusEnum approvalStatus)
        {
            var deptId = _headOfDepartmentManager.GetDepartmentIdByUserName(memberUserName);

            if (deptId == null)
            {
                return null;
            }

            return _expenseApprovalTicketRepository.Get(
                  c => c.ApprovalLevel.IsHeadOfDepartment &&
                      c.ApprovalStatusId == (int)approvalStatus
                      && deptId.Contains((decimal)c.AdvanceExpenseHeader.RequesterDepartmentId),
                      c => c.ApprovalLevel.ApprovalLevelMembers,
                      c => c.AdvanceExpenseHeader.AdvanceExpenseDetails,
                      c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ICollection<ExpenseApprovalTicket> GetTicketsForDestinationMember(string memberUserName, ApprovalStatusEnum approvalStatus)
        {
            return
                _expenseApprovalTicketRepository.Get(
                    c => c.DestinationUserForTickets.Any(d => d.DestinationUserName == memberUserName &&
                        c.ApprovalStatusId == (int)approvalStatus),
                    c => c.DestinationUserForTickets, c => c.AdvanceExpenseHeader.AdvanceExpenseDetails,
                    c => c.ApprovalLevel.ApprovalLevelMembers, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }
        public ICollection<ExpenseApprovalTicket> GetTicketsForMemberWithoutSpecificDestinationMember(string memberUserName,
         ApprovalStatusEnum approvalStatus)
        {
            var levelsOfMember = _approvalLevelManager.GetLevelsOfMember(memberUserName);

            if (levelsOfMember == null || !levelsOfMember.Any())
            {
                return null;
            }
            var levelOfMemberIdList = levelsOfMember.Select(c => c.Id).ToList();
            return
                _expenseApprovalTicketRepository.Get(
                    c =>
                        levelOfMemberIdList.Contains(c.ApprovalLevelId)
                        && c.ApprovalStatusId == (long)approvalStatus,

                        c => c.ApprovalLevel.ApprovalLevelMembers,
                        c => c.AdvanceExpenseHeader.AdvanceExpenseDetails,
                        c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ExpenseApprovalTicket GetByAdvanceExpenseHeaderIdWithNotApproved(long expneseHeaderId)
        {
            return
                _expenseApprovalTicketRepository.GetFirstOrDefaultBy(
                    c =>
                        c.AdvanceExpenseHeaderId == expneseHeaderId &&
                        c.ApprovalStatusId != (long)ApprovalStatusEnum.Approved,
                        c => c.AdvanceExpenseHeader,
                        c => c.DestinationUserForTickets,
                        c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.ApprovalStatus);
        }

        public DateTime? GetAuthorizedOnById(long? expenseApprovalTicketId)
        {
            DateTime? authorizedOn = null;
            if (expenseApprovalTicketId != null)
            {
                var ticket = GetById((long)expenseApprovalTicketId);
                if (ticket != null)
                    authorizedOn = ticket.AuthorizedOn;
            }
            return authorizedOn;
        }

        public IEnumerable<ExpenseApprovalTicket> GetTicketsForDiluteMember(string memberUserName, ApprovalStatusEnum approvaStatus)
        {
            return
               _expenseApprovalTicketRepository.Get(
                   c =>
                       c.ApprovalStatusId == (long)approvaStatus &&
                       c.DestinationUserForTickets
                                   .Any(d => d.DestinationUserName.Equals(memberUserName)
                                       && d.ApprovalLevelId == c.ApprovalLevelId
                                       && c.ApprovalPanelId == d.ApprovalPanelId),

                       c => c.DestinationUserForTickets).ToList();
        }
    }
}
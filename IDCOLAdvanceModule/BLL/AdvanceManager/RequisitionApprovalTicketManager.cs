using System;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.BLL.MISManager;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using IDCOLAdvanceModule.BLL.AdvanceManager.NotificationManagment;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;

namespace IDCOLAdvanceModule.BLL
{
    class RequisitionApprovalTicketManager : IRequisitionApprovalTicketManager
    {

        private readonly IHeadOfDepartmentManager _headOfDepartmentManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IRequisitionApprovalTicketRepository _requisitionApprovalTicketRepository;
        private readonly IRequisitionApprovalTrackerManager _requisitionApprovalTrackerManager;
        private readonly IApprovalLevelManager _approvalLevelManager;
        private readonly IDestinationUserForTicketRepository _destinationUserForTicketRepository;
        private NotificationManager _notificationManager;

        public RequisitionApprovalTicketManager()
        {
            _requisitionApprovalTicketRepository = new RequisitionApprovalTicketRepository();
            _headOfDepartmentManager = new HeadOfDepartmentManager();
            _employeeManager = new EmployeeManager();
            _approvalLevelManager = new ApprovalLevelManager();
            _requisitionApprovalTrackerManager = new RequisitionApprovalTrackerManager();
            _destinationUserForTicketRepository = new DestinationUserForTicketRepository();
            _notificationManager = new NotificationManager();
        }

        public RequisitionApprovalTicketManager(IHeadOfDepartmentManager headOfDepartmentManager, IEmployeeManager employeeManager, IRequisitionApprovalTicketRepository requisitionApprovalTicketRepository, IRequisitionApprovalTrackerManager requisitionApprovalTrackerManager, IApprovalLevelManager approvalLevelManager, IDestinationUserForTicketRepository destinationUserForTicketRepository)
        {
            _headOfDepartmentManager = headOfDepartmentManager;
            _employeeManager = employeeManager;
            _requisitionApprovalTicketRepository = requisitionApprovalTicketRepository;
            _requisitionApprovalTrackerManager = requisitionApprovalTrackerManager;
            _approvalLevelManager = approvalLevelManager;
            _destinationUserForTicketRepository = destinationUserForTicketRepository;
        }

        public bool Insert(RequisitionApprovalTicket entity)
        {
            var trackers = entity.RequisitionApprovalTrackers.Where(c => c.Id == 0).ToList();
            entity.RequisitionApprovalTrackers = null;

            using (var ts = new TransactionScope())
            {
                bool isInsert = _requisitionApprovalTicketRepository.Insert(entity);
                if (isInsert && trackers != null && trackers.Any())
                {
                    trackers.ForEach(c => c.ApprovalTicketId = entity.Id);
                    _requisitionApprovalTrackerManager.Insert(trackers);
                }

                ts.Complete();
                return isInsert;
            }
        }

        public bool Insert(ICollection<RequisitionApprovalTicket> entityCollection)
        {
            return _requisitionApprovalTicketRepository.Insert(entityCollection);
        }

        public bool Edit(RequisitionApprovalTicket entity)
        {
            ICollection<RequisitionApprovalTracker> trackers = null;
            if (entity.RequisitionApprovalTrackers != null)
            {
                trackers = entity.RequisitionApprovalTrackers.Where(c => c.Id == 0).ToList();
            }
            if (entity.DestinationUserForTickets == null)
            {
                entity.DestinationUserForTickets = new List<DestinationUserForTicket>();
            }
            var updatedDesignationForTickets = entity.DestinationUserForTickets.ToList();
            var destinationUserForTickets = entity.DestinationUserForTickets;
            entity.RequisitionApprovalTrackers = null;
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
            bool isTicketUpdated = _requisitionApprovalTicketRepository.Edit(entity);
            if (isTicketUpdated && trackers != null && trackers.Any())
            {
                bool isTrackerUpdated = _requisitionApprovalTrackerManager.Insert(trackers);
            }

            int deleteCount = 0, updateCount = 0;
            bool isDeleted = false, isAdded = false, isUpdated = false;

            if (addeableItems != null && addeableItems.Any())
            {
                isAdded = _destinationUserForTicketRepository.Insert(addeableItems);
            }
            bool isNotificationSent = false;
            isNotificationSent = _notificationManager.Notify(entity);

            //ts.Complete();

            return isTicketUpdated || isAdded || isNotificationSent;

            //}
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _requisitionApprovalTicketRepository.Delete(entity);
        }

        public RequisitionApprovalTicket GetById(long id)
        {
            return _requisitionApprovalTicketRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.ApprovalTrackers, c => c.AdvanceRequisitionHeader, c => c.AdvanceRequisitionHeader.AdvanceCategory, c => c.ApprovalLevel, c => c.ApprovalPanel,
                        c => c.DestinationUserForTickets);
        }

        public ICollection<RequisitionApprovalTicket> GetAll()
        {
            return _requisitionApprovalTicketRepository.GetAll(c => c.AdvanceRequisitionHeader,
                c => c.ApprovalStatus, c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets);
        }

        public RequisitionApprovalTicket GetByAdvanceRequisitionHeaderId(long advanceRequisitionHeaderId)
        {
            return _requisitionApprovalTicketRepository.GetFirstOrDefaultBy(
                c => c.AdvanceRequisitionHeaderId == advanceRequisitionHeaderId, c => c.AdvanceRequisitionHeader,
                c => c.ApprovalStatus, c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets);
        }

        public RequisitionApprovalTicket GetByAdvanceRequisitionHeaderIdWithNotApproved(long advanceRequisitionHeaderId)
        {
            return _requisitionApprovalTicketRepository.GetFirstOrDefaultBy(
                c => c.AdvanceRequisitionHeaderId == advanceRequisitionHeaderId &&
            c.ApprovalStatusId != (long)ApprovalStatusEnum.Approved, c => c.AdvanceRequisitionHeader,
                c => c.ApprovalStatus, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails, c => c.ApprovalTrackers, c => c.ApprovalLevel, c => c.ApprovalPanel,
                        c => c.DestinationUserForTickets);
        }

        public ICollection<RequisitionApprovalTicket> GetAllByApprovePanelIdAndApproveLevelId(int approvePanelId, int approveLevelId)
        {
            return
                _requisitionApprovalTicketRepository.Get(c => c.ApprovalLevelId == approveLevelId && c.ApprovalPanelId == approvePanelId, c => c.ApprovalLevel, c => c.ApprovalPanel, c => c.ApprovalStatus, c => c.AdvanceRequisitionHeader, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets);
        }

        public ICollection<RequisitionApprovalTicket> GetTicketsForUnitHead(string memberUserName, ApprovalStatusEnum approvalStatus)
        {

            var userInfo = _employeeManager.GetByUserName(memberUserName);

            return
                _requisitionApprovalTicketRepository.Get(
                    c =>
                        c.ApprovalLevel.IsLineSupervisor &&
                        c.AdvanceRequisitionHeader.RequesterSupervisorId == userInfo.UserId &&
                        c.ApprovalStatusId == (long)approvalStatus, c => c.ApprovalLevel.ApprovalLevelMembers,
                    c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ICollection<RequisitionApprovalTicket> GetTicketsForDeptHead(string memberUserName, ApprovalStatusEnum approvalStatus)
        {
            List<decimal> deptId = _headOfDepartmentManager.GetDepartmentIdByUserName(memberUserName);

            if (deptId == null)
            {
                return null;
            }

            return _requisitionApprovalTicketRepository.Get(
                  c => c.ApprovalLevel.IsHeadOfDepartment &&
                      c.ApprovalStatusId == (int)approvalStatus
                      && deptId.Contains((decimal)c.AdvanceRequisitionHeader.RequesterDepartmentId), c => c.ApprovalLevel.ApprovalLevelMembers, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails, c => c.ApprovalTrackers, c => c.ApprovalPanel, c => c.ApprovalLevel,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ICollection<RequisitionApprovalTicket> GetTicketsForDestinationMember(string memberUserName, ApprovalStatusEnum approvalStatus)
        {
            return
                _requisitionApprovalTicketRepository.Get(
                    c => c.DestinationUserForTickets.Any(d => d.DestinationUserName == memberUserName &&
                        c.ApprovalStatusId == (int)approvalStatus),
                    c => c.DestinationUserForTickets, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails,
                    c => c.ApprovalLevel.ApprovalLevelMembers, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ICollection<RequisitionApprovalTicket> GetRevertedTicketsForRequester(string requesterUserName)
        {
            return
                _requisitionApprovalTicketRepository.Get(
                    c => c.RequesterUserName.Equals(requesterUserName) && c.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted,
                    c => c.DestinationUserForTickets, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails,
                    c => c.ApprovalLevel.ApprovalLevelMembers, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ICollection<RequisitionApprovalTicket> GetRevertedTicketsForOtherMembers(string requesterUserName)
        {
            return
                _requisitionApprovalTicketRepository.Get(
                    c => !c.RequesterUserName.Equals(requesterUserName) && c.AdvanceRequisitionHeader
                        .CreatedBy.Equals(requesterUserName) && c.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted,
                    c => c.DestinationUserForTickets, c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails,
                    c => c.ApprovalLevel.ApprovalLevelMembers, c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public ICollection<RequisitionApprovalTicket> GetTicketsForMemberWithoutSpecificDestinationMember(string memberUserName,
            ApprovalStatusEnum approvalStatus)
        {
            var levelsOfMember = _approvalLevelManager.GetLevelsOfMember(memberUserName);

            if (levelsOfMember == null || !levelsOfMember.Any())
            {
                return null;
            }
            var levelOfMemberIdList = levelsOfMember.Select(c => c.Id).ToList();
            return
                _requisitionApprovalTicketRepository.Get(
                    c =>
                        levelOfMemberIdList.Contains(c.ApprovalLevelId)
                        && c.ApprovalStatusId == (long)approvalStatus,
                        c => c.ApprovalLevel.ApprovalLevelMembers,
                        c => c.AdvanceRequisitionHeader.AdvanceRequisitionDetails,
                        c => c.ApprovalTrackers,
                        c => c.DestinationUserForTickets).ToList();
        }

        public DateTime? GetAuthorizedOnById(long? requisitionApprovalTicketId)
        {
            DateTime? authorizedOn = null;
            if (requisitionApprovalTicketId != null)
            {
                var ticket = GetById((long)requisitionApprovalTicketId);
                if (ticket != null)
                    authorizedOn = ticket.AuthorizedOn;
            }
            return authorizedOn;
        }

        public ICollection<RequisitionApprovalTicket> GetTicketsForDiluteMember(string memberUserName, ApprovalStatusEnum approvaStatus)
        {
            return
                _requisitionApprovalTicketRepository.Get(
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class RequisitionApprovalTrackerManager : IRequisitionApprovalTrackerManager
    {
        private IRequisitionApprovalTrackerRepository _requisitionApprovalTrackerRepository;
        public RequisitionApprovalTrackerManager()
        {
            _requisitionApprovalTrackerRepository = new RequisitionApprovalTrackerRepositoryRepository();
        }

        public RequisitionApprovalTrackerManager(IRequisitionApprovalTrackerRepository requisitionApprovalTrackerRepository)
        {
            _requisitionApprovalTrackerRepository = requisitionApprovalTrackerRepository;
        }
        public bool Insert(RequisitionApprovalTracker entity)
        {
            return _requisitionApprovalTrackerRepository.Insert(entity);
        }

        public bool Insert(ICollection<RequisitionApprovalTracker> entityCollection)
        {
            return _requisitionApprovalTrackerRepository.Insert(entityCollection);
        }

        public bool Edit(RequisitionApprovalTracker entity)
        {
            return _requisitionApprovalTrackerRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var requisitionTracker = GetById(id);
            return _requisitionApprovalTrackerRepository.Delete(requisitionTracker);

        }

        public RequisitionApprovalTracker GetById(long id)
        {
            return _requisitionApprovalTrackerRepository.GetFirstOrDefaultBy(c => c.ApprovalTicketId == id, c => c.ApprovalStatus, c => c.RequisitionApprovalTicket, c => c.RequisitionApprovalTicket.AdvanceRequisitionHeader);
        }

        public ICollection<RequisitionApprovalTracker> GetAll()
        {
            return _requisitionApprovalTrackerRepository.GetAll(c => c.ApprovalStatus, c => c.RequisitionApprovalTicket, c => c.RequisitionApprovalTicket.AdvanceRequisitionHeader);
        }

        public ICollection<RequisitionApprovalTracker> GetByAuthorizedBy(string username)
        {
            return _requisitionApprovalTrackerRepository.Get(c => c.AuthorizedBy == username && c.ApprovalStatusId == (long)ApprovalStatusEnum.Approved || c.ApprovalStatusId == (long)ApprovalStatusEnum.Rejected || c.ApprovalStatusId == (long)ApprovalStatusEnum.Reverted && (DbFunctions.DiffDays(c.AuthorizedOn, DateTime.Now) <= Utility.Utility.TimeDuration), c => c.ApprovalTicket);
        }
    }
}

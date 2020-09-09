using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.BLL.AdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.CustomException;
using IDCOLAdvanceModule.Model.EntityModels.Base;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IAdvanceQueryManagerModule;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ApprovalTicketManager : IApprovalTicketManager
    {
        public IApprovalTicketRepository _approvalTicketRepository;

        public ApprovalTicketManager()
        {
            _approvalTicketRepository = new ApprovalTicketRepository();
        }

        public ApprovalTicketManager(IApprovalTicketRepository approvalTicketRepository)
        {
            _approvalTicketRepository = approvalTicketRepository;
        }
        public bool Insert(ApprovalTicket entity)
        {
            return _approvalTicketRepository.Insert(entity);
        }

        public bool Insert(ICollection<ApprovalTicket> entityCollection)
        {
            return _approvalTicketRepository.Insert(entityCollection);

        }

        public bool Edit(ApprovalTicket entity)
        {
            return _approvalTicketRepository.Edit(entity);

        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _approvalTicketRepository.Delete(entity);
        }

        public ApprovalTicket GetById(long id)
        {
            return _approvalTicketRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.ApprovalLevel,
                c => c.ApprovalPanel, c => c.ApprovalStatus, c => c.ApprovalTrackers, c => c.DestinationUserForTickets,
                c => c.TicketType);
        }

        public ICollection<ApprovalTicket> GetAll()
        {
            return _approvalTicketRepository.GetAll(c => c.ApprovalLevel,
                c => c.ApprovalPanel, c => c.ApprovalStatus, c => c.ApprovalTrackers, c => c.DestinationUserForTickets,
                c => c.TicketType);
        }
    }
}

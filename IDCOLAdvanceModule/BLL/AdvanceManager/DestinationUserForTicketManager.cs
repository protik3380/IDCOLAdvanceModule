using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class DestinationUserForTicketManager : IDestinationUserForTicketManager
    {
        private readonly IDestinationUserForTicketRepository _destinationUserForTicketRepository;

        public DestinationUserForTicketManager()
        {
            _destinationUserForTicketRepository = new DestinationUserForTicketRepository();
        }

        public DestinationUserForTicketManager(IDestinationUserForTicketRepository destinationUserForTicketRepository)
        {
            _destinationUserForTicketRepository = destinationUserForTicketRepository;
        }
        public bool Insert(DestinationUserForTicket entity)
        {
            return _destinationUserForTicketRepository.Insert(entity);
        }

        public bool Insert(ICollection<DestinationUserForTicket> entityCollection)
        {
            return _destinationUserForTicketRepository.Insert(entityCollection);

        }

        public bool Edit(DestinationUserForTicket entity)
        {
            return _destinationUserForTicketRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _destinationUserForTicketRepository.Delete(entity);
        }

        public DestinationUserForTicket GetById(long id)
        {
            return _destinationUserForTicketRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<DestinationUserForTicket> GetAll()
        {
            return _destinationUserForTicketRepository.GetAll();
        }

        public ICollection<DestinationUserForTicket> GetBy(long ticketId, long approvalLevelId, long approvalPanelId)
        {
            return _destinationUserForTicketRepository.Get(c => c.ApprovalTicketId == ticketId && c.ApprovalLevelId == approvalLevelId && c.ApprovalPanelId == approvalPanelId).ToList();
        }

        public ICollection<DestinationUserForTicket> GetBy(string destinationUserName)
        {
            return
                _destinationUserForTicketRepository.Get(c => c.DestinationUserName.Equals(destinationUserName)).ToList();
        }

        public ICollection<DestinationUserForTicket> GetBy(long approvalLevelId, long approvalPanelId  )
        {
            return _destinationUserForTicketRepository.Get(c=>c.ApprovalLevelId == approvalLevelId && c.ApprovalPanelId ==approvalPanelId).ToList();
        }

        public DestinationUserForTicket GetLastDestinationUser(long ticketId, long approvalLevelId, long approvalPanelId)
        {
            return GetBy(ticketId, approvalLevelId, approvalPanelId).LastOrDefault();
        }
    }
}

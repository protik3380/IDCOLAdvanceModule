using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IDestinationUserForTicketManager : IManager<DestinationUserForTicket>
    {
        ICollection<DestinationUserForTicket> GetBy(long approvalLevelId,
            long approvalPanelId);
        ICollection<DestinationUserForTicket> GetBy(long ticketId, long approvalLevelId,
           long approvalPanelId);
        ICollection<DestinationUserForTicket> GetBy(string destinationUserName);
        DestinationUserForTicket GetLastDestinationUser(long ticketId, long approvalLevelId, long approvalPanelId);
    }
}

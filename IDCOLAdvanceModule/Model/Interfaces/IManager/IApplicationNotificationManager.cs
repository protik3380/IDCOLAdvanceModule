using System.Collections.Generic;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IApplicationNotificationManager : IManager<ApplicationNotification>
    {
        ICollection<ApplicationNotification> GetUnReadNotificationBy(string toUserName);
        ICollection<ApplicationNotification> GetReadNotificationBy(string toUserName);
        bool SetMarkAsRead(ICollection<ApplicationNotification> applicationNotifications);
    }
}

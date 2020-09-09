using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.IManager.BaseManager;

namespace IDCOLAdvanceModule.Model.Interfaces.IManager
{
    public interface IEmailNotificationManager : IManager<EmailNotification>
    {
        ICollection<EmailNotification> GetNotSentEmailNotifications();
        ICollection<MailMessage> GetMailMessageList(ICollection<EmailNotification> emailListNotifications);
        MailMessage GetMailMessage(EmailNotification emailListNotification);
        void SendEmail();
    }
}

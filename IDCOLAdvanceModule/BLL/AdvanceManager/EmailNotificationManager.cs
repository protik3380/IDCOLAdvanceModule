using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class EmailNotificationManager : IEmailNotificationManager
    {
        private readonly IEmailNotificationRepository _emailNotificationRepository;
        private ICollection<EmailNotification> _notSentEmailNotifications;
        public EmailNotificationManager()
        {
            _emailNotificationRepository = new EmailNotificationRepository();
        }

        public EmailNotificationManager(IEmailNotificationRepository emailNotificationRepository)
        {
            _emailNotificationRepository = emailNotificationRepository;
        }

        public bool Insert(EmailNotification entity)
        {
            return _emailNotificationRepository.Insert(entity);
        }

        public bool Insert(ICollection<EmailNotification> entityCollection)
        {
            return _emailNotificationRepository.Insert(entityCollection);
        }

        public bool Edit(EmailNotification entity)
        {
            return _emailNotificationRepository.Edit(entity);
        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _emailNotificationRepository.Delete(entity);
        }

        public EmailNotification GetById(long id)
        {
            return _emailNotificationRepository.GetFirstOrDefaultBy(c => c.Id == id);
        }

        public ICollection<EmailNotification> GetAll()
        {
            return _emailNotificationRepository.GetAll();
        }

        public ICollection<EmailNotification> GetNotSentEmailNotifications()
        {
            return _emailNotificationRepository.Get(c => c.IsSent == false);
        }

        public ICollection<MailMessage> GetMailMessageList(ICollection<EmailNotification> emailListNotifications)
        {
            ICollection<MailMessage> mailMessages = new List<MailMessage>();
            foreach (var emailListNotification in emailListNotifications)
            {
                var fromEmail = Utility.Utility.EmailFrom;

                var toEmailAddresses = emailListNotification.To.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                var ccEmailAddresses = emailListNotification.Cc.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (!toEmailAddresses.Any())
                {
                    continue;
                }

                MailAddress from = new MailAddress(fromEmail, String.Empty, System.Text.Encoding.UTF8);

                MailMessage message = new MailMessage();
                message.From = from;
                foreach (string emailAddress in toEmailAddresses)
                {
                    var toaddress = new MailAddress(emailAddress);
                    message.To.Add(toaddress);
                }

                if (ccEmailAddresses.Any())
                {
                    foreach (string ccEmailAddress in ccEmailAddresses)
                    {
                        var ccAddress = new MailAddress(ccEmailAddress);
                        message.CC.Add(ccAddress);
                    }
                }

                message.Body = emailListNotification.MessageContentHtml;
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = emailListNotification.Subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessages.Add(message);
            }

            return mailMessages;
        }

        public MailMessage GetMailMessage(EmailNotification emailListNotification)
        {
            var fromEmail = Utility.Utility.EmailFrom;

            var toEmailAddresses = emailListNotification.To.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            string[] ccEmailAddresses = null;
            if (!string.IsNullOrEmpty(emailListNotification.Cc))
            {
                ccEmailAddresses = emailListNotification.Cc.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }


            if (!toEmailAddresses.Any())
            {
                return null;
            }

            MailAddress from = new MailAddress(fromEmail, String.Empty, System.Text.Encoding.UTF8);

            MailMessage message = new MailMessage();
            message.From = from;
            foreach (string emailAddress in toEmailAddresses)
            {
                var toaddress = new MailAddress(emailAddress);
                message.To.Add(toaddress);
            }

            if (ccEmailAddresses != null && ccEmailAddresses.Any())
            {
                foreach (string ccEmailAddress in ccEmailAddresses)
                {
                    var ccAddress = new MailAddress(ccEmailAddress);
                    message.CC.Add(ccAddress);
                }
            }

            message.Body = emailListNotification.MessageContentHtml;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = emailListNotification.Subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            return message;
        }

        public async void  SendEmail()
        {
            var notSentEmailList = GetNotSentEmailNotifications();
            if (notSentEmailList.Any())
            {
                ThreadPool.QueueUserWorkItem(t =>
                {
                    foreach (var notificationEmail in notSentEmailList)
                    {
                        var mailMessage = GetMailMessage(notificationEmail);

                        if (mailMessage == null)
                        {
                            continue;
                        }
                        
                        SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                        client.EnableSsl = true;
                        client.Credentials = new System.Net.NetworkCredential(Utility.Utility.EmailFrom, Utility.Utility.EmailPassword);

                        //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                        try
                        {
                            if (Utility.Utility.CheckForInternetConnection())
                            {
                                client.SendAsync(mailMessage,mailMessage);
                                if (_notSentEmailNotifications == null)
                                {
                                    _notSentEmailNotifications = new List<EmailNotification>();
                                }
                                SendComplete(notificationEmail);


                                //_notSentEmailNotifications.Add(notificationEmail);
                            }

                        }
                        catch (Exception)
                        {
                            // email not sent!
                        }
                    }
                });

                
            }
        }

        private  bool SendComplete(ICollection<EmailNotification> notSentEmailList)
        {
            int successCount = 0;
            foreach (var emailNotification in notSentEmailList)
            {
               bool isSent =   SendComplete(emailNotification);
                if (isSent)
                {
                    successCount++;
                }
            }

            return successCount == notSentEmailList.Count;
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (_notSentEmailNotifications!=null && _notSentEmailNotifications.Any())
            {
              bool isupdated  =  SendComplete(_notSentEmailNotifications);
            }
            _notSentEmailNotifications = null;

        }

        private bool SendComplete(EmailNotification notificationEmail)
        {
            notificationEmail.IsSent = true;
            notificationEmail.SentDate = DateTime.Now;
            bool isUpdated = Edit(notificationEmail);
            return isUpdated;
        }
    }
}

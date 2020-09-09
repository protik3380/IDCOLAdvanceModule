using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using IDCOLAdvanceModule.BLL.AdvanceManager;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using Microsoft.Win32;

namespace IDCOLAdvanceModule.WindowsService
{
    partial class AdvanceScheduler : ServiceBase
    {
        private System.Timers.Timer emailTimer = null;
        private IEmailNotificationManager _emailNotificationManager;


        public AdvanceScheduler()
        {
            InitializeComponent();
            _emailNotificationManager = new EmailNotificationManager();
        }

        protected override void OnStart(string[] args)
        {
            emailTimer = new System.Timers.Timer();
            emailTimer.Interval = 2000;
            emailTimer.Elapsed += new ElapsedEventHandler(SendEmail);
            emailTimer.Start();
        }

        private void SendEmail(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _emailNotificationManager.SendEmail();

        }

        private void SendComplete(EmailNotification notificationEmail)
        {

            notificationEmail.IsSent = true;
            bool isUpdated = _emailNotificationManager.Edit(notificationEmail);

        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the message we sent
            MailMessage msg = (MailMessage)e.UserState;

            if (e.Cancelled)
            {
                // prompt user with "send cancelled" message 
            }
            if (e.Error != null)
            {
                // prompt user with error message 
            }
            else
            {
                // prompt user with message sent!
                // as we have the message object we can also display who the message
                // was sent to etc 
            }

            // finally dispose of the message
            if (msg != null)
                msg.Dispose();
        }
        protected override void OnStop()
        {
            emailTimer.Stop();
        }

        

        
    }
}

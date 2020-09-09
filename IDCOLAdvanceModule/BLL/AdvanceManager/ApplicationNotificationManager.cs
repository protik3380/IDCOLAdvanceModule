using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.IManager;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;
using IDCOLAdvanceModule.Model.Interfaces.IModuleRepository;
using IDCOLAdvanceModule.Repository;

namespace IDCOLAdvanceModule.BLL.AdvanceManager
{
    public class ApplicationNotificationManager : IApplicationNotificationManager
    {
        private readonly IApplicationNotificationRepository _applicationNotificationRepository;

        public ApplicationNotificationManager()
        {
            _applicationNotificationRepository = new ApplicationNotificationRepository();
        }

        public ApplicationNotificationManager(IApplicationNotificationRepository applicationNotificationRepository)
        {
            _applicationNotificationRepository = applicationNotificationRepository;
        }

        public bool Insert(ApplicationNotification entity)
        {
            return _applicationNotificationRepository.Insert(entity);
        }

        public bool Insert(ICollection<ApplicationNotification> entityCollection)
        {
            return _applicationNotificationRepository.Insert(entityCollection);

        }

        public bool Edit(ApplicationNotification entity)
        {
            return _applicationNotificationRepository.Edit(entity);

        }

        public bool Delete(long id)
        {
            var entity = GetById(id);
            return _applicationNotificationRepository.Delete(entity);
        }

        public ApplicationNotification GetById(long id)
        {
            return _applicationNotificationRepository.GetFirstOrDefaultBy(c => c.Id == id, c => c.TicketStatus);
        }

        public ICollection<ApplicationNotification> GetAll()
        {
            return _applicationNotificationRepository.GetAll(c => c.TicketStatus);
        }

        public ICollection<ApplicationNotification> GetUnReadNotificationBy(string toUserName)
        {
            return _applicationNotificationRepository.Get(c => c.ToUserName.ToLower().Equals(toUserName.ToLower()) 
                && c.IsRead == false,
                c => c.TicketStatus);
        }

        public ICollection<ApplicationNotification> GetReadNotificationBy(string toUserName)
        {
            return _applicationNotificationRepository.Get(c => c.ToUserName.ToLower().Equals(toUserName.ToLower()) && c.IsRead == true && (DbFunctions.DiffDays(c.ReadDate, DateTime.Now) <= Utility.Utility.TimeDuration),
                c => c.TicketStatus);
        }

        public bool SetMarkAsRead(ICollection<ApplicationNotification> applicationNotifications)
        {
            bool isUpdated;
            int updateCount = 0;
            foreach (ApplicationNotification notifiation in applicationNotifications)
            {
                notifiation.IsRead = true;
                notifiation.ReadDate = DateTime.Now;
                isUpdated = Edit(notifiation);
                if (isUpdated)
                {
                    updateCount++;
                }
            }
            isUpdated = updateCount == (applicationNotifications == null ? 0 : applicationNotifications.Count());
            return isUpdated;
        }
    }
}

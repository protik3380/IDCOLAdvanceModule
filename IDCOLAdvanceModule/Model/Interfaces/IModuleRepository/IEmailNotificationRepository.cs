﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.Interfaces.Repository.BaseRepository;

namespace IDCOLAdvanceModule.Model.Interfaces.IModuleRepository
{
    public interface IEmailNotificationRepository : IRepository<EmailNotification>
    {
    }
}

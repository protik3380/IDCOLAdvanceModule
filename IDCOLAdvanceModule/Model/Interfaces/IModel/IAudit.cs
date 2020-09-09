﻿using System;

namespace IDCOLAdvanceModule.Model.Interfaces.IModel
{
    public interface IAudit
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string LastModifiedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}

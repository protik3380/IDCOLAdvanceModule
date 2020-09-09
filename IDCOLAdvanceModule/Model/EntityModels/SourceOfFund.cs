﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.Interfaces.IModel;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class SourceOfFund : IAudit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public ICollection<SourceOfFundHistory> SourceOfFundHistories { get; set; }
    }
}
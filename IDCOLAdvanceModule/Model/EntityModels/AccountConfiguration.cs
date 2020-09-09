using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class AccountConfiguration
    {
        public long Id { get; set; }
        public long AdvanceCategoryId { get; set; }
        public long AccountTypeId { get; set; }
        public string AccountCode { get; set; }
        public bool IsDefaultAccount { get; set; }


        public AdvanceCategory AdvanceCategory { get; set; }
        public AccountType AccountType { get; set; }
    }
}

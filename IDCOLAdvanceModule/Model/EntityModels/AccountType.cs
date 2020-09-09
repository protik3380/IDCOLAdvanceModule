using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class AccountType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccountConfiguration> AccountConfigurations { get; set; }
        public ICollection<GeneralAccountConfiguration> GeneralAccountConfigurations { get; set; }
    }
}

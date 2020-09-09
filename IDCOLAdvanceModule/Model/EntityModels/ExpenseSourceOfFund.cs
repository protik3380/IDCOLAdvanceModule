using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.Interfaces.IModel;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ExpenseSourceOfFund : IAudit
    {
        public long Id { get; set; }
        public long SourceOfFundId { get; set; }
        public double Percentage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public long AdvanceExpenseHeaderId { get; set; }
        public AdvanceExpenseHeader AdvanceExpenseHeader { get; set; }
        public SourceOfFund SourceOfFund { get; set; }
    }
}

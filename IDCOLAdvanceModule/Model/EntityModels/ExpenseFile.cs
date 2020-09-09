using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCOLAdvanceModule.Model.EntityModels.Expense;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class ExpenseFile
    {
        public long Id { get; set; }
        public string FileLocation { get; set; }
        public bool IsDeleted { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public long AdvanceExpenseHeaderId { get; set; }
        [NotMapped]
        public string FileName
        {
            get { return Path.GetFileName(FileLocation); }
        }

        public AdvanceExpenseHeader AdvanceExpenseHeader { get; set; }
    }
}

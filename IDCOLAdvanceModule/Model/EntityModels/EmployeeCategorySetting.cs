


using System.ComponentModel.DataAnnotations.Schema;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class EmployeeCategorySetting
    {
        public long Id { get; set; }
        public long EmployeeCategoryId { get; set; }
        public decimal AdminRankId { get; set; }
        public EmployeeCategory EmployeeCategory { get; set; }
        [NotMapped]
        public Admin_Rank AdminRank { get; set; }
    }
}

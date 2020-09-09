
using System.Collections.Generic;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class EmployeeCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<EmployeeCategorySetting> EmployeeCategorySettings { get; set; }
    }
}

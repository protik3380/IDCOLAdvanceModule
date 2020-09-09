
namespace IDCOLAdvanceModule.Model.EntityModels.DBViewModels
{

    public class EmployeeCategoryDesignationVM
    {
        public System.Guid Id { get; set; }
        public decimal RankID { get; set; }
        public string RankName { get; set; }
        public long? EmployeeCategoryId { get; set; }
        public string EmployeeCategoryName { get; set; }
        public long? EmployeeCategorySettingsId { get; set; }
        public bool IsActive { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using IDCOLAdvanceModule.Context.MISContext;

namespace IDCOLAdvanceModule.Model.EntityModels
{
    public class DiluteDesignation
    {
        public int Id { get; set; }
        public decimal DesignationId { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public Admin_Rank Designation { get; set; }
        

    }
}

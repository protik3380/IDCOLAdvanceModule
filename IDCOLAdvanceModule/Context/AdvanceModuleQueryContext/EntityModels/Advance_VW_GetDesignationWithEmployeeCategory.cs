//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IDCOLAdvanceModule.Context.AdvanceModuleQueryContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Advance_VW_GetDesignationWithEmployeeCategory
    {
        public Nullable<System.Guid> Id { get; set; }
        public decimal RankID { get; set; }
        public string RankName { get; set; }
        public Nullable<long> EmployeeCategoryId { get; set; }
        public string EmployeeCategoryName { get; set; }
        public Nullable<long> EmployeeCategorySettingsId { get; set; }
        public bool IsActive { get; set; }
    }
}

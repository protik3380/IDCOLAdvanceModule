//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IDCOLAdvanceModule.Context.MISContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admin_Rank
    {
        public Admin_Rank()
        {
            this.UserTables = new HashSet<UserTable>();
        }
    
        public decimal RankID { get; set; }
        public string RankName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> Priority { get; set; }
        public Nullable<decimal> OrderBy { get; set; }
    
        public virtual ICollection<UserTable> UserTables { get; set; }
    }
}

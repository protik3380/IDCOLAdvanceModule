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
    
    public partial class Advance_VW_GetExpenseSourceOfFund
    {
        public Nullable<System.Guid> Id { get; set; }
        public Nullable<long> ExpenseHeaderId { get; set; }
        public Nullable<long> SourceOfFundId { get; set; }
        public string SourceOfFundName { get; set; }
        public double Percentage { get; set; }
    }
}
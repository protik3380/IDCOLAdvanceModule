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
    
    public partial class Advance_SP_GetOverseasTravelReport
    {
        public Nullable<System.Guid> Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeUserName { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<decimal> DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long RequisitionId { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionPurpose { get; set; }
        public string RequisitionPlace { get; set; }
        public string RequisitionCountryName { get; set; }
        public System.DateTime RequisitionFromDate { get; set; }
        public System.DateTime RequisitionToDate { get; set; }
        public Nullable<System.DateTime> AdvanceIssueDate { get; set; }
        public Nullable<decimal> AdvanceAmount { get; set; }
        public Nullable<long> ExpenseId { get; set; }
        public string ExpenseNo { get; set; }
        public string ExpensePurpose { get; set; }
        public string ExpensePlace { get; set; }
        public string ExpenseCountryName { get; set; }
        public Nullable<System.DateTime> ExpenseFromDate { get; set; }
        public Nullable<System.DateTime> ExpenseToDate { get; set; }
        public Nullable<System.DateTime> ExpenseIssueDate { get; set; }
        public Nullable<decimal> ExpenseAmount { get; set; }
    }
}
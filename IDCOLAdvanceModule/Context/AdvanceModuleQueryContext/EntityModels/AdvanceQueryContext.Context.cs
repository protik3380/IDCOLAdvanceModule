﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Data.Entity.Core.Objects;

namespace IDCOLAdvanceModule.Context.AdvanceModuleQueryContext
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class AdvanceQueryContext : DbContext
    {
        public AdvanceQueryContext()
            : base("name=AdvanceQueryContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Advance_VW_GetAdvanceExpense> Advance_VW_GetAdvanceExpense { get; set; }
        public virtual DbSet<Advance_VW_GetAdvanceExpenseDetail> Advance_VW_GetAdvanceExpenseDetail { get; set; }
        public virtual DbSet<Advance_VW_GetAdvanceRequisition> Advance_VW_GetAdvanceRequisition { get; set; }
        public virtual DbSet<Advance_VW_GetAdvanceRequisitionDetail> Advance_VW_GetAdvanceRequisitionDetail { get; set; }
        public virtual DbSet<Advance_VW_GetAgingReport> Advance_VW_GetAgingReport { get; set; }
        public virtual DbSet<Advance_VW_GetApprovalLevelMember> Advance_VW_GetApprovalLevelMember { get; set; }
        public virtual DbSet<Advance_VW_GetDesignationWithEmployeeCategory> Advance_VW_GetDesignationWithEmployeeCategory { get; set; }
        public virtual DbSet<Advance_VW_GetEntitlementMappingSetting> Advance_VW_GetEntitlementMappingSetting { get; set; }
        public virtual DbSet<Advance_VW_GetExpenseSignatory> Advance_VW_GetExpenseSignatory { get; set; }
        public virtual DbSet<Advance_VW_GetExpenseSourceOfFund> Advance_VW_GetExpenseSourceOfFund { get; set; }
        public virtual DbSet<Advance_VW_GetHeadOfDepartment> Advance_VW_GetHeadOfDepartment { get; set; }
        public virtual DbSet<Advance_VW_GetLocalTravelReport> Advance_VW_GetLocalTravelReport { get; set; }
        public virtual DbSet<Advance_VW_GetPendingExpenseReport> Advance_VW_GetPendingExpenseReport { get; set; }
        public virtual DbSet<Advance_VW_GetPendingRequisitionReport> Advance_VW_GetPendingRequisitionReport { get; set; }
        public virtual DbSet<Advance_VW_GetRejectedExpenseReport> Advance_VW_GetRejectedExpenseReport { get; set; }
        public virtual DbSet<Advance_VW_GetRejectedRequisitionReport> Advance_VW_GetRejectedRequisitionReport { get; set; }
        public virtual DbSet<Advance_VW_GetRequisitionSignatory> Advance_VW_GetRequisitionSignatory { get; set; }
        public virtual DbSet<Advance_VW_GetRequisitionSourceOfFund> Advance_VW_GetRequisitionSourceOfFund { get; set; }
        public virtual DbSet<Advance_VW_GetTimeLagForExpense> Advance_VW_GetTimeLagForExpense { get; set; }
        public virtual DbSet<Advance_VW_GetTimeLagForRequisition> Advance_VW_GetTimeLagForRequisition { get; set; }

        public virtual ObjectResult<Advance_SP_GetLocalTravelReport> Advance_SP_GetLocalTravelReport()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Advance_SP_GetLocalTravelReport>("Advance_SP_GetLocalTravelReport");
        }

        public virtual ObjectResult<Advance_SP_GetOverseasTravelReport> Advance_SP_GetOverseasTravelReport()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Advance_SP_GetOverseasTravelReport>("Advance_SP_GetOverseasTravelReport");
        }
    }
}

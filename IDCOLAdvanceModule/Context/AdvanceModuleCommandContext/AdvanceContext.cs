using IDCOLAdvanceModule.Context.AdvanceModuleContext.Configuration;
using IDCOLAdvanceModule.Model.EntityModels;
using IDCOLAdvanceModule.Model.EntityModels.Base;
using IDCOLAdvanceModule.Model.EntityModels.DBViewModels;
using IDCOLAdvanceModule.Model.EntityModels.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Requisition;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IDCOLAdvanceModule.Model;
using IDCOLAdvanceModule.Model.EntityModels.DesignationUserForTicket;
using IDCOLAdvanceModule.Model.EntityModels.History;
using IDCOLAdvanceModule.Model.EntityModels.History.ExpenseHistory;
using IDCOLAdvanceModule.Model.EntityModels.History.RequisitionHistory;
using IDCOLAdvanceModule.Model.EntityModels.Notification;
using IDCOLAdvanceModule.Model.EntityModels.Voucher;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;

namespace IDCOLAdvanceModule.Context.AdvanceModuleCommandContext
{
    public class AdvanceContext : DbContext
    {

        public AdvanceContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new EntitlementMappingSettingViewConfiguration());
            modelBuilder.Configurations.Add(new EmployeeCategoryDesignationViewConfiguration());
            modelBuilder.Configurations.Add(new AdvanceRequisitionSearchCriteriaViewConfiguration());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<AdvanceCategory>()
                .HasOptional(c => c.RequisitionApprovalPanel)
                .WithMany(c => c.AdvanceRequisitionCategories)
                .HasForeignKey(c => c.RequisitionApprovalPanelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AdvanceCategory>()
                .HasOptional(c => c.ExpenseApprovalPanel)
                .WithMany(c => c.AdvanceExpenseCategories)
                .HasForeignKey(c => c.ExpenseApprovalPanelId)
                .WillCascadeOnDelete(false);

        }
        public DbSet<AdvanceRequisitionHeader> AdvanceRequisitionHeaders { get; set; }
        public DbSet<AdvanceRequisitionDetail> AdvanceRequisitionDetails { get; set; }
        public DbSet<CostItem> CostItems { get; set; }
        public DbSet<Entitlement> Entitlements { get; set; }
        public DbSet<AdvanceStatus> AdvanceStatuses { get; set; }
        public DbSet<BaseAdvanceCategory> BaseAdvanceCategories { get; set; }
        public DbSet<AdvanceCategory> AdvanceCategories { get; set; }
        public DbSet<EmployeeCategory> EmployeeCategories { get; set; }
        public DbSet<EmployeeCategorySetting> EmployeeCategorySettings { get; set; }
        public DbSet<AdvanceCategoryWiseCostItemSetting> RequisitionCategoryWiseCostItemSettings { get; set; }
        public DbSet<EntitlementMappingSettingHeader> EntitlementMappingSettingHeaders { get; set; }
        public DbSet<EntitlementMappingSettingDetail> EntitlementMappingSettingDetails { get; set; }
        public DbSet<RequisitionApprovalTicket> RequisitionApprovalTickets { get; set; }
        public DbSet<RequisitionApprovalTracker> RequisitionApprovalTrackers { get; set; }
        public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
        public DbSet<ApprovalPanelType> ApprovalPanelTypes { get; set; }
        public DbSet<ApprovalPanel> ApprovalPanels { get; set; }
        public DbSet<ApprovalLevel> ApprovalLevels { get; set; }
        public DbSet<PlaceOfVisit> PlaceOfVisits { get; set; }
        public DbSet<ApprovalLevelMember> ApprovalLevelMembers { get; set; }
        public DbSet<HistoryOperationMode> HistoryOperationModes { get; set; }
        public DbSet<OverseasTravelGroup> OverseasTravelGroups { get; set; }
        public DbSet<HeadOfDepartment> HeadOfDepartments { get; set; }
        public DbSet<LocationGroup> LocationGroups { get; set; }
        public DbSet<AdvanceExpenseHeader> AdvanceExpenseHeaders { get; set; }
        public DbSet<AdvanceExpenseDetail> AdvanceExpenseDetails { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<ExecutiveOverseasTravellingAllowance> ExecutiveOverseasTravellingAllowances { get; set; }
        public DbSet<SourceOfFund> SourceOfFunds { get; set; }
        public DbSet<SourceOfFundHistory> SourceOfFundsHistories { get; set; }
        public DbSet<RequisitionSourceOfFund> RequisitionSourceOfFunds { get; set; }
        public DbSet<CurrencyConversionRateDetail> CurrencyConversionRateDetails { get; set; }
        public DbSet<ExpenseSourceOfFund> ExpenseSourceOfFunds { get; set; }
        public DbSet<VatTaxType> VatTaxTypes { get; set; }
        public DbSet<HistoryMode> HistoryModes { get; set; }
        public DbSet<RequisitionHistoryHeader> RequisitionHeaderHistories { get; set; }
        public DbSet<RequisitionHistoryDetail> RequisitionDetailHistories { get; set; }
        public DbSet<VoucherStatus> VoucherStatuses { get; set; }
        public DbSet<RequisitionVoucherHeader> RequisitionVoucherHeaders { get; set; }
        public DbSet<ExpenseVoucherDetail> ExpenseVoucherDetails { get; set; }
        public DbSet<DiluteDesignation> DiluteDesignations { get; set; }
        public DbSet<DestinationUserForTicket> DestinationUserForTickets { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AccountConfiguration> AccountConfigurations { get; set; }
        public DbSet<GeneralAccountConfiguration> GeneralAccountConfigurations { get; set; }
        public DbSet<ExpenseHistoryHeader> ExpenseHistoryHeaders { get; set; }
        public DbSet<ExpenseHistoryDetail> ExpenseHistoryDetails { get; set; }
        public DbSet<ApplicationNotification> ApplicationNotifiations { get; set; }
        public DbSet<RequisitionNotification> RequisitionNotifications { get; set; }
        public DbSet<ExpenseNotification> ExpenseNotifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
       

        #region SQLVIEW
        public DbSet<EntitlementMappingSettingVM> EntitlementMappingSettingVms { get; set; }
        public DbSet<EmployeeCategoryDesignationVM> EmployeeCategoryDesignationVms { get; set; }
        public DbSet<AdvanceRequisitionSearchCriteriaVM> AdvanceRequisitionSearchCriteriaVms { get; set; }
        #endregion
    }
}

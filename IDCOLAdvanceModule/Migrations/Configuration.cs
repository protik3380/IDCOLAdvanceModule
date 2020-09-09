using IDCOLAdvanceModule.Context.AdvanceModuleCommandContext;
using IDCOLAdvanceModule.Model.EntityModels.Notification;

namespace IDCOLAdvanceModule.Migrations
{
    using IDCOLAdvanceModule.Model.EntityModels;
    using IDCOLAdvanceModule.Model.EntityModels.Base;
    using IDCOLAdvanceModule.Model.EntityModels.History;
    using IDCOLAdvanceModule.Model.EntityModels.Voucher;
    using IDCOLAdvanceModule.Model.Enums;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AdvanceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AdvanceContext context)
        {
            //context.EmployeeCategories.AddOrUpdate(
            //    new EmployeeCategory { Id = 1, Name = "Executive" },
            //    new EmployeeCategory { Id = 2, Name = "Group A" },
            //    new EmployeeCategory { Id = 3, Name = "Group B" },
            //    new EmployeeCategory { Id = 4, Name = "Group C" }
            //    );

            //context.ApprovalPanelTypes.AddOrUpdate(
            //    new ApprovalPanelType { Id = 1, Name = "Requisition", CreatedOn = DateTime.Now },
            //    new ApprovalPanelType { Id = 2, Name = "Expense", CreatedOn = DateTime.Now }
            //    );

            //context.CostItems.AddOrUpdate(
            //    new CostItem { Id = 1, Name = "Per diem" },
            //    new CostItem { Id = 2, Name = "Accomodation" },
            //    new CostItem { Id = 3, Name = "Local Travel" },
            //    new CostItem { Id = 4, Name = "Visa Fee" },
            //    new CostItem { Id = 5, Name = "Insurance" },
            //    new CostItem { Id = 6, Name = "Air Fare" },
            //    new CostItem { Id = 7, Name = "Hotel Allowance" },
            //    new CostItem { Id = 8, Name = "Others if any" }
            //    );

            //context.AdvanceStatuses.AddOrUpdate(
            //    new AdvanceStatus { Id = (long)AdvanceStatusEnum.Draft, Name = AdvanceStatusEnum.Draft.ToString() },
            //    new AdvanceStatus { Id = (long)AdvanceStatusEnum.ApprovalOnProgress, Name = AdvanceStatusEnum.ApprovalOnProgress.ToString() },
            //    new AdvanceStatus { Id = (long)AdvanceStatusEnum.Reverted, Name = AdvanceStatusEnum.Reverted.ToString() },
            //    new AdvanceStatus { Id = (long)AdvanceStatusEnum.Approved, Name = AdvanceStatusEnum.Approved.ToString() },
            //    new AdvanceStatus { Id = (long)AdvanceStatusEnum.Rejected, Name = AdvanceStatusEnum.Rejected.ToString() },
            //    new AdvanceStatus { Id = (long)AdvanceStatusEnum.Removed, Name = AdvanceStatusEnum.Removed.ToString() }
            //    );

            //context.BaseAdvanceCategories.AddOrUpdate(
            //    new BaseAdvanceCategory { Id = (long)BaseAdvanceCategoryEnum.Travel, Name = "Travel" },
            //    new BaseAdvanceCategory { Id = (long)BaseAdvanceCategoryEnum.PettyCash, Name = "Petty Cash" },
            //    new BaseAdvanceCategory { Id = (long)BaseAdvanceCategoryEnum.Miscellaneous, Name = "Miscellaneous" },
            //    new BaseAdvanceCategory { Id = (long)BaseAdvanceCategoryEnum.CorporateAdvisory, Name = "Corporate Advisory" }
            //);

            //context.AdvanceCategories.AddOrUpdate(
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.LocalTravel, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.Travel, Name = "Local Travel", DisplaySerial = 1},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.OversearTravel, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.Travel, Name = "Overseas Travel", DisplaySerial = 2},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.PettyCash, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.PettyCash, Name = "Petty Cash", DisplaySerial = 1},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.Event, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.Miscellaneous, Name = "Event", DisplaySerial = 1},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.TrainingAndWorkshop, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.Miscellaneous, Name = "Training & Workshop", DisplaySerial = 2},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.Meeting, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.Miscellaneous, Name = "Meeting", DisplaySerial = 3},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.Others, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.Miscellaneous, Name = "Others", DisplaySerial = 5},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.CorporateAdvisory, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.CorporateAdvisory, Name = "Corporate Advisory", DisplaySerial = 1},
            //    new AdvanceCategory { Id = (long)AdvanceCategoryEnum.Procurement, BaseAdvanceCategoryId = (long)BaseAdvanceCategoryEnum.Miscellaneous, Name = "Procurement", DisplaySerial = 4}
            //    );

            //context.ApprovalPanels.AddOrUpdate(
            //    new ApprovalPanel { Id = 1, Name = "Advance Travel Requisition Approval Panel", ApprovalPanelTypeId = 1, CreatedOn = DateTime.Now },
            //    new ApprovalPanel { Id = 2, Name = "Advance Petty Cash Requisition Approval Panel", ApprovalPanelTypeId = 1, CreatedOn = DateTime.Now }
            //    );

            //context.PlaceOfVisits.AddOrUpdate(
            //         new PlaceOfVisit { Id = 1, Name = "Europe" },
            //         new PlaceOfVisit { Id = 2, Name = "North America" },
            //         new PlaceOfVisit { Id = 3, Name = "Middle East" },
            //         new PlaceOfVisit { Id = 4, Name = "South America" },
            //         new PlaceOfVisit { Id = 5, Name = "Japan" },
            //         new PlaceOfVisit { Id = 6, Name = "Korea" },
            //         new PlaceOfVisit { Id = 7, Name = "Hong Kong" },
            //         new PlaceOfVisit { Id = 8, Name = "Singapore" },
            //         new PlaceOfVisit { Id = 9, Name = "Australia" },
            //         new PlaceOfVisit { Id = 10, Name = "New Zealand" },
            //         new PlaceOfVisit { Id = 11, Name = "Others" }
            //    );

            //context.ApprovalStatuses.AddOrUpdate(
            //    new ApprovalStatus { Id = (long)ApprovalStatusEnum.ApprovalInitiated, Name = ApprovalStatusEnum.ApprovalInitiated.ToString() },
            //    new ApprovalStatus { Id = (long)ApprovalStatusEnum.SentForApproval, Name = ApprovalStatusEnum.SentForApproval.ToString() },
            //    new ApprovalStatus { Id = (long)ApprovalStatusEnum.WaitingForApproval, Name = ApprovalStatusEnum.WaitingForApproval.ToString() },
            //    new ApprovalStatus { Id = (long)ApprovalStatusEnum.Reverted, Name = ApprovalStatusEnum.Reverted.ToString() },
            //    new ApprovalStatus { Id = (long)ApprovalStatusEnum.Approved, Name = ApprovalStatusEnum.Approved.ToString() },
            //    new ApprovalStatus { Id = (long)ApprovalStatusEnum.Rejected, Name = ApprovalStatusEnum.Rejected.ToString() },
            //    new ApprovalStatus { Id = (long)ApprovalStatusEnum.ApprovalSkipped, Name = ApprovalStatusEnum.ApprovalSkipped.ToString() }
            //    );

            //context.HistoryOperationModes.AddOrUpdate(
            //        new HistoryOperationMode[]
            //        {
            //            new HistoryOperationMode(){Id=(long) HistoryOperationModeEnum.Add, Name = HistoryOperationModeEnum.Add.ToString()}, 
            //            new HistoryOperationMode(){Id=(long) HistoryOperationModeEnum.Update, Name = HistoryOperationModeEnum.Update.ToString()}, 
            //            new HistoryOperationMode(){Id=(long) HistoryOperationModeEnum.Delete, Name = HistoryOperationModeEnum.Delete.ToString()}, 
            //        }
            //    );

            //context.TicketTypes.AddOrUpdate(
            //    new TicketType { Id = (long)TicketTypeEnum.Requisition, Name = TicketTypeEnum.Requisition.ToString() },
            //    new TicketType { Id = (long)TicketTypeEnum.Expense, Name = TicketTypeEnum.Expense.ToString() }
            //    );

            //context.VatTaxTypes.AddOrUpdate(
            //    new VatTaxType { Id = (long)VatTaxTypeEnum.VAT, Name = VatTaxTypeEnum.VAT.ToString() },
            //    new VatTaxType { Id = (long)VatTaxTypeEnum.TAX, Name = VatTaxTypeEnum.TAX.ToString() }
            //    );

            //context.HistoryModes.AddOrUpdate(
            //    new HistoryMode { Id = (long)HistoryModeEnum.Create, Name = HistoryModeEnum.Create.ToString() },
            //    new HistoryMode { Id = (long)HistoryModeEnum.Edit, Name = HistoryModeEnum.Edit.ToString() },
            //    new HistoryMode { Id = (long)HistoryModeEnum.Delete, Name = HistoryModeEnum.Delete.ToString() }
            //    );

            //context.VoucherStatuses.AddOrUpdate(
            //    new VoucherStatus { Id = (long)VoucherStatusEnum.Draft, Name = VoucherStatusEnum.Draft.ToString() },
            //    new VoucherStatus { Id = (long)VoucherStatusEnum.Sent, Name = VoucherStatusEnum.Sent.ToString() },
            //    new VoucherStatus { Id = (long)VoucherStatusEnum.Posted, Name = VoucherStatusEnum.Posted.ToString() }
            //    );

            //context.AccountTypes.AddOrUpdate(
            //    new AccountType() { Id = (long)AccountTypeEnum.AdvanceAccount, Name = "Advance Account" },
            //    new AccountType() { Id = (long)AccountTypeEnum.ExpenseAccount, Name = "Expense Account" },
            //    new AccountType() { Id = (long)AccountTypeEnum.VatAccount, Name = "Vat Account" },
            //    new AccountType() { Id = (long)AccountTypeEnum.TaxAccount, Name = "Tax Account" },
            //    new AccountType() { Id = (long)AccountTypeEnum.BankPaymentAccount, Name = "Bank Payment Account" },
            //    new AccountType() { Id = (long)AccountTypeEnum.BankDepositAccount, Name = "Bank Deposit Account" },
            //    new AccountType() { Id = (long)AccountTypeEnum.ReimbursementPayableAccount, Name = "Reimbursement Payable Account" },
            //    new AccountType() { Id = (long)AccountTypeEnum.RefundReceiveableAccount, Name = "Refund Receiveable Account" }
            //    );

            //context.NotificationTypes.AddOrUpdate(
            //    new NotificationType() { Id = (long)NotificaitonTypeEnum.Requisition, Name = NotificaitonTypeEnum.Requisition.ToString() },
            //    new NotificationType() { Id = (long)NotificaitonTypeEnum.Expense, Name = NotificaitonTypeEnum.Expense.ToString() }
            //    );
        }
    }
}

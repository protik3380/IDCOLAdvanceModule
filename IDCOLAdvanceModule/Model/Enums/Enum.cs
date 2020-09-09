
namespace IDCOLAdvanceModule.Model.Enums
{
    public enum AdvanceStatusEnum
    {
        Draft = 1,
        ApprovalOnProgress = 2,
        Reverted = 3,
        Approved = 4,
        Rejected = 5,
        Removed = 6
    }

    public enum BaseAdvanceCategoryEnum
    {
        Travel = 1,
        PettyCash = 2,
        Miscellaneous = 3,
        CorporateAdvisory = 4
    }

    public enum AdvanceCategoryEnum
    {
        LocalTravel = 1,
        OversearTravel = 2,
        PettyCash = 3,
        Event = 4,
        TrainingAndWorkshop = 5,
        Meeting = 6,
        Others = 7,
        CorporateAdvisory = 8,
        Procurement = 9
    }

    public enum EmployeeCategoryEnum
    {
        Executive = 1,
        ExecutiveGroupA = 2,
        ExecutiveGroupB = 3,
        ExecutiveGroupC = 4
    }

    public enum AdvancedFormMode
    {
        Create = 1,
        View = 2,
        Update = 3
    }

    public enum ApprovalStatusEnum
    {
        ApprovalInitiated = 1,
        SentForApproval = 2,
        WaitingForApproval = 3,
        Reverted = 4,
        Approved = 5,
        Rejected = 6,
        ApprovalSkipped = 7
    }

    public enum HistoryOperationModeEnum
    {
        Add = 1,
        Update = 2,
        Delete = 3
    }

    public enum AdvanceExpenseTypeEnum
    {
        New = 1,
        Recommended = 2,
        Verified = 3,
        Approved = 4,
        Draft = 5
    }

    public enum TicketTypeEnum
    {
        Requisition = 1,
        Expense = 2
    }

    public enum EntryTypeEnum
    {
        Requisition = 1,
        Expense = 2
    }

    public enum VatTaxTypeEnum
    {
        VAT = 1,
        TAX = 2
    }

    public enum HistoryModeEnum
    {
        Create = 1,
        Edit = 2,
        Delete = 3
    }

    public enum VoucherStatusEnum
    {
        Draft = 1,
        Sent = 2,
        Posted = 3
    }

    public enum DebitCreditTypeEnum
    {
        Debit = 1,
        Credit = 2
    }

    public enum SourceStatusEnum
    {
        RequisitionPayement = 1,
        ExpensePayment = 2
    }

    public enum AccountTypeEnum
    {
        AdvanceAccount = 1,
        ExpenseAccount = 2,
        VatAccount = 3,
        TaxAccount = 4,
        BankPaymentAccount = 5,
        BankDepositAccount = 6,
        ReimbursementPayableAccount = 7,
        RefundReceiveableAccount = 8
    }

    public enum NotificaitonTypeEnum
    {
        Requisition = 1,
        Expense = 2
    }
}

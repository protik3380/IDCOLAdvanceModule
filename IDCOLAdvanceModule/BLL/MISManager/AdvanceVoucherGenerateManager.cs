using System;
using System.Collections.Generic;
using System.Linq;
using Accounts;
using Accounts.BLL;
using Accounts.NerdCastle.NCBLL;
using Accounts.NerdCastle.NCDAO;
using Accounts.NerdCastle.Utility;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Expense;
using IDCOLAdvanceModule.Model.EntityModels.Voucher.Requisition;
using IDCOLAdvanceModule.Model.Enums;
using IDCOLAdvanceModule.Model.Interfaces.IManager.IMISManager;

namespace IDCOLAdvanceModule.BLL.MISManager
{
    public class AdvanceVoucherGenerateManager
    {
        private IBankManager _bankManager;
        private IBranchManager _branchManager;
        private Accounts.BLL.AccountManager _misAccountManager = new Accounts.BLL.AccountManager();
        private readonly IChartOfAccountManager _chartOfAccountManager;
        private TransactionMasterManager _transactionMasterManager;
        private Accounts_NC_VatTaxCategoryManager _accountsNcVatTaxCategoryManager;
        private Accounts_NC_VendorInfoManager _accountsNcVendorInfoManager;


        public AdvanceVoucherGenerateManager()
        {
            _bankManager = new BankManager();
            _branchManager = new BranchManager();
            _chartOfAccountManager = new ChartOfAccountManager();
            _transactionMasterManager = new TransactionMasterManager();
            _accountsNcVatTaxCategoryManager = new Accounts_NC_VatTaxCategoryManager();
            _accountsNcVendorInfoManager = new Accounts_NC_VendorInfoManager();
        }


        public TransactionVoucher GetTransactionVoucherFromRequisitionVoucher(RequisitionVoucherHeader requisitionVoucherHeader)
        {
            if (requisitionVoucherHeader == null)
            {
                return null;
            }

            var activefiscalYear = Accounts.FiscalYear.GetActiveYear();
            
            TransactionVoucher transactionVoucher = new TransactionVoucher();
            if (requisitionVoucherHeader.BankId != null)
            {
                var bank = _bankManager.GetById((long)requisitionVoucherHeader.BankId);
                transactionVoucher.BankId = Convert.ToInt32(bank.Bank_ID);
                transactionVoucher.BankAddress = bank.Bank_Address;
                transactionVoucher.BankName = bank.Bank_Name;
            }
            if (requisitionVoucherHeader.BranchId != null)
            {
                var branch = _branchManager.GetById((long)requisitionVoucherHeader.BranchId);
                transactionVoucher.BranchId = Convert.ToInt32(branch.Branch_ID);
                transactionVoucher.BranchName = branch.Branch_Name;
                transactionVoucher.BranchAddress = branch.Address;
            }

            transactionVoucher.VoucherTypeId = (int) requisitionVoucherHeader.VoucherTypeId;
            transactionVoucher.EntryDate = requisitionVoucherHeader.VoucherEntryDate;
            transactionVoucher.CreatedBy = requisitionVoucherHeader.CreatedBy;
            transactionVoucher.EntryNo = transactionVoucher.GetEntryNo();
            transactionVoucher.FiscalYearId = activefiscalYear.FiscalYearId;
            transactionVoucher.FiscalPeriod =
                FiscalYear.FiscalPeriod(transactionVoucher.EntryDate.Month).ToString();

            transactionVoucher.CurrencyCode = requisitionVoucherHeader.Currency;
            transactionVoucher.ConversionRate = Convert.ToDouble(requisitionVoucherHeader.ConversionRate);
            transactionVoucher.VoucherDesc = requisitionVoucherHeader.VoucherDescription;
            transactionVoucher.GlSource = "Advance";
            transactionVoucher.SourceCode = "Advance";

            transactionVoucher.VoucherOwner = requisitionVoucherHeader.RecipientName;

            transactionVoucher.ChequeNo = requisitionVoucherHeader.ChequeNo;
            transactionVoucher.ChequeDate = requisitionVoucherHeader.VoucherEntryDate;

            transactionVoucher.FiscalYear = transactionVoucher.EntryDate.Year.ToString();
            transactionVoucher.OnBalance = true;
            //transactionVoucher.DateFrom =DateTime.Now;
            //transactionVoucher.DateTo =DateTime.Now;


            List<TransactionDetail> transactionDetails = new List<TransactionDetail>();
            transactionVoucher.Details = transactionDetails;

            foreach (var detail in requisitionVoucherHeader.RequisitionVoucherDetails)
            {

                TransactionDetail transactionDetail = new TransactionDetail();
                transactionDetail.TransactionId = transactionVoucher.TransactionId;
                transactionDetail.AccountCode = detail.AccountCode;
                transactionDetail.AccountDesc = _chartOfAccountManager.GetByCode(detail.AccountCode).AccountDesc;
                transactionDetail.Description = detail.Description;
                transactionDetail.ConvRate = Convert.ToDouble(requisitionVoucherHeader.ConversionRate);
                transactionDetail.SCurnCode = requisitionVoucherHeader.Currency;

                transactionDetail.BilledAmount = 0.0;
                if (detail.CrAmount != null)
                {
                    transactionDetail.CreditAmount = Convert.ToDouble(detail.CrAmount);
                }
                else if (detail.DrAmount != null)
                {
                    transactionDetail.DebitAmount = Convert.ToDouble(detail.DrAmount);
                }
                var account = _chartOfAccountManager.GetByCode(detail.AccountCode);
                if (account.IsVatTaxAccount)
                {
                    transactionDetail.VatTaxPercentage = detail.Percentage;
                    transactionDetail.VendorInfoId = detail.VendorId;
                    transactionDetail.VatTaxCategoryId = detail.VatTaxCategoryId;
                    var vendor = _accountsNcVendorInfoManager.GetVendorInfo((int)detail.VendorId);
                    Accounts_NC_VatTaxCategory category =
                        _accountsNcVatTaxCategoryManager.GetVatTaxCategory((int) detail.VatTaxCategoryId);
                    transactionDetail.Vendor = vendor;
                    transactionDetail.VatTaxCategory = category;
                    transactionDetail.IsVatTaxTransaction = true;

                }
                transactionDetail.IsNet = false;

                transactionDetail.IsNet = detail.IsNet;

                transactionDetails.Add(transactionDetail);
            }


            return transactionVoucher;
        }
        public bool InitiateVoucher(TransactionVoucher voucher)
        {
            _transactionMasterManager = new TransactionMasterManager();
            bool isVoucherAdded = _transactionMasterManager.InsertVoucher(voucher);
            return isVoucherAdded;
        }

        public Accounts_NC_Integration GetIntegrationInstance(TransactionVoucher voucher, RequisitionVoucherHeader requisitionVoucherHeader)
        {
            var integration = new Accounts_NC_Integration()
            {
                GLSource = "Advance",
                TransactionId = voucher.TransactionId,
                EntryDate = voucher.EntryDate,
                SourceState = SourceStatusEnum.RequisitionPayement.ToString(),
                CreatedBy = voucher.CreatedBy,
                IsCurrent = true
            };

            var integrationDetails = new List<Accounts_NC_IntegrationDetail>();
            Accounts_NC_IntegrationDetail integrationDetail = new Accounts_NC_IntegrationDetail();
            integrationDetail.IntegrationId = integration.Id;
            integrationDetail.SourceId = requisitionVoucherHeader.Id;
            var sum = requisitionVoucherHeader.RequisitionVoucherDetails.Sum(c => c.DrAmount??0);
            integrationDetail.SourceAmount = sum;

            integrationDetails.Add(integrationDetail);
            integration.IntegrationDetails = integrationDetails;
            return integration;

        }

        public Accounts_NC_Integration GetIntegrationInstance(TransactionVoucher voucher, ExpenseVoucherHeader expenseVoucherHeader)
        {
            var integration = new Accounts_NC_Integration()
            {
                GLSource = "Advance",
                TransactionId = voucher.TransactionId,
                EntryDate = voucher.EntryDate,
                SourceState = SourceStatusEnum.ExpensePayment.ToString(),
                CreatedBy = voucher.CreatedBy,
                IsCurrent = true
            };

            var integrationDetails = new List<Accounts_NC_IntegrationDetail>();
            Accounts_NC_IntegrationDetail integrationDetail = new Accounts_NC_IntegrationDetail();
            integrationDetail.IntegrationId = integration.Id;
            integrationDetail.SourceId = expenseVoucherHeader.Id;
            var sum = expenseVoucherHeader.ExpenseVoucherDetails.Sum(c => c.DrAmount);
            integrationDetail.SourceAmount = (decimal)(sum ?? 0);

            integrationDetails.Add(integrationDetail);
            integration.IntegrationDetails = integrationDetails;
            return integration;
        }
        public bool InsertIntegration(Accounts_NC_Integration integration)
        {
            _transactionMasterManager = new TransactionMasterManager();
            bool isIntegrationInserted = _transactionMasterManager.InsertIntegration(integration);
            return isIntegrationInserted;
        }

        public TransactionVoucher GetTransactionVoucherFromExpenseVoucher(ExpenseVoucherHeader expenseVoucherHeader)
        {
            if (expenseVoucherHeader == null)
            {
                return null;
            }

            var activefiscalYear = Accounts.FiscalYear.GetActiveYear();

            TransactionVoucher transactionVoucher = new TransactionVoucher();
            if (expenseVoucherHeader.BankId != null)
            {
                var bank = _bankManager.GetById((long)expenseVoucherHeader.BankId);
                transactionVoucher.BankId = Convert.ToInt32(bank.Bank_ID);
                transactionVoucher.BankAddress = bank.Bank_Address;
                transactionVoucher.BankName = bank.Bank_Name;
            }
            if (expenseVoucherHeader.BranchId != null)
            {
                var branch = _branchManager.GetById((long)expenseVoucherHeader.BranchId);
                transactionVoucher.BranchId = Convert.ToInt32(branch.Branch_ID);
                transactionVoucher.BranchName = branch.Branch_Name;
                transactionVoucher.BranchAddress = branch.Address;
            }

            transactionVoucher.VoucherTypeId = (int) expenseVoucherHeader.VoucherTypeId;
            transactionVoucher.EntryDate = expenseVoucherHeader.VoucherEntryDate;
            transactionVoucher.CreatedBy = expenseVoucherHeader.CreatedBy;
            transactionVoucher.EntryNo = transactionVoucher.GetEntryNo();
            transactionVoucher.FiscalYearId = activefiscalYear.FiscalYearId;
            transactionVoucher.FiscalPeriod =
                FiscalYear.FiscalPeriod(transactionVoucher.EntryDate.Month).ToString();

            transactionVoucher.CurrencyCode = expenseVoucherHeader.Currency;
            transactionVoucher.ConversionRate = Convert.ToDouble(expenseVoucherHeader.ConversionRate);
            transactionVoucher.VoucherDesc = expenseVoucherHeader.VoucherDescription;
            transactionVoucher.GlSource = "Advance";
            transactionVoucher.SourceCode = "Advance";

            transactionVoucher.VoucherOwner = expenseVoucherHeader.RecipientName;

            transactionVoucher.ChequeNo = expenseVoucherHeader.ChequeNo;
            transactionVoucher.ChequeDate = expenseVoucherHeader.VoucherEntryDate;

            transactionVoucher.FiscalYear = transactionVoucher.EntryDate.Year.ToString();
            transactionVoucher.OnBalance = true;
            //transactionVoucher.DateFrom =DateTime.Now;
            //transactionVoucher.DateTo =DateTime.Now;


            List<TransactionDetail> transactionDetails = new List<TransactionDetail>();
            transactionVoucher.Details = transactionDetails;

            foreach (var detail in expenseVoucherHeader.ExpenseVoucherDetails)
            {

                TransactionDetail transactionDetail = new TransactionDetail();
                transactionDetail.TransactionId = transactionVoucher.TransactionId;
                transactionDetail.AccountCode = detail.AccountCode;
                transactionDetail.AccountDesc = _chartOfAccountManager.GetByCode(detail.AccountCode).AccountDesc;
                transactionDetail.Description = detail.Description;
                transactionDetail.ConvRate = Convert.ToDouble(expenseVoucherHeader.ConversionRate);
                transactionDetail.SCurnCode = expenseVoucherHeader.Currency;

                transactionDetail.BilledAmount = 0.0;
                if (detail.CrAmount != null)
                {
                    transactionDetail.CreditAmount = Convert.ToDouble(detail.CrAmount);
                }
                else if (detail.DrAmount != null)
                {
                    transactionDetail.DebitAmount = Convert.ToDouble(detail.DrAmount);
                }

                var account = _chartOfAccountManager.GetByCode(detail.AccountCode);
                if (account.IsVatTaxAccount)
                {
                    transactionDetail.VatTaxPercentage = detail.Percentage;
                    transactionDetail.VendorInfoId = detail.VendorId;
                    transactionDetail.VatTaxCategoryId = detail.VatTaxCategoryId;
                    var vendor = _accountsNcVendorInfoManager.GetVendorInfo((int)detail.VendorId);
                    Accounts_NC_VatTaxCategory category =
                        _accountsNcVatTaxCategoryManager.GetVatTaxCategory((int)detail.VatTaxCategoryId);
                    transactionDetail.Vendor = vendor;
                    transactionDetail.VatTaxCategory = category;
                    transactionDetail.IsVatTaxTransaction = true;

                }

                transactionDetail.IsNet = false;

                transactionDetail.IsNet = detail.IsNet;

                transactionDetails.Add(transactionDetail);
            }


            return transactionVoucher;
        }


    }
}

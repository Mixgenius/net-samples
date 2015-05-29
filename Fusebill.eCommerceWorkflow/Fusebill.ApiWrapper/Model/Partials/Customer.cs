using System;
using System.Collections.Generic;
using System.Linq;
using DataCommon.DataStructures;
using DataCommon.Models;
using Model.Interfaces;
using Newtonsoft.Json;

namespace Model
{
    public partial class Customer : IIntegrationSyncable
    {
        [JsonIgnore]
        public CustomerAccountStatusJournal LatestCustomerAccountStatusJournal
        {
            get
            {
                if (CustomerAccountStatusJournals == null || CustomerAccountStatusJournals.Count == 0) throw new FusebillException("Customer is missing CustomerAccountStatusJournals. Customer CustomerAccountStatusJournals should be included in the framework.");

                return CustomerAccountStatusJournals.Single(c => c.IsActive);
            }
        }

        [JsonIgnore]
        public CustomerStatusJournal LatestCustomerStatusJournal
        {
            get
            {
                if (CustomerStatusJournals == null || CustomerStatusJournals.Count == 0) throw new FusebillException("Customer is missing CustomerStatusJournals. Customer CustomerStatusJournals should be included in the framework.");

                return CustomerStatusJournals.Single(c => c.IsActive);
            }
        }

        [JsonIgnore]
        public List<BillingPeriod> OpenBillingPeriods
        {
            get
            {
                if (BillingPeriods.Count == 0) throw new FusebillException("Customer is missing BillingPeriods. Customer BillingPeriods should be included in the framework.");

                return BillingPeriods.Where(b => b.PeriodStatus == PeriodStatus.Open).ToList();
            }
        }

        /// <summary>
        /// Gets the current open billing periods in a collection that can safely be iterated through to close them
        /// </summary>
        /// <returns></returns>
        public List<BillingPeriod> GetOpenBillingPeriods()
        {
            var openBillingPeriods = new List<BillingPeriod>();
            openBillingPeriods.AddRange(OpenBillingPeriods);
            return openBillingPeriods;
        }

        public CustomerStatus GetPreviousStatus()
        {
            return
                CustomerStatusJournals.Where(csj => !csj.IsActive && csj.Status != Status)
                    .OrderByDescending(csj => csj.Id)
                    .First().Status;
        }

        public void CreateLedgerEntriesFromTransaction(Transaction transaction)
        {
            SetSortOrder(transaction);

            var debitLedger = new LedgerEntry
            {
                CustomerId = Id,
                Debit = transaction.Amount,
                Transaction = transaction,
                CurrencyId = CurrencyId
            };

            var creditLedger = new LedgerEntry
            {
                CustomerId = Id,
                Credit = transaction.Amount,
                Transaction = transaction,
                CurrencyId = CurrencyId
            };

            var customerLedgerJournal = new CustomerLedgerJournal
            {
                Transaction = transaction
            };

            switch (transaction.TransactionType)
            {
                case TransactionType.Purchase:
                case TransactionType.SetupFee:
                case TransactionType.Charge:
                    debitLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.DeferredRevenue;
                    customerLedgerJournal.UnearnedCredit = transaction.Amount;
                    break;
                case TransactionType.Payment:
                    debitLedger.LedgerType = LedgerType.CashCollected;
                    customerLedgerJournal.CashDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.PartialRefund:
                case TransactionType.FullRefund:
                    debitLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.CashCollected;
                    customerLedgerJournal.CashCredit = transaction.Amount;
                    break;
                case TransactionType.Earning:
                    debitLedger.LedgerType = LedgerType.DeferredRevenue;
                    customerLedgerJournal.UnearnedDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.EarnedRevenue;
                    customerLedgerJournal.EarnedCredit = transaction.Amount;
                    break;
                case TransactionType.EarningDiscount:
                    debitLedger.LedgerType = LedgerType.Discount;
                    customerLedgerJournal.DiscountDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.DeferredDiscount;
                    customerLedgerJournal.UnearnedDiscountCredit = transaction.Amount;
                    break;
                case TransactionType.ReverseSetupFee:
                case TransactionType.ReverseCharge:
                    debitLedger.LedgerType = LedgerType.DeferredRevenue;
                    customerLedgerJournal.UnearnedDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.ReverseChargeEarned:
                    debitLedger.LedgerType = LedgerType.EarnedRevenue;
                    customerLedgerJournal.EarnedDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.ReverseEarning:
                    debitLedger.LedgerType = LedgerType.EarnedRevenue;
                    customerLedgerJournal.EarnedDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.DeferredRevenue;
                    customerLedgerJournal.UnearnedCredit = transaction.Amount;
                    break;
                case TransactionType.WriteOff:
                    debitLedger.LedgerType = LedgerType.WriteOff;
                    customerLedgerJournal.WriteOffDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.Tax:
                    debitLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.TaxesPayable;
                    customerLedgerJournal.TaxesPayableCredit = transaction.Amount;
                    break;
                case TransactionType.ReverseTax:
                    debitLedger.LedgerType = LedgerType.TaxesPayable;
                    customerLedgerJournal.TaxesPayableDebit = transaction.Amount;
                    
                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.Discount:
                    debitLedger.LedgerType = LedgerType.Discount;
                    customerLedgerJournal.DiscountDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.DeferredDiscount:
                    debitLedger.LedgerType = LedgerType.DeferredDiscount;
                    customerLedgerJournal.UnearnedDiscountDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.ReverseDiscount:
                    creditLedger.LedgerType = LedgerType.Discount;
                    customerLedgerJournal.DiscountCredit = transaction.Amount;

                    debitLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArDebit = transaction.Amount;
                    break;
                case TransactionType.ReverseDeferredDiscount:
                    creditLedger.LedgerType = LedgerType.DeferredDiscount;
                    customerLedgerJournal.UnearnedDiscountCredit = transaction.Amount;

                    debitLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArDebit = transaction.Amount;
                    break;
                case TransactionType.Credit:
                    debitLedger.LedgerType = LedgerType.Credit;
                    customerLedgerJournal.CreditDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.Debit:
                    debitLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.Credit;
                    customerLedgerJournal.CreditCredit = transaction.Amount;
                    break;
                case TransactionType.OpeningBalance:
                    debitLedger.LedgerType = LedgerType.OpeningBalance;
                    customerLedgerJournal.OpeningBalanceDebit = transaction.Amount;

                    creditLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArCredit = transaction.Amount;
                    break;
                case TransactionType.OpeningBalanceOwing:
                    creditLedger.LedgerType = LedgerType.OpeningBalance;
                    customerLedgerJournal.OpeningBalanceCredit = transaction.Amount;

                    debitLedger.LedgerType = LedgerType.ARBalance;
                    customerLedgerJournal.ArDebit = transaction.Amount;
                    break;
                default:
                    throw new FusebillException("Unknown transaction type");
            }

            transaction.LedgerEntries.Add(debitLedger);
            transaction.LedgerEntries.Add(creditLedger);
            transaction.CustomerLedgerJournals.Add(customerLedgerJournal);

            // Update stored AR Balance
            ArBalance += customerLedgerJournal.ArDebit;
            ArBalance -= customerLedgerJournal.ArCredit;
        }

        private void SetSortOrder(Transaction transaction)
        {
            if (transaction.SortOrder == 0)
            {
                switch (transaction.TransactionType)
                {
                    case TransactionType.OpeningBalance:
                    case TransactionType.OpeningBalanceOwing:
                        transaction.SortOrder = 6;
                        break;
                    case TransactionType.Tax:
                        transaction.SortOrder = 7;
                        break;
                    case TransactionType.DeferredDiscount:
                    case TransactionType.Discount:
                        transaction.SortOrder = 8;
                        break;
                    case TransactionType.ReverseCharge:
                    case TransactionType.ReverseSetupFee:
                    case TransactionType.ReverseChargeEarned:
                        transaction.SortOrder = 9;
                        break;
                    case TransactionType.ReverseTax:
                        transaction.SortOrder = 10;
                        break;
                    case TransactionType.ReverseDeferredDiscount:
                    case TransactionType.ReverseDiscount:
                        transaction.SortOrder = 11;
                        break;
                    case TransactionType.Payment:
                    case TransactionType.Credit:
                    case TransactionType.WriteOff:
                        transaction.SortOrder = 12;
                        break;
                    case TransactionType.FullRefund:
                    case TransactionType.PartialRefund:
                    case TransactionType.Debit:
                        transaction.SortOrder = 13;
                        break;
                    default:
                        transaction.SortOrder = 99;
                        break;
                }
            }
        }

        public InvoicePreview InvoicePreview { get; set; }
        public List<InvoicePreview> AdditionalInvoicesForPreview { get; set; }
        public DraftInvoiceDisplayOptions DraftInvoiceDisplayOptions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using DataCommon.Helpers;
using DataCommon.Models;

namespace Model.Factories.Implementation
{
    public class InvoiceFactory : IInvoiceFactory
    {
        private readonly ITimeOfTransaction _timeOfTransaction;

        public InvoiceFactory(ITimeOfTransaction timeOfTransaction)
        {
            _timeOfTransaction = timeOfTransaction;
        }

        public Invoice Create(BillingPeriod billingPeriod, long draftInvoiceId, DateTime effectiveTimestamp, DateTime postedTimestamp, Customer customer, Account account, string poNumber, string invoiceNotes)
        {
            var defaultPoNumber = string.Empty;
            if (customer != null && customer.CustomerBillingSetting != null)
                defaultPoNumber = customer.CustomerBillingSetting.StandingPo;

            var defaultInvoiceNotes = string.Empty;
            if (account != null && account.AccountPreference != null &&
                account.AccountPreference.AccountInvoicePreference != null)
                defaultInvoiceNotes = account.AccountPreference.AccountInvoicePreference.InvoiceNote;

            var invoice = new Invoice(_timeOfTransaction, billingPeriod, draftInvoiceId, effectiveTimestamp, postedTimestamp, customer)
            {
                AccountId = account.Id,
                PoNumber = OverridePattern.GetOverrideOrDefault(poNumber, defaultPoNumber),
                Notes = OverridePattern.GetOverrideOrDefault(invoiceNotes, defaultInvoiceNotes),
                ChargeGroups = new List<ChargeGroup>()
            };
            return invoice;
        }

        public static Invoice GenerateSampleData()
        {
            var timeOfTransaction = new TimeOfTransaction();

            var invoiceJournalEntry = new InvoiceJournal
            {
                Id = 1,
                SumOfCharges = 99.99m
            };

            var paymentSchedule1 = new PaymentSchedule
            {
                PaymentScheduleJournals = new List<PaymentScheduleJournal> { 
                    new PaymentScheduleJournal {
                        DueDate = timeOfTransaction.Timestamp,
                        Status = InvoiceStatus.Due,
                        OutstandingBalance = 5,
                        IsActive = true
                    }
                },
                DaysDueAfterTerm = 0,
                Amount = 5
            };

            var paymentSchedule2 = new PaymentSchedule
            {
                PaymentScheduleJournals = new List<PaymentScheduleJournal> { 
                    new PaymentScheduleJournal {
                        DueDate = timeOfTransaction.Timestamp.AddDays(10),
                        Status = InvoiceStatus.Due,
                        OutstandingBalance = 5,
                        IsActive = true
                    }
                },
                DaysDueAfterTerm = 10,
                Amount = 5
            };

            var customer = new Customer
            {
                CustomerBillingSetting = new CustomerBillingSetting { Term = Term.Net15 },
                CustomerAddressPreference = new CustomerAddressPreference(),
                CurrencyId = 1
            };

            var account = new Account
            {
                Id = 1,
                AccountPreference = new AccountPreference
                {
                    Timezone = new Timezone {ClrId = "Eastern Standard Time"}
                },
                AccountBillingPreference = new AccountBillingPreference
                {
                    ShowZeroDollarCharges = true
                }
            };
 
            return new Invoice(timeOfTransaction, new BillingPeriod { Customer = customer }, 1, timeOfTransaction.Timestamp, timeOfTransaction.Timestamp, customer)
            {
                Id = 1,
                InvoiceJournals = new List<InvoiceJournal> { invoiceJournalEntry },
                PaymentSchedules = new List<PaymentSchedule> { paymentSchedule1, paymentSchedule2 },
                AccountId = 1
            };
        }
    }
}

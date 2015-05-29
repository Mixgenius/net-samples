using System;
using System.Linq;
using DataCommon.Models;
using Model.Interfaces;
using Newtonsoft.Json;

namespace Model
{
    public partial class Invoice : IIntegrationSyncable
    {
        internal Invoice(ITimeOfTransaction timeOfTransaction, BillingPeriod billingPeriod, long draftInvoiceId, DateTime effectiveTimestamp, DateTime postedTimestamp, Customer customer)
            : this()
        {
            TimeOfTransacation = timeOfTransaction;
            BillingPeriod = billingPeriod;
            DraftInvoiceId = draftInvoiceId;
            EffectiveTimestamp = effectiveTimestamp;
            PostedTimestamp = postedTimestamp;
            Customer = customer;

            // Default to empty because it must be set after the invoice is inserted to DB
            Signature = String.Empty;

            InitializeInvoiceCustomerProperties(customer);
        }

        private void InitializeInvoiceCustomerProperties(Customer customer)
        {
            if (null == customer)
                return;

            InvoiceCustomer = new InvoiceCustomer
                {
                    CreatedTimestamp = customer.CreatedTimestamp,
                    ModifiedTimestamp = customer.ModifiedTimestamp,
                    EffectiveTimestamp = customer.EffectiveTimestamp,
                    Id = customer.Id,
                    Title = customer.Title.ToString(),
                    FirstName = customer.FirstName,
                    MiddleName = customer.MiddleName,
                    LastName = customer.LastName,
                    Suffix = customer.Suffix,
                    PrimaryEmail = customer.PrimaryEmail,
                    PrimaryPhone = customer.PrimaryPhone,
                    Reference = customer.Reference,
                    CurrencyId = customer.CurrencyId,
                    Currency = customer.Currency,
                    CompanyName = customer.CompanyName
                };

            if (customer.CustomerAddressPreference != null)
            {
                InvoiceCustomer.ContactName = customer.CustomerAddressPreference.ContactName;
                InvoiceCustomer.ShippingInstructions = customer.CustomerAddressPreference.ShippingInstructions;
            }

            if (customer.CustomerAddressPreference != null && customer.CustomerAddressPreference.Addresses != null)
            {
                foreach (var address in customer.CustomerAddressPreference.Addresses)
                {
                    var invoiceAddress = new InvoiceAddress
                    {
                            Line1 = address.Line1,
                            Line2 = address.Line2,
                            CompanyName = address.CompanyName,
                            PostalZip = address.PostalZip,
                            City = address.City,
                            CountryId = address.CountryId,
                            StateId = address.StateId,
                            AddressType = address.AddressType,
                            CreatedTimestamp = address.CreatedTimestamp,
                            ModifiedTimestamp = address.ModifiedTimestamp,
                            Country = address.Country.Name,
                            State = address.State == null ? String.Empty : address.State.Name
                        };

                    InvoiceAddresses.Add(invoiceAddress);
                }

                CopyBillingAddressIfUseBillingAddressAsShippingIsSet(customer);
            }
        }

        private void CopyBillingAddressIfUseBillingAddressAsShippingIsSet(Customer customer)
        {
            if (customer.CustomerAddressPreference.UseBillingAddressAsShippingAddress)
            {
                InvoiceAddresses.Remove(InvoiceAddresses.FirstOrDefault(a => a.AddressType == AddressType.Shipping));

                var billingAddress = InvoiceAddresses.FirstOrDefault(a => a.AddressType == AddressType.Billing);
                if (billingAddress != null)
                    InvoiceAddresses.Add(new InvoiceAddress
                    {
                        Line1 = billingAddress.Line1,
                        Line2 = billingAddress.Line2,
                        CompanyName = billingAddress.CompanyName,
                        PostalZip = billingAddress.PostalZip,
                        City = billingAddress.City,
                        CountryId = billingAddress.CountryId,
                        StateId = billingAddress.StateId,
                        AddressType = AddressType.Shipping,
                        CreatedTimestamp = billingAddress.CreatedTimestamp,
                        ModifiedTimestamp = billingAddress.ModifiedTimestamp,
                        Country = billingAddress.Country,
                        State = billingAddress.State
                    });
            }
        }

        public void MakePayment(decimal amount, Payment payment)
        {
            if (amount > Balance) throw new FusebillException("Payment cannot exceed the balance");

            MakePaymentForReversal(amount, payment);
        }

        public void MakePaymentForReversal(decimal amount, Payment payment)
        {
            var paymentNote = new PaymentNote
            {
                Amount = amount,
                Payment = payment,
                PaymentId = payment.Id,
                EffectiveTimestamp = TimeOfTransacation.Timestamp,
                InvoiceId = Id
            };

            payment.PaymentNotes.Add(paymentNote);

            PaymentNotes.Add(paymentNote);
        }

        public void MakeCredit(CreditAllocation creditAllocation, Credit credit)
        {
            if (creditAllocation.Amount > Balance) throw new FusebillException("Credit cannot exceed the balance");

            MakeCreditForReversal(creditAllocation, credit);
        }

        public void MakeCreditForReversal(CreditAllocation creditAllocation, Credit credit)
        {
            creditAllocation.EffectiveTimestamp = TimeOfTransacation.Timestamp;
            CreditAllocations.Add(creditAllocation);
        }

        public void MakeOpeningBalance(OpeningBalanceAllocation balanceAllocation, OpeningBalance openingBalance)
        {
            if (balanceAllocation.Amount > Balance) throw new FusebillException("Allocation cannot exceed the balance");

            balanceAllocation.EffectiveTimestamp = TimeOfTransacation.Timestamp;
            OpeningBalanceAllocations.Add(balanceAllocation);
        }

        public void MakeRefund(decimal amount, Refund refund)
        {
            if (null == refund) throw new FusebillException("No refund to process");
            if (null == refund.OriginalPayment) throw new FusebillException("No original payment is associated with the refund to process");
            if (amount <= 0) throw new FusebillException("Payment amounts must be greater than 0");

            if (amount > refund.OriginalPayment.Amount) throw new FusebillException("The refund amount cannot exceed the original payment amount.");

            var refundNote = new RefundNote { Amount = amount, Refund = refund, EffectiveTimestamp = TimeOfTransacation.Timestamp };

            refund.AddRefundNote(refundNote);

            RefundNotes.Add(refundNote);
        }

        public WriteOff WriteOff(decimal writeoffAmount)
        {
            if (Balance == 0) throw new FusebillException("You cannot write off an invoice that has a balance of zero.");

            if (Customer == null) throw new FusebillException("You must include the Customer on the invoice's before calling WriteOff");

            var writeOff = new WriteOff
            {
                Amount = writeoffAmount,
                EffectiveTimestamp = TimeOfTransacation.Timestamp,
                Customer = Customer,
                TransactionType = TransactionType.WriteOff,
                CurrencyId = Charges.First().CurrencyId
            };

            WriteOffs.Add(writeOff);

            return writeOff;
        }

        public Dispute Dispute()
        {
            var dispute = new Dispute { InvoiceId = Id };
            
            Disputes.Add(dispute);
            return dispute;
        }

        public void AddDefaultPaymentSchedule()
        {
            if (null == TimeOfTransacation)
                throw new FusebillException("No time of transaction was specified");

            PaymentSchedules.Add(
                new PaymentSchedule
                {
                    Amount = Balance,
                    DaysDueAfterTerm = 0,
                    IsDefault = true
                }
            );
        }

        private int GetOffsetDays()
        {
            if (null == Customer || null == Customer.CustomerBillingSetting)
                throw new FusebillException("No billing term was supplied");

            switch (Customer.CustomerBillingSetting.Term)
            {
                case Term.Net5:
                    return 5;

                case Term.Net10:
                    return 10;

                case Term.Net15:
                    return 15;

                case Term.Net30:
                    return 30;

                case Term.Net45:
                    return 45;

                case Term.Net60:
                    return 60;

                case Term.Net90:
                    return 90;

                default:
                    return 0;
            }
        }

        public void UpdateInvoiceNumber(int invoiceNumber)
        {
            InvoiceNumber = invoiceNumber;
        }

        [JsonIgnore]
        public ITimeOfTransaction TimeOfTransacation { get; set; }

        public InvoiceJournal LatestJournal
        {
            get { return InvoiceJournals.OrderByDescending(i => i.Id).FirstOrDefault(); }
        }

        public PaymentSchedule LatestPaymentSchedule
        {
            get { return PaymentSchedules.FirstOrDefault(); }
        }

        public DateTime DueDate
        {
            get
            {
                var dueDate = EffectiveTimestamp.AddDays(GetOffsetDays());

                return dueDate.AddMilliseconds(-dueDate.Millisecond);
            }
        }

        public decimal SumOfCharges
        {
            get { return Charges.Sum(c => c.Amount); }
        }

        public decimal SumOfDiscounts
        {
            get { return Charges.Sum(charge => charge.Discounts.Sum(discount => discount.Amount)); }
        }

        public decimal SumOfCreditNotes
        {
            get { return CreditNotes.Sum(c => c.Amount) + CreditAllocations.Sum(c => c.Amount) - DebitAllocations.Sum(d => d.Amount) + OpeningBalanceAllocations.Sum(o => o.Amount); }
        }

        public decimal SumOfPayments
        {
            get { return PaymentNotes.Sum(p => p.Amount); }
        }

        public decimal SumOfRefunds
        {
            get { return RefundNotes.Sum(p => p.Amount); }
        }

        public decimal SumOfWriteoffs
        {
            get { return WriteOffs.Sum(w => w.Amount); }
        }

        public decimal InvoiceAmount
        {
            get { return SumOfCharges - SumOfDiscounts + SumOfTaxes; }
        }

        public decimal TotalPayments
        {
            get { return (SumOfPayments - SumOfRefunds); }
        }

        public decimal SumOfTaxes
        {
            get { return Taxes.Sum(t => t.Amount); }
        }

        public decimal Balance
        {
            get { return SumOfCharges - SumOfDiscounts + SumOfTaxes - SumOfCreditNotes - (SumOfPayments - SumOfRefunds) - SumOfWriteoffs; }
        }

        public InvoiceJournal MostRecentJournal
        {
            get { return InvoiceJournals.OrderByDescending(jr => jr.Id).FirstOrDefault(); }
        }

        public InvoiceAddress BillingAddress
        {
            get { return InvoiceAddresses.FirstOrDefault(a => a.AddressType == AddressType.Billing); }
        }

        public InvoiceAddress ShippingAddress
        {
            get { return InvoiceAddresses.FirstOrDefault(a => a.AddressType == AddressType.Shipping); }
        }

        public string NetsuiteId
        {
            get { return null; }
        }
    }
}

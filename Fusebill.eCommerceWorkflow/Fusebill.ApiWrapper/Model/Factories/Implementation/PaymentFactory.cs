using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Factories.Implementation
{
    public class PaymentFactory : IPaymentFactory
    {
        public Payment Create(Invoice invoice, DateTime effectiveTimestamp, PaymentActivityJournal paymentActivityJournal, bool autoAllocate = true)
        {
            var allocation = new List<PaymentAllocation>();

            if (autoAllocate)
            {
                allocation.Add(new PaymentAllocation
                    {
                        Amount = Math.Min(paymentActivityJournal.Amount, invoice.Balance),
                        Invoice = invoice
                    });
            }

            return Create(invoice.InvoiceCustomer.CurrencyId, invoice.Customer, effectiveTimestamp, paymentActivityJournal, allocation);
        }

        public Payment Create(List<Invoice> invoices, DateTime effectiveTimestamp, PaymentActivityJournal paymentActivityJournal, DateTime accountUtcTime, Customer customer)
        {
            var currencyId = customer.CurrencyId;
            var timestamp = effectiveTimestamp;

            var allocations = (from invoice in invoices
                let totalOverdueForThisInvoice = CalculateSumDue(invoice, accountUtcTime)
                select new PaymentAllocation
                {
                    Amount = totalOverdueForThisInvoice,
                    Invoice = invoice
                }).ToList();

            return Create(currencyId, customer, timestamp, paymentActivityJournal, allocations);
        }

        public Payment CreateAutomaticAllocations(List<Invoice> invoices, DateTime effectiveTimestamp,
            PaymentActivityJournal paymentActivityJournal, Customer customer)
        {
            var currencyId = customer.CurrencyId;
            var timestamp = effectiveTimestamp;

            var paymentAllocations = new List<PaymentAllocation>();

            var remainingAmountToAllocate = paymentActivityJournal.Amount;

            foreach (var invoice in invoices.Where(i => i.Balance > 0).OrderBy(i => i.EffectiveTimestamp))
            {
                var paymentAllocation = new PaymentAllocation
                {
                    Amount = Math.Min(invoice.Balance, remainingAmountToAllocate),
                    Invoice = invoice,
                    InvoiceId = invoice.Id
                };

                paymentAllocations.Add(paymentAllocation);

                remainingAmountToAllocate -= paymentAllocation.Amount;

                if (remainingAmountToAllocate <= 0)
                    break;
            }

            return Create(currencyId, customer, timestamp, paymentActivityJournal, paymentAllocations);
        }

        private Payment Create(long currencyId, Customer customer, DateTime effectiveTimeStamp, PaymentActivityJournal paymentActivityJournal, List<PaymentAllocation> allocations)
        {
            return new Payment
            {
                Amount = paymentActivityJournal.Amount,
                RefundableAmount = paymentActivityJournal.Amount,
                UnallocatedAmount = paymentActivityJournal.Amount,
                CurrencyId = currencyId,
                Customer = customer,
                EffectiveTimestamp = effectiveTimeStamp,
                PaymentActivityJournal = paymentActivityJournal,
                PaymentAllocations = allocations,
                TransactionType = TransactionType.Payment
            };
        }

        private decimal CalculateSumDue(Invoice invoice, DateTime accountUtcTime)
        {
            var overduePaymentSchedules = new List<PaymentSchedule>();
            overduePaymentSchedules.AddRange(invoice.PaymentSchedules.Where(paymentSchedule => paymentSchedule.Status == InvoiceStatus.Overdue));

            return
                overduePaymentSchedules.Where(
                    paymentSchedule =>
                        paymentSchedule.Status == InvoiceStatus.Overdue && paymentSchedule.DueDate < accountUtcTime)
                    .Sum(paymentSchedule => paymentSchedule.OutstandingBalance);
        }
    }
}

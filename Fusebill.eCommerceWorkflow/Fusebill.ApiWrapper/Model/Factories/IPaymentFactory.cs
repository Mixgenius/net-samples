using System;
using System.Collections.Generic;

namespace Model.Factories
{
    public interface IPaymentFactory
    {
        Payment Create(Invoice invoice, DateTime effectiveTimestamp, PaymentActivityJournal paymentActivityJournal, bool autoAllocate = true);
        Payment Create(List<Invoice> invoices, DateTime effectiveTimestamp, PaymentActivityJournal paymentActivityJournal, DateTime accountUtcTime, Customer customer);
        Payment CreateAutomaticAllocations(List<Invoice> invoices, DateTime effectiveTimestamp, PaymentActivityJournal paymentActivityJournal, Customer customer);
    }
}

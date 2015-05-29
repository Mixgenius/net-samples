using System;

namespace Model.Factories
{
    public interface IInvoiceFactory
    {
        Invoice Create(BillingPeriod billingPeriod, long draftInvoiceId, DateTime effectiveTimestamp, DateTime postedTimestamp, Customer customer, Account account, string poNumber, string invoiceNotes);
    }
}

using System.Collections.Generic;

namespace Model.FactoryMethods
{
    public class PaymentFactoryMethods
    {
        public static PaymentAllocation CreatePaymentAllocation(decimal amount, List<PaymentAllocation> allocations, Invoice invoice, Payment originalPayment)
        {
            var allocation = new PaymentAllocation
            {
                Amount = amount,
                InvoiceId = invoice.Id,
                Invoice = invoice
            };

            allocations.Add(allocation);

            return allocation;
        }
    }
}

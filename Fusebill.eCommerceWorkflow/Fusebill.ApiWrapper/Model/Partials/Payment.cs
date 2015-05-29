using System.Collections.Generic;

namespace Model
{
    public partial class Payment
    {
        public List<PaymentAllocation> PaymentAllocations { get; set; }

        public void AddRefund(Refund refund)
        {
            Refunds.Add(refund);
        }
    }
}

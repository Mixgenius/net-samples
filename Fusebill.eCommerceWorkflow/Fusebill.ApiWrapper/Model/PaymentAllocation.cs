namespace Model
{
    public class PaymentAllocation
    {
        public long InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public decimal Amount { get; set; }

        public bool IsAllocated { get; set; }
    }
}

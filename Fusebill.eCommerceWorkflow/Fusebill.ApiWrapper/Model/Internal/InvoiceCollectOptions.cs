namespace Model.Internal
{
    /// <summary>
    /// Specifying these options will override billing rules such as: 
    /// - grouping on same invoice with existing draft items
    /// - auto post off
    /// - auto collect on Net0
    /// </summary>
    public class InvoiceCollectOptions
    {
        public PaymentMethodOptions PaymentMethodOptions { get; set; }

        public long? PaymentMethodId { get; set; }

        public PaymentMethodValidation.Request.PaymentMethod PaymentMethod { get; set; }

        public Address Address { get; set; }

        public bool UseAnyAvailableFundsFirst { get; set; }

        public bool RollbackOnFailedPayment { get; set; }

        public bool TemporarilyDisableAutoPost { get; set; }
    }
}
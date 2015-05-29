namespace Model.Internal
{
    public class SaveOptions
    {
        public bool SuppressCommunicationPlatformEvents { get; set; }
        public bool SuppressAllEmails { get; set; }
        
        public bool SuppressPaymentEmails { get; set; }
        public bool SuppressNewInvoiceEmails { get; set; }
        public bool SuppressSubscriptionActivationEmails { get; set; }
        public bool SuppressCustomerSuspensionEmails { get; set; }
    }
}

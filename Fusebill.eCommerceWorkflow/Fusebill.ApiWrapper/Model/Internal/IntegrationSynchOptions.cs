namespace Model.Internal
{
    public class IntegrationSynchOptions
    {
        public IntegrationType IntegrationType { get; set; }

        public bool ExportCustomers { get; set; }

        public bool ExportSubscriptions { get; set; }

        public bool ExportSubscriptionProducts { get; set; }

        public bool ExportInvoices { get; set; }

        public bool DeleteSubscriptions { get; set; }
    }
}

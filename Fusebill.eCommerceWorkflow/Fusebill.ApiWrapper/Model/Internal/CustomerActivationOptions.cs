namespace Model.Internal
{
    public class CustomerActivationOptions
    {
        public CustomerActivationOptions(long customerId, bool activateAllSubscriptions, bool activateAllDraftPurchases,
            bool temporarilyDisableAutoPost, bool showZeroDollarCharges, bool preview, bool loadFullSubscription)
        {
            CustomerId = customerId;
            ActivateAllSubscriptions = activateAllSubscriptions;
            ActivateAllDraftPurchases = activateAllDraftPurchases;
            TemporarilyDisableAutoPost = temporarilyDisableAutoPost;
            ShowZeroDollarCharges = showZeroDollarCharges;
            Preview = preview;
            LoadFullSubscription = loadFullSubscription;
        }

        public long CustomerId { get; private set; }
        public bool ActivateAllSubscriptions { get; private set; }
        public bool ActivateAllDraftPurchases { get; private set; }
        public bool TemporarilyDisableAutoPost { get; private set; }
        public bool ShowZeroDollarCharges { get; private set; }
        public bool Preview { get; private set; }
        public bool LoadFullSubscription { get; private set; }
    }
}

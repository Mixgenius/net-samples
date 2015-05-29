using System;

namespace Model
{
    public enum CustomerActivity : int
    {
        ActivateCustomer = 1,
        ActivateSubscription = 2,
        ProvisionSubscription = 3,
        ActivateScheduledSubscription = 4,
        ChangeQuantity = 5,
        NewCharges = 6,
        AutoCollect = 7,
        AutoPost = 8,
        CollectionRetries = 9,
        Billing = 10,
        Earning = 11,
        InvoiceAging = 12,
        CreateSubscription = 13,
        CreatePurchase = 14,
        BuyPurchase = 15
    }
}

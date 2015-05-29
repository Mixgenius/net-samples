namespace Model
{
    public partial class Charge
    {
        public bool IsTaxExempt()
        {
            if (SubscriptionProductCharge != null)
                return SubscriptionProductCharge.SubscriptionProduct.PlanProduct.Product.TaxExempt;

            if (TransactionType == TransactionType.OpeningBalanceOwing)
                return true;

            if (PurchaseCharge != null)
                return PurchaseCharge.Purchase.Product.TaxExempt;

            return false;
        }
    }
}

namespace Model
{
    public partial class DraftCharge
    {
        public bool IsTaxExempt()
        {
            if (IsDraftSubscriptionProductCharge())
                return DraftSubscriptionProductCharge.SubscriptionProduct.PlanProduct.Product.TaxExempt;

            if (TransactionType == TransactionType.OpeningBalanceOwing)
                return true;

            if (IsDraftPurchaseCharge())
                return Purchase.Product.TaxExempt;

            return false;
        }

        public bool IsDraftSubscriptionProductCharge()
        {
            return DraftSubscriptionProductCharge != null;
        }

        public bool IsDraftPurchaseCharge()
        {
            return Purchase != null;
        }

        public bool IsEarnedImmediately()
        {
            if (IsDraftSubscriptionProductCharge())
                return DraftSubscriptionProductCharge.SubscriptionProduct.PlanOrderToCashCycle.IsEarnedImmediately;

            if (IsDraftPurchaseCharge())
                return Purchase.IsEarnedImmediately;

            return true;
        }
    }
}

namespace Model.FactoryMethods
{
    public class DiscountFactoryMethods
    {
        public static Discount CreateDiscount(Charge charge, DraftCharge draftCharge, DraftDiscount draftDiscount, decimal amount, decimal unEarnedAmount, TransactionType transactionType)
        {
            var discount = new Discount
            {
                Amount = amount,
                CurrencyId = draftCharge.CurrencyId,
                Customer = draftCharge.DraftInvoice.Customer,
                ConfiguredDiscountAmount = draftDiscount.ConfiguredDiscountAmount,
                DiscountType = draftDiscount.DiscountType,
                TransactionType = transactionType,
                ChargeId = charge.Id,
                EffectiveTimestamp = charge.EffectiveTimestamp,
                Description = draftDiscount.Description,
                RemainingReversalAmount = amount,
                UnearnedAmount = unEarnedAmount,
                Quantity = draftDiscount.Quantity
            };

            charge.Discounts.Add(discount);

            discount.Customer.CreateLedgerEntriesFromTransaction(discount);

            return discount;
        }
    }
}

using System;

namespace Model.FactoryMethods
{
    public class DraftDiscountFactoryMethods
    {
        public static DraftDiscount CreateDraftDiscount(decimal amount, decimal configuredDiscountAmount, DiscountType discountType, string description, DraftCharge draftCharge)
        {
            var draftDiscount = new DraftDiscount
            {
                Amount = amount,
                ConfiguredDiscountAmount = configuredDiscountAmount,
                DiscountType = discountType,
                TransactionType = TransactionType.Discount,
                CurrencyId = draftCharge.CurrencyId,
                Description = description,
                Quantity = discountType == DiscountType.AmountPerUnit ? draftCharge.Quantity : 1
            };

            draftCharge.DraftDiscounts.Add(draftDiscount);
            draftCharge.TaxableAmount = Math.Max(draftCharge.TaxableAmount - draftDiscount.Amount, 0);

            return draftDiscount;
        }
    }
}

using System;
namespace Model.FactoryMethods
{
    public class ReverseDiscountFactoryMethod
    {
        public static void Create(decimal amount, TransactionType transactionType, CreditNote creditNote, string description, Discount discount, string reference, DateTime effectiveDate, ReverseCharge reverseCharge)
        {
            var reverseDiscount = new ReverseDiscount
            {
                Amount = amount,
                CreditNote = creditNote,
                Description = description,
                Discount = discount,
                Reference = reference,
                TransactionType = transactionType,
                EffectiveTimestamp = effectiveDate,
                CurrencyId = discount.CurrencyId,
                CustomerId = discount.CustomerId,
                ReverseCharge = reverseCharge
            };

            creditNote.ReverseDiscounts.Add(reverseDiscount);
            discount.ReverseDiscounts.Add(reverseDiscount);

            discount.RemainingReversalAmount = Math.Max(discount.RemainingReversalAmount - reverseDiscount.Amount, 0);

            discount.UnearnedAmount = Math.Max(discount.UnearnedAmount - reverseDiscount.Amount, 0);

            creditNote.Amount = Math.Max(creditNote.Amount - reverseDiscount.Amount, 0);

            discount.Customer.CreateLedgerEntriesFromTransaction(reverseDiscount);
        }
    }
}

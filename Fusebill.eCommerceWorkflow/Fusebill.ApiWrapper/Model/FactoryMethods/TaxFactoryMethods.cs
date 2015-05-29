using System;
using Model.Avalara;

namespace Model.FactoryMethods
{
    public class TaxFactoryMethods
    {
        public static DraftTax CreateDraftTax(DraftChargeStatus draftChargeStatus, decimal taxableAmount, decimal taxPercentage, TaxRule taxRule,
            long currencyId)
        {
            var draftTax = new DraftTax
            {
                Amount =
                    draftChargeStatus == DraftChargeStatus.Active ? decimal.Round(taxableAmount*taxPercentage, 2) : 0,
                TaxRule = taxRule,
                TaxRuleId = taxRule.Id,
                CurrencyId = currencyId
            };

            return draftTax;
        }

        public static Tax CreateTax(Invoice invoice, Charge charge, decimal taxPercentage, TaxRule taxRule)
        {
            return CreateTax(decimal.Round(charge.TaxableAmount()*taxPercentage, 2), taxRule, charge.CurrencyId,
                charge.Customer, charge.EffectiveTimestamp, charge.Invoice);
        }

        public static Tax CreateTax(TaxDetail taxDetail, TaxRule existingTaxRule, Charge charge)
        {
            return CreateTax(taxDetail.Tax, existingTaxRule, charge.CurrencyId, charge.Customer,
                charge.EffectiveTimestamp, charge.Invoice);
        }

        private static Tax CreateTax(decimal amount, TaxRule taxRule, long currencyId, Customer customer,
            DateTime effectiveDate, Invoice invoice)
        {
            var tax = new Tax
            {
                Amount = amount,
                TaxRule = taxRule,
                CurrencyId = currencyId,
                Customer = customer,
                TransactionType = TransactionType.Tax,
                RemainingReversalAmount = amount,
                EffectiveTimestamp = effectiveDate,
                Invoice = invoice
            };

            customer.CreateLedgerEntriesFromTransaction(tax);
            invoice.Taxes.Add(tax);

            return tax;
        }

        public static void CreateReverseTax(Tax originalTax, ReverseCharge reverseCharge, CreditNote creditNote, decimal reverseTaxAmount, DateTime effectiveDate)
        {
            var reverseTax = new ReverseTax
            {
                Amount = reverseTaxAmount,
                TransactionType = TransactionType.ReverseTax,
                EffectiveTimestamp = effectiveDate,
                Customer = originalTax.Customer,
                CreditNote = creditNote,
                CurrencyId = reverseCharge.CurrencyId,
                ReverseCharge = reverseCharge
            };

            originalTax.RemainingReversalAmount = Math.Max(originalTax.RemainingReversalAmount - reverseTaxAmount, 0);

            originalTax.ReverseTaxes.Add(reverseTax);

            creditNote.ReverseTaxes.Add(reverseTax);
            creditNote.Amount += reverseTaxAmount;

            originalTax.Customer.CreateLedgerEntriesFromTransaction(reverseTax);
        }
    }
}
using System;
using System.Collections.Generic;
namespace Model.Internal
{
    public class CustomerLedgerSummary
    {
        public long CustomerId { get; set; }
        public decimal ArDebit { get; set; }
        public decimal ArCredit { get; set; }
        public decimal CashDebit { get; set; }
        public decimal CashCredit { get; set; }
        public decimal EarnedRevenueDebit { get; set; }
        public decimal EarnedRevenueCredit { get; set; }
        public decimal WriteOffDebit { get; set; }
        public decimal WriteOffCredit { get; set; }
        public decimal DeferredRevenueDebit { get; set; }
        public decimal DeferredRevenueCredit { get; set; }
        public decimal TaxesPayableDebit { get; set; }
        public decimal TaxesPayableCredit { get; set; }
        public decimal DiscountDebit { get; set; }
        public decimal DiscountCredit { get; set; }
        public decimal DeferredDiscountDebit { get; set; }
        public decimal DeferredDiscountCredit { get; set; }
        public decimal OpeningDebit { get; set; }
        public decimal OpeningCredit { get; set; }
        public decimal CreditDebit { get; set; }
        public decimal CreditCredit { get; set; }
    }
}

using System.Linq;

namespace Model.Internal
{
    public class ReverseChargeResults
    {
        public ReverseChargeResults(ReverseCharge unearnedReverseCharge, ReverseCharge earnedReverseCharge, decimal reversedAmountSum, CreditNote creditNote)
        {
            UnearnedReverseCharge = unearnedReverseCharge;
            EarnedReverseCharge = earnedReverseCharge;
            ReversedAmountSum = reversedAmountSum;
            CreditNote = creditNote;
        }

        public ReverseCharge UnearnedReverseCharge { get; private set; }
        public ReverseCharge EarnedReverseCharge { get; private set; }
        public decimal ReversedAmountSum { get; private set; }

        public decimal TaxableReversedAmountSum
        {
            get
            {
                return ReversedAmountSum - CreditNote.ReverseDiscounts.Sum(rd => rd.Amount);
            }
        }

        public CreditNote CreditNote { get; private set; }
    }
}

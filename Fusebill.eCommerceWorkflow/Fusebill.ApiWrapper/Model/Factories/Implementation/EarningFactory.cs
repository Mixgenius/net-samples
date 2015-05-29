using DataCommon.Models;

namespace Model.Factories.Implementation
{
    public class EarningFactory : IEarningFactory
    {
        private readonly ITimeOfTransaction _timeOfTransaction;

        public EarningFactory(ITimeOfTransaction timeOfTransaction)
        {
            _timeOfTransaction = timeOfTransaction;
        }

        public Earning CreateEarningForFullUnearned(Charge charge)
        {
            var earning = new Earning
            {
                Amount = charge.UnearnedAmount,
                EffectiveTimestamp = _timeOfTransaction.Timestamp,
                TransactionType = TransactionType.Earning,
                Customer = charge.Customer,
                CurrencyId = charge.CurrencyId
            };

            SetChargeLastEarning(charge, earning);

            return earning;
        }

        private static void SetChargeLastEarning(Charge charge, Earning earning)
        {
            if (charge.ChargeLastEarning == null) charge.ChargeLastEarning = new ChargeLastEarning { Id = charge.Id };

            charge.ChargeLastEarning.Earning = earning;
        }

        public EarningDiscount CreateEarningForFullUnearned(Discount discount)
        {
            return new EarningDiscount
            {
                Amount = discount.UnearnedAmount,
                EffectiveTimestamp = _timeOfTransaction.Timestamp,
                TransactionType = TransactionType.EarningDiscount,
                Customer = discount.Customer,
                CurrencyId = discount.CurrencyId
            };
        }
    }
}

namespace Model.Factories
{
    public interface IEarningFactory
    {
        Earning CreateEarningForFullUnearned(Charge charge);

        EarningDiscount CreateEarningForFullUnearned(Discount discount);
    }
}
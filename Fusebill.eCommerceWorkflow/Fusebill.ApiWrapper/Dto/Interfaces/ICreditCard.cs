namespace Fusebill.ApiWrapper.Dto.Interfaces
{
    public interface ICreditCard : IPaymentMethod
    {
        string CardNumber { get; set; }
        int ExpirationMonth { get; set; }
        int ExpirationYear { get; set; }
        string Cvv { get; set; }
    }
}

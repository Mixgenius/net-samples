using Model.CreditCardValidation.Request;

namespace Model.PaymentMethodValidation.Request
{
    public class TokenCollectRequest
    {
        public BaseRequest BaseRequest { get; set; }
        public string Token  { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public long CustomerId { get; set; }
    }
}

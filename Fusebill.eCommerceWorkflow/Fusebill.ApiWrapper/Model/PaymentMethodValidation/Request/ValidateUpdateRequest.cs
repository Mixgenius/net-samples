using Model.CreditCardValidation.Request;

namespace Model.PaymentMethodValidation.Request
{
    public class ValidateUpdateRequest
    {
        public BaseRequest BaseRequest { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Token { get; set; }
        public PaymentSource Source { get; set; }
    }
}

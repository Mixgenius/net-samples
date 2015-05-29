using Model.CreditCardValidation.Request;

namespace Model.PaymentMethodValidation.Request
{
    public class ValidationRequest
    {
        public BaseRequest BaseRequest { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentSource Source { get; set; }
        public string ExternalCustomerId { get; set; }
    }
}

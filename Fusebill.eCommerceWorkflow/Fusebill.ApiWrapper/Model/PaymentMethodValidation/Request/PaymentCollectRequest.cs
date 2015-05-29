using Model.CreditCardValidation.Request;

namespace Model.PaymentMethodValidation.Request
{
    public class PaymentCollectRequest
    {
        public BaseRequest BaseRequest { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ExternalCustomerId { get; set; }
        public long CustomerId { get; set; }
    }
}

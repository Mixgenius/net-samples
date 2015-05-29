using Model.CreditCardValidation.Request;

namespace Model.PaymentMethodValidation.Request
{
    public class RefundRequest
    {
        public BaseRequest BaseRequest { get; set; }
        public string SuccessfulGatewayTransactionId { get; set; }
        public decimal Amount { get; set; }
        public bool Test { get; set; }
        public long CustomerId { get; set; }
    }
}

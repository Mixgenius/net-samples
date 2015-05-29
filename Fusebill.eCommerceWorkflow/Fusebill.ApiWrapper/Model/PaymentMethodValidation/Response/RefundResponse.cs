using System;

namespace Model.PaymentMethodValidation.Response
{
    public class RefundResponse : BaseResponse
    {
        public Guid? PaymentSuccessfulGatewayTransactionId { get; set; }
        public string PaymentTransactionReferenceNumber { get; set; }
        public TokenCollectResponse PaymentCollectionResponse { get; set; }
        public decimal? PaymentAmount { get; set; }
    }
}

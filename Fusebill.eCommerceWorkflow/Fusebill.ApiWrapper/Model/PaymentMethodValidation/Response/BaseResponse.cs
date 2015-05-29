using System;

namespace Model.PaymentMethodValidation.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string ReasonCode { get; set; }
        public string TransactionReferenceNumber { get; set; }
        public string SecondaryTransactionReferenceNumber { get; set; }
        public string GatewayResponseDescription { get; set; }
        public bool Test { get; set; }
        public string SuccessfulGatewayTransactionId { get; set; }
        public long AccountConfigurationId { get; set; }
        public string AccountConfigurationName { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Created { get; set; }
        public string Token { get; set; }
        public long? PaymentMethodId { get; set; }
        public long? CustomerId { get; set; }
        public ExternalVaultResponse ExternalVault { get; set; }
    }
}

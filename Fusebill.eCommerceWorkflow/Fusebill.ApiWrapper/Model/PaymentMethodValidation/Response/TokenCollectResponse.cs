namespace Model.PaymentMethodValidation.Response
{
    public class TokenCollectResponse : BaseResponse
    {
        public string ReplacementToken { get; set; }
        public ExpirationDate ExpirationDate { get; set; }
    }
}

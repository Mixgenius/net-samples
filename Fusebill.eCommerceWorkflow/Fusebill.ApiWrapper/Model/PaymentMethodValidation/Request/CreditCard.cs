namespace Model.PaymentMethodValidation.Request
{
    public class CreditCard
    {
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CVN { get; set; }
        public ExpirationDate ExpirationDate { get; set; }
        public string CardShortCode { get; set; }
        public Address Address { get; set; }
    }
}

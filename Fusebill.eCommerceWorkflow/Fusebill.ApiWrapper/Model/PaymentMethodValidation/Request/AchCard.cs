namespace Model.PaymentMethodValidation.Request
{
    public class AchCard
    {
        public string AccountNumber { get; set; }
        public string TransitNumber { get; set; }
        public string AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}

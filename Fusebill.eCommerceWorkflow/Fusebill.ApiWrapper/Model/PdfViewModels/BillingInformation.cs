namespace Model.PdfViewModels
{
    public class BillingInformation
    {
        public string CustomerReference { get; set; }

        public string CustomerName { get; set; }

        public AddressInformation Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
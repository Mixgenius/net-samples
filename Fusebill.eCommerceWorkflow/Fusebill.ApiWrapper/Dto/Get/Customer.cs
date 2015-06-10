using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Customer : BaseDto
    {


        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(50)]
        [JsonProperty(PropertyName = "firstName")]
        [DisplayName("First name")]

        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName")]
        [DisplayName("Middle name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Tell us your last name!")]
        [StringLength(50)]
        [JsonProperty(PropertyName = "lastName")]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        [DisplayName("Company name")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "suffix")]
        [DisplayName("Suffix")]
        public string Suffix { get; set; }



        [Required(ErrorMessage = "What's your email address?")]
        [StringLength(255)]
        [RegularExpression(
    @"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$",
    ErrorMessage = "Invalid Email Address")]
        public string PrimaryEmail { get; set; }

        [Required(ErrorMessage = "Let us know your phone number")]
        [StringLength(50)]
        [JsonProperty(PropertyName = "primaryPhone")]
        [DisplayName("Primary phone number")]
        public string PrimaryPhone { get; set; }

        [JsonProperty(PropertyName = "secondaryEmail")]
        [DisplayName("Secondary email address")]
        public string SecondaryEmail { get; set; }

        [JsonProperty(PropertyName = "secondaryPhone")]
        [DisplayName("Secondary phone number")]
        public string SecondaryPhone { get; set; }

        [JsonProperty(PropertyName = "title")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [DisplayName("Customer ID")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "status")]
        [DisplayName("Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "customerAccountStatus")]
        [DisplayName("Customer Account Status")]
        public string CustomerAccountStatus { get; set; }

        [JsonProperty(PropertyName = "currency")]
        [DisplayName("Currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "customerReference")]
        public CustomerReference CustomerReference { get; set; }

        [JsonProperty(PropertyName = "customerAcquisition")]
        public CustomerAcquisition CustomerAcquisition { get; set; }

        [JsonProperty(PropertyName = "invoicePreview")]
        public InvoicePreview InvoicePreview { get; set; }

        public bool ShouldSerializeInvoicePreview() { return InvoicePreview != null; }

        [JsonProperty(PropertyName = "additionalInvoicesForPreview")]
        public List<InvoicePreview> AdditionalInvoicesForPreview { get; set; }

        public bool ShouldSerializeAdditionalInvoicesForPreview() { return AdditionalInvoicesForPreview != null; }

        [JsonProperty(PropertyName = "monthlyRecurringRevenue")]
        public decimal MonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "netMonthlyRecurringRevenue")]
        [DisplayName("Net MRR")]
        public decimal NetMonthlyRecurringRevenue { get; set; }

        [JsonProperty(PropertyName = "salesforceId")]
        [DisplayName("Salesforce Id")]
        public string SalesforceId { get; set; }

        [JsonProperty(PropertyName = "netsuiteId")]
        [DisplayName("Netsuite Id")]
        public string NetsuiteId { get; set; }
    }
}

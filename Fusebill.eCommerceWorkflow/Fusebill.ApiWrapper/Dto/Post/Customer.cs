using Fusebill.ApiWrapper.Dto.Get;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class Customer : BaseDto
    {
        [JsonProperty(PropertyName = "firstName")]
        [DisplayName("First name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName")]
        [DisplayName("Middle name")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        [DisplayName("Last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "companyName")]
        [DisplayName("Company name")]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "suffix")]
        [DisplayName("Suffix")]
        [StringLength(50)]
        public string Suffix { get; set; }


        [JsonProperty(PropertyName = "primaryEmail")]
        [DisplayName("Primary email address (comma or semicolon separated)")]
     //   [EmailMultiple(ErrorMessage = "Please enter valid email addresses")]
        [StringLength(255)]
        public string PrimaryEmail { get; set; }

        [JsonProperty(PropertyName = "primaryPhone")]
        [DisplayName("Primary phone number")]
        [StringLength(50)]
        public string PrimaryPhone { get; set; }

        [JsonProperty(PropertyName = "secondaryEmail")]
        [DisplayName("Alternate email address (comma or semicolon separated)")]
     //   [EmailMultiple(ErrorMessage = "Please enter valid email addresses")]
        [StringLength(255)]
        public string SecondaryEmail { get; set; }

        [JsonProperty(PropertyName = "secondaryPhone")]
        [DisplayName("Alternate phone number")]
        [StringLength(50)]
        public string SecondaryPhone { get; set; }

        [JsonProperty(PropertyName = "title")]
        [DisplayName("Title")]
    //    [TitleValidation]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "reference")]
        [DisplayName("Customer ID")]
        [StringLength(255)]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "currency")]
        [DisplayName("Currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "customerReference")]
        public CustomerReference CustomerReference { get; set; }

        [JsonProperty(PropertyName = "customerAcquisition")]
        public CustomerAcquisition CustomerAcquisition { get; set; }
    }
}

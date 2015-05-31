using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Post
{
    public class PaymentAllocation : BaseDto
    {
        [JsonProperty(PropertyName = "invoiceId")]
        [DisplayName("InvoiceId")]
        public long InvoiceId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        [DisplayName("Amount")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Amount must be a positive number and cannot exceed two decimal places")]
        [RegularExpression(@"^\d*.?\d{0,2}?$", ErrorMessage = "Amount must be a positive number and cannot exceed two decimal places")]
        public decimal Amount { get; set; }
    }
}

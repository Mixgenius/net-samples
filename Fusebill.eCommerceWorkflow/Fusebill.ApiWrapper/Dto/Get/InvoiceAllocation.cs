using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class InvoiceAllocation
    {
        [JsonProperty(PropertyName = "invoiceId")]
        public long InvoiceId { get; set; }

        [JsonProperty(PropertyName = "invoiceNumber")]
        public long InvoiceNumber { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "outstandingBalance")]
        public decimal OutstandingBalance { get; set; }

        [JsonProperty(PropertyName = "invoiceTotal")]
        public decimal InvoiceTotal { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}
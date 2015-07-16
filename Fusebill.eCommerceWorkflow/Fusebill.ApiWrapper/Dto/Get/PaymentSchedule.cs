using System;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PaymentSchedule
    {
        [JsonProperty(PropertyName = "dueDateTimestamp")]
        public DateTime DueDateTimestamp { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "outstandingBalance")]
        public decimal OutstandingBalance { get; set; }

        [JsonProperty(PropertyName = "daysDueAfterTerm")]
        public int DaysDueAfterTerm { get; set; }
    }
}
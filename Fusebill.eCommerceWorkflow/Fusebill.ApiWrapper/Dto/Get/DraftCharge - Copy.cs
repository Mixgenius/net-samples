using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class DraftCharge : BaseDto
    {
        [JsonProperty(PropertyName = "transactionId")]
        [DisplayName("Transaction Id")]
        public long TransactionId { get; set; }

        [JsonProperty(PropertyName = "chargeTypeId")]
        [DisplayName("Charge Type ID")]
        public string ChargeTypeId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "unitPrice")]
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty(PropertyName = "amount")]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "taxableAmount")]
        [DisplayName("Taxable Amount")]
        public decimal TaxableAmount { get; set; }

        [JsonProperty(PropertyName = "subscriptionPeriodId")]
        [DisplayName("Subscription Period Id")]
        public long SubscriptionPeriodId { get; set; }

        [JsonProperty(PropertyName = "draftInvoiceId")]
        [DisplayName("Draft Invoice ID")]
        public long DraftInvoiceId { get; set; }

        [JsonProperty(PropertyName = "name")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "effectiveTimestamp")]
        public DateTime EffectiveTimestamp { get; set; }

        [JsonProperty(PropertyName = "proratedUnitPrice")]
        [DisplayName("Prorated Unit Price")]
        public decimal? ProratedUnitPrice { get; set; }

        [JsonProperty(PropertyName = "startServiceDate")]
        public DateTime StartServiceDate { get; set; }

        [JsonProperty(PropertyName = "endServiceDate")]
        public DateTime EndServiceDate { get; set; }

        [JsonProperty(PropertyName = "rangeQuantity")]
        public decimal? RangeQuantity { get; set; }

        [JsonProperty(PropertyName = "draftDiscount")]
        public DraftDiscount DraftDiscount { get; set; }

        [JsonProperty(PropertyName = "draftDiscounts")]
        public List<DraftDiscount> DraftDiscounts { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}

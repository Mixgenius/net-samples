using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class InvoicePreview
    {
        [JsonProperty(PropertyName = "draftCharges")]
        [DisplayName("Draft Charges")]
        public List<DraftCharge> DraftCharges { get; set; }

        [JsonProperty(PropertyName = "subtotal")]
        [DisplayName("Subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty(PropertyName = "total")]
        [DisplayName("Total")]
        public decimal Total { get; set; }

        [JsonProperty(PropertyName = "draftTaxes")]
        [DisplayName("Draft Taxes")]
        public List<Tax> DraftTaxes { get; set; }

        [JsonProperty(PropertyName = "tax")]
        [DisplayName("Tax")]
        public Tax Tax { get; set; }

        public bool ShouldSerializeTax()
        {
            return Tax != null;
        }

        [JsonProperty(PropertyName = "totalTaxes")]
        [DisplayName("Total Taxes")]
        public decimal TotalTaxes { get; set; }

        [JsonProperty(PropertyName = "poNumber")]
        [DisplayName("PO Number")]
        public string PoNumber { get; set; }

        [JsonProperty(PropertyName = "effectiveTimestamp")]
        [DisplayName("Effective Date")]
        public DateTime EffectiveTimestamp { get; set; }

        [JsonProperty(PropertyName = "status")]
        [DisplayName("Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "draftChargeGroups")]
        [DisplayName("Draft Charge Groups")]
        public List<DraftChargeGroup> DraftChargeGroups { get; set; }

        [JsonProperty(PropertyName = "notes")]
        [DisplayName("Notes")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "shippingInstructions")]
        [DisplayName("Shipping Instructions")]
        public string ShippingInstructions { get; set; }
    }
}

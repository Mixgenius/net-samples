using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class ActivatedSubscriptions : BaseDto
    {
        [JsonProperty(PropertyName = "subscriptions")]
        public List<Subscription> Subscriptions { get; set; }

        [JsonProperty(PropertyName = "invoicePreview")]
        public InvoicePreview InvoicePreview { get; set; }

        public bool ShouldSerializeInvoicePreview() { return InvoicePreview != null; }

        [JsonProperty(PropertyName = "additionalInvoicesForPreview")]
        public List<InvoicePreview> AdditionalInvoicesForPreview { get; set; }

        public bool ShouldSerializeAdditionalInvoicesForPreview() { return AdditionalInvoicesForPreview != null; }
    }
}

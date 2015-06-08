using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PlanProductList
    {
        [JsonProperty(PropertyName = "planProducts")]
        public List<PlanProduct> PlanProducts { get; set; }
        
        [JsonProperty(PropertyName = "changeSummary")]
        public PlanChangeSummary ChangeSummary { get; set; }

        [JsonProperty(PropertyName = "changePreview")]
        public PlanChangeSummary ChangePreview { get; set; }

        public bool ShouldSerializeChangeSummary()
        {
            return ChangePreview == null && ChangeSummary != null;
        }

        public bool ShouldSerializeChangePreview()
        {
            return ChangeSummary == null && ChangePreview != null;
        }
    }

    public class PlanProduct : BaseDto
    {

        public bool IsIncluded { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "productId")]
        public long ProductId { get; set; }

        [JsonProperty(PropertyName = "planId")]
        public long PlanId { get; set; }

        [JsonProperty(PropertyName = "productCode")]
        public string ProductCode { get; set; }

        [DisplayName("Product Name")]
        [JsonProperty(PropertyName = "productName")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "productStatus")]
        public string ProductStatus { get; set; }

        [DisplayName("Product Description")]
        [JsonProperty(PropertyName = "productDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty(PropertyName = "productType")]
        public string ProductType { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.######}", ApplyFormatInEditMode = true)]
        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.######}", ApplyFormatInEditMode = true)]
        [DisplayName("Max Quantity")]
        [JsonProperty(PropertyName = "maxQuantity")]
        public decimal? MaxQuantity { get; set; }

        [JsonProperty(PropertyName = "isRecurring")]
        public bool IsRecurring { get; set; }

        [DisplayName("Fixed Quantity")]
        [JsonProperty(PropertyName = "isFixed")]
        public bool IsFixed { get; set; }

        [JsonProperty(PropertyName = "isOptional")]
        public bool IsOptional { get; set; }

        [DisplayName("Selected by Default")]
        [JsonProperty(PropertyName = "isIncludedByDefault")]
        public bool IsIncludedByDefault { get; set; }

        [DisplayName("Track Unique Quantities")]
        [JsonProperty(PropertyName = "isTrackingItems")]
        public bool IsTrackingItems { get; set; }

        [JsonProperty(PropertyName = "chargeAtSubscriptionActivation")]
        public bool ChargeAtSubscriptionActivation { get; set; }

        [JsonProperty(PropertyName = "orderToCashCycles")]
        public List<PlanOrderToCashCycle> OrderToCashCycles { get; set; }

        [DisplayName("Automatic Quantity Reset")]
        [JsonProperty(PropertyName = "resetType")]
        public string ResetType { get; set; }

        [JsonProperty(PropertyName = "planProductUniqueId")]
        public long PlanProductUniqueId { get; set; }

        [JsonProperty(PropertyName = "changeSummary")]
        public PlanChangeSummary ChangeSummary { get; set; }

        [JsonProperty(PropertyName = "changePreview")]
        public PlanChangeSummary ChangePreview { get; set; }

        public bool ShouldSerializeChangeSummary()
        {
            return ChangePreview == null && ChangeSummary != null;
        }

        public bool ShouldSerializeChangePreview()
        {
            return ChangeSummary == null && ChangePreview != null;
        }
    }
}

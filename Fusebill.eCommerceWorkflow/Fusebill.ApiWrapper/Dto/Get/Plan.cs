using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class Plan : BaseDto
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "longdescription")]
        public string LongDescription { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "modificationTimestamp")]
        public DateTime ModificationTimestamp { get; set; }

        [JsonProperty(PropertyName = "planFrequencies")]
        public List<PlanFrequency> PlanFrequencies { get; set; }

        [DisplayName("Auto Apply Changes")]
        [JsonProperty(PropertyName = "autoApplyChanges")]
        public bool AutoApplyChanges { get; set; }

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

    public class PlanChangeSummary
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "allSubscriptions")]
        public SubscriptionChanges AllSubscriptions { get; set; }

        [JsonProperty(PropertyName = "existingSubscriptions")]
        public SubscriptionChanges ExistingSubscriptions { get; set; }

        [JsonProperty(PropertyName = "newSubscriptions")]
        public SubscriptionChanges NewSubscriptions { get; set; }
    }

    public class SubscriptionChanges
    {
        [JsonProperty(PropertyName = "numberAffected")]
        public int? NumberAffected { get; set; }

        public bool ShouldSerializeNumberAffected()
        {
            return NumberAffected.HasValue;
        }

        [JsonProperty(PropertyName = "numberWillBeAffected")]
        public int? NumberWillBeAffected { get; set; }

        public bool ShouldSerializeNumberWillBeAffected()
        {
            return NumberWillBeAffected.HasValue;
        }

        [JsonProperty(PropertyName = "changes")]
        public List<Change> Changes { get; set; }
    }

    public class Change
    {
        [JsonProperty(PropertyName = "property")]
        public string Property { get; set; }

        [JsonProperty(PropertyName = "oldValue")]
        public string OldValue { get; set; }

        [JsonProperty(PropertyName = "newValue")]
        public string NewValue { get; set; }
    }
}

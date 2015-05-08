using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class DraftChargeGroup
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "draftCharges")]
        [DisplayName("Draft Charges")]
        public List<DraftCharge> DraftCharges { get; set; }
    }
}

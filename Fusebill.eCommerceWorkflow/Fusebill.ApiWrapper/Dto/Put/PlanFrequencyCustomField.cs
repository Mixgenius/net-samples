using System.ComponentModel;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class PlanFrequencyCustomField : BaseDto
    {
        [DisplayName("Default Value")]
        [JsonProperty(PropertyName = "defaultValue")]
        public dynamic DefaultValue { get; set; }
    }
}
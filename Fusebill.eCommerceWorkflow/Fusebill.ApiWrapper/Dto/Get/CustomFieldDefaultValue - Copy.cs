using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public abstract class CustomFieldDefaultValue
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "friendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty(PropertyName = "dataType")]
        public string DataType { get; set; }

        [JsonProperty(PropertyName = "defaultValue")]
        public dynamic DefaultValue { get; set; }
    }
}

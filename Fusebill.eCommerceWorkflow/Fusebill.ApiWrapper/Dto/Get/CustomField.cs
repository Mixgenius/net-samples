using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class CustomField
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "friendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty(PropertyName = "dataType")]
        public string DataType { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}

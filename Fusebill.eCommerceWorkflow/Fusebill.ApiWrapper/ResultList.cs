using Fusebill.ApiWrapper.Dto.Get;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fusebill.ApiWrapper
{
    public class ResultList<T>
    {
        [JsonProperty(PropertyName = "headers")]
        public PagingHeaderData PagingHeaderData { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<T> Results { get; set; }
    }
}

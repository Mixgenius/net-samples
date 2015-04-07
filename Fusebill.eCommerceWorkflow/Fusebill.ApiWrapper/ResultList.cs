using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fusebill.ApiWrapper.Dto.Get;
using Newtonsoft.Json;

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

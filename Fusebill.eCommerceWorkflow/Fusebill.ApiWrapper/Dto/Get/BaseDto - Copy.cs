using System;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public interface IBaseDto
    {
        long Id { get; set; }
        string Uri { get; set; }
    }
    public class BaseDto : IBaseDto, IEquatable<BaseDto>
    {
        [JsonProperty(PropertyName = "id", Order = -1)]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        public bool Equals(BaseDto other)
        {
            if (null == other)
            {
                return false;
            }
            return (Id == other.Id && Uri == other.Uri);
        }
    }
}

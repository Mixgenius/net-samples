﻿using System;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public abstract class BaseDto : IBaseDto, IEquatable<BaseDto>
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        public bool Equals(BaseDto other)
        {
            if (null == other)
            {
                return false;
            }
            return (Id == other.Id);
        }
    }
}

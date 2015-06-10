using Fusebill.ApiWrapper.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fusebill.ApiWrapper
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public T DeserializeToEntity<T>(string entityToDeserialize)
        {
            if (string.IsNullOrEmpty(entityToDeserialize))
                return default(T);

            var entity = JsonConvert.DeserializeObject<T>(entityToDeserialize);
            return entity;
        }

        public List<T> DeserializeToList<T>(string entitiesToDeserialize)
        {
            if (string.IsNullOrEmpty(entitiesToDeserialize))
                return null;

            var entities = JsonConvert.DeserializeObject<List<T>>(entitiesToDeserialize);
            return entities;
        }
    }
}

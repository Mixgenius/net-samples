using System.Collections.Generic;

namespace Fusebill.ApiWrapper
{
    public interface IJsonDeserializer
    {
        T DeserializeToEntity<T>(string entityToDeserialize);
        List<T> DeserializeToList<T>(string entitiesToDeserialize);
    }
}

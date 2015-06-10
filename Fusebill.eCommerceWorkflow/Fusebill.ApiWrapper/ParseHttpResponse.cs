using Fusebill.ApiWrapper.Contracts;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Fusebill.ApiWrapper
{
    public class ParseHttpResponse : IParseHttpResponse
    {
        private readonly IJsonDeserializer _jsonDeserializer;

        public ParseHttpResponse(IJsonDeserializer jsonDeserializer)
        {
            _jsonDeserializer = jsonDeserializer;
        }

        public List<T> GetEntities<T>(string httpContent)
        {
            return _jsonDeserializer.DeserializeToList<T>(httpContent);
        }

        public T GetEntity<T>(string httpContent)
        {
            return _jsonDeserializer.DeserializeToEntity<T>(httpContent);
        }

        public Dto.Get.PagingHeaderData GetHeaderData(HttpResponseHeaders responseHeaders)
        {
            var extractDataFromHeaders = new ExtractDataFromHeaders(responseHeaders);
            return extractDataFromHeaders.ExtractPaginationDataFromHeader();
        }
    }
}

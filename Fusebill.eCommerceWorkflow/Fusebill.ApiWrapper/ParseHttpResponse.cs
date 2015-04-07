using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fusebill.ApiWrapper
{
    public class ParseHttpResponse : IParseHttpResponse
    {
        private IJsonDeserializer _jsonDeserializer;

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

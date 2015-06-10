using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Fusebill.ApiWrapper.Contracts
{
    public interface IParseHttpResponse
    {
        List<T> GetEntities<T>(string httpContent);
        T GetEntity<T>(string httpContent);
        Dto.Get.PagingHeaderData GetHeaderData(HttpResponseHeaders responseHeaders);
    }
}

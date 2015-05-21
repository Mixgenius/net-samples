using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Fusebill.ApiWrapper
{
    public interface IExecuteHttpRequest
    {
        HttpResponseMessage ExecuteHttpGet(string url, string acceptType = "application/json", int timeout = 60);
        HttpResponseMessage ExecuteHttpPost<T>(string url, T entity, string acceptType = "application/json");
        HttpResponseMessage ExecuteHttpPost<T>(string url, List<T> entities, string acceptType = "application/json");
        HttpResponseMessage ExecuteHttpPut<T>(string url, T entity, string acceptType = "application/json");
        //HttpResponseMessage ExecuteHttpPatch<T>(string url, T entity, string acceptType = "application/json");
        HttpResponseMessage ExecuteHttpDelete(string url);

        long LoggedInUserId { get; set; }
        string ApiKey { get; set; }
        DateTime? DateForTesting { get; set; }
        string SystemSource { get; set; }
    }
}

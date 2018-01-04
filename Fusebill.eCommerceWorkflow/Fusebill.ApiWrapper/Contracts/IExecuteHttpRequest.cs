using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fusebill.ApiWrapper.Contracts
{
    public interface IExecuteHttpRequest
    {
        Task<HttpResponseMessage> ExecuteHttpGet(string url, string acceptType = "application/json", int timeout = 60);
        //Task<HttpResponseMessage> ExecuteHttpPost<T>(string url, T entity, string acceptType = "application/json");
        //Task<HttpResponseMessage> ExecuteHttpPost<T>(string url, List<T> entities, string acceptType = "application/json");
        Task<HttpResponseMessage> ExecuteHttpPost<T>(string url, T entity, string acceptType = "application/json", int timeout = 60);
        Task<HttpResponseMessage> ExecuteHttpPut<T>(string url, T entity, string acceptType = "application/json");
        Task<HttpResponseMessage> ExecuteHttpDelete(string url);

        Task<HttpResponseMessage> ExecuteHttpPatch<T>(string url, T entities, string acceptType = "application/json");

        long LoggedInUserId { get; set; }
        string ApiKey { get; set; }
        DateTime? DateForTesting { get; set; }
        string SystemSource { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Fusebill.ApiWrapper.Contracts;
using Fusebill.ApiWrapper.Dto;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Fusebill.ApiWrapper
{
    public class ExecuteHttpRequest : IExecuteHttpRequest
    {
        private readonly ILogger<ExecuteHttpRequest> _log;
        private readonly string _auditSource;
        public long LoggedInUserId { get; set; }
        public string ApiKey { get; set; }
        public DateTime? DateForTesting { get; set; }
        public string SystemSource { get; set; }

        public ExecuteHttpRequest(ILogger<ExecuteHttpRequest> log, string auditSource)
        {
            _log = log;
            _auditSource = auditSource;
        }

        public async Task<HttpResponseMessage> ExecuteHttpGet(string url, string acceptType = "application/json", int timeout = 60)
        {
            var httpClient = GetHttpClient(acceptType, timeout);
            var result = await httpClient.GetAsync(url);

            await ValidateResponse(result);
            return result;
        }

        public async Task<HttpResponseMessage> ExecuteHttpPatch<T>(string url, T entities, string acceptType = "application/json")
        {
            var httpClient = GetHttpClient(acceptType);
            var stopwatch = Stopwatch.StartNew();
            var result = await httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = SetupJson(entities)
            });
            stopwatch.Stop();
            LogStopwatch(stopwatch, url);

            await ValidateResponse(result);
            return result;

        }

        //public async Task<HttpResponseMessage> ExecuteHttpPost<T>(string url, List<T> entities, string acceptType = "application/json")
        //{
        //    var httpClient = GetHttpClient(acceptType);
        //    var stopwatch = Stopwatch.StartNew();
        //    var result = await httpClient.PostAsync(url, SetupJson(entities));
        //    stopwatch.Stop();
        //    LogStopwatch(stopwatch, url);

        //    await ValidateResponse(result);
        //    return result;

        //}

        public async Task<HttpResponseMessage> ExecuteHttpPost<T>(string url, T entity, string acceptType = "application/json", int timeout = 60)
        {
            var httpClient = GetHttpClient(acceptType, timeout);
            var stopwatch = Stopwatch.StartNew();
            var result = await httpClient.PostAsync(url, SetupJson(entity));
            stopwatch.Stop();
            LogStopwatch(stopwatch, url);

            await ValidateResponse(result);
            return result;
        }

        //public async Task<HttpResponseMessage> ExecuteHttpPost<T>(string url, T entity, string acceptType = "application/json")
        //{
        //    var httpClient = GetHttpClient(acceptType);
        //    var stopwatch = Stopwatch.StartNew();
        //    var result = await httpClient.PostAsync(url, SetupJson(entity));
        //    stopwatch.Stop();
        //    LogStopwatch(stopwatch, url);

        //    await ValidateResponse(result);
        //    return result;
        //}

        public async Task<HttpResponseMessage> ExecuteHttpPut<T>(string url, T entity, string acceptType = "application/json")
        {
            var httpClient = GetHttpClient(acceptType);
            var stopwatch = Stopwatch.StartNew();
            var result = await httpClient.PutAsync(url, SetupJson(entity));
            stopwatch.Stop();
            LogStopwatch(stopwatch, url);

            await ValidateResponse(result);
            return result;
        }

        public async Task<HttpResponseMessage> ExecuteHttpDelete(string url)
        {
            var httpClient = GetHttpClient();
            var stopwatch = Stopwatch.StartNew();
            var result = await httpClient.DeleteAsync(url);
            stopwatch.Stop();
            LogStopwatch(stopwatch, url);

            await ValidateResponse(result);
            return result;
        }

        private HttpClient GetHttpClient(string acceptType = "application/json", int timeout = 60)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    ApiKey);

            httpClient.DefaultRequestHeaders.Add("Accept", acceptType);
            httpClient.DefaultRequestHeaders.Add("x-audit-source", _auditSource);
            httpClient.DefaultRequestHeaders.Add("x-system-source", SystemSource);

            httpClient.Timeout = new TimeSpan(0, 0, 0, timeout);

            LookForTimeSimulationCookieAndSetIt(httpClient);

            _log.LogInformation("ApiKey: {0}", httpClient.DefaultRequestHeaders.Authorization);
            return httpClient;
        }

        private void LookForTimeSimulationCookieAndSetIt(HttpClient httpClient)
        {
            if (DateForTesting.HasValue)
                httpClient.DefaultRequestHeaders.Add("DateForTesting", DateForTesting.Value.ToString());
        }

        private HttpContent SetupJson<T>(T dto)
        {
            var inputJson = JsonConvert.SerializeObject(dto);
            return new StringContent(inputJson, Encoding.UTF8, "application/json");
        }

        private async Task ValidateResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
            {
                var content = await response.Content.ReadAsStringAsync();
                ApiError apiError = JsonConvert.DeserializeObject<ApiError>(content);

                // DEBT: This is catching errors outside of API control and creating our friendly ApiError
                if (apiError.Errors == null)
                {
                    apiError = HttpResponseHelper.CreateApiError(response.StatusCode, content);
                }


                throw new ApiClientException(apiError.Errors.Any() ? apiError.Errors.First().Value : response.ReasonPhrase, null, apiError);
            }
        }

        private void LogStopwatch(Stopwatch stopwatch, string url)
        {
            _log.LogInformation("API Request: {0}ms - {1}", stopwatch.ElapsedMilliseconds, url);
        }

    }
}
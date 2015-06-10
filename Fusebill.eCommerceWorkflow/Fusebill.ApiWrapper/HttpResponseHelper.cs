using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Fusebill.ApiWrapper
{
    public static class HttpResponseHelper
    {
        public static HttpResponseMessage SetAccessControlHeaders(HttpResponseMessage response)
        {
            if (response == null) 
                return null;

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Expose-Headers", "X-Count");
            response.Headers.Add("Access-Control-Expose-Headers", "X-CurrentPage");
            response.Headers.Add("Access-Control-Expose-Headers", "X-PreviousPage");
            response.Headers.Add("Access-Control-Expose-Headers", "X-NextPage");
            response.Headers.Add("Access-Control-Expose-Headers", "X-MaxCount");
            response.Headers.Add("Access-Control-Expose-Headers", "X-PageSize");
            response.Headers.Add("Access-Control-Expose-Headers", "X-MaxPageIndex");
            response.Headers.Add("Access-Control-Expose-Headers", "X-SortExpression");
            response.Headers.Add("Access-Control-Expose-Headers", "X-SortOrder");
            response.Headers.Add("Access-Control-Expose-Headers", "X-AuthorizationFailed");

            return response;
        }

        public static Dto.ApiError CreateApiError(HttpStatusCode statusCode, string error)
        {
            return new Dto.ApiError
            {
                HttpStatusCode = statusCode,
                Errors = CreateErrorListFromString(error)
            };
        }

        public static Dto.ApiError CreateApiError(HttpStatusCode statusCode, Exception exception)
        {
            return new Dto.ApiError
            {
                HttpStatusCode = statusCode,
                Errors = CreateErrorListFromException(exception)
            };
        }

        public static HttpResponseMessage CreateResponseWithAccessControlHeaders(HttpRequestMessage request, HttpStatusCode statusCode, string error)
        {
            return SetAccessControlHeaders(CreateHttpResponseMessage(request, CreateApiError(statusCode, error)));
        }

        public static HttpResponseMessage CreateResponseWithAccessControlHeaders(HttpRequestMessage request, HttpStatusCode statusCode, Exception exception)
        {
            return SetAccessControlHeaders(CreateHttpResponseMessage(request, CreateApiError(statusCode, exception)));
        }

        public static HttpResponseMessage CreateHttpResponseMessage(HttpRequestMessage request, HttpStatusCode statusCode, string error)
        {
            return SetAccessControlHeaders(CreateHttpResponseMessage(request, CreateApiError(statusCode, error)));
        }

        private static HttpResponseMessage CreateHttpResponseMessage(HttpRequestMessage request, Dto.ApiError apiError)
        {
            return request.CreateResponse<Dto.ApiError>(apiError.HttpStatusCode, apiError);
        }

        private static List<KeyValuePair<string, string>> CreateErrorListFromException(Exception exception)
        {
            var errors = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Api Error", GetDeepestLevelException(exception).Message)
            };

            return errors;
        }

        private static Exception GetDeepestLevelException(Exception exception)
        {
            while (exception.InnerException != null)
            {
                exception = GetDeepestLevelException(exception.InnerException);
            }

            return exception;
        }

        private static List<KeyValuePair<string, string>> CreateErrorListFromString(string error)
        {
            var errors = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Api Error", error)
            };

            return errors;
        }
    }
}

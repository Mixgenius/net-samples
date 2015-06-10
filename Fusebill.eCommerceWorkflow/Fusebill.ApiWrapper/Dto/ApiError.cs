using System;
using System.Collections.Generic;
using System.Net;

namespace Fusebill.ApiWrapper.Dto
{
    public class ApiError
    {
        public int ErrorId { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public List<KeyValuePair<string, string>> Errors { get; set; }

        public override string ToString()
        {
            string error = String.Format("Api Error: {0}\nHttp Status Code: {1}\nErrors:\n", ErrorId, HttpStatusCode);

            foreach (var kvp in Errors)
            {
                error += String.Format("{0} - {1}\n", kvp.Key, kvp.Value);
            }

            return error;
        }
    }
}

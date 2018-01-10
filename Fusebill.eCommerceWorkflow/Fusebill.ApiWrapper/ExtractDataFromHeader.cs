using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Fusebill.ApiWrapper.Contracts;

namespace Fusebill.ApiWrapper
{
    public class ExtractDataFromHeaders : IExtractDataFromHeaders
    {
        private readonly HttpResponseHeaders _httpResponseHeaders;

        public ExtractDataFromHeaders(HttpResponseHeaders httpResponseHeaders)
        {
            _httpResponseHeaders = httpResponseHeaders;
        }

        public virtual Dto.Get.PagingHeaderData TryExtractPaginationDataFromHeader()
        {
            try
            {
                var pagingHeaderData = new Dto.Get.PagingHeaderData
                {
                    Count = ExtractInt64ValueFromHeader("X-Count"),
                    CurrentPage = ExtractInt64ValueFromHeader("X-CurrentPage"),
                    PreviousPage = ExtractInt64ValueFromHeader("X-PreviousPage"),
                    NextPage = ExtractInt64ValueFromHeader("X-NextPage"),
                    MaxCount = ExtractInt64ValueFromHeader("X-MaxCount"),
                    PageSize = ExtractInt64ValueFromHeader("X-PageSize"),
                    MaxPageIndex = ExtractInt64ValueFromHeader("X-MaxPageIndex"),
                    SortExpression = ExtractStringValueFromHeader("X-SortExpression"),
                    SortOrder = ExtractStringValueFromHeader("X-SortOrder")
                };

                return pagingHeaderData;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
}

        public long ExtractInt64ValueFromHeader(string key)
        {
            object value = ExtractStringValueFromHeader(key);

            long int64Value;

            Int64.TryParse(value.ToString(), out int64Value);

            return int64Value;
        }

        public string ExtractStringValueFromHeader(string key)
        {
            IEnumerable<string> values;
            if (!_httpResponseHeaders.TryGetValues(key, out values))
            {
                throw new KeyNotFoundException(string.Format("{0} does not exist in the headers", key));
            }

            return values.FirstOrDefault();
        }
    }
}
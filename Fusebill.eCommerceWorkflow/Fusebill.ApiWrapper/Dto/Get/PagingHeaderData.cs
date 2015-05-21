using System;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Get
{
    public class PagingHeaderData : IEquatable<PagingHeaderData>, IPagingHeaderData
    {
        [JsonProperty(PropertyName = "count")]
        public long Count { get; set; }
        [JsonProperty(PropertyName = "currentPage")]
        public long CurrentPage { get; set; }
        [JsonProperty(PropertyName = "previousPage")]
        public long PreviousPage { get; set; }
        [JsonProperty(PropertyName = "nextPage")]
        public long NextPage { get; set; }
        [JsonProperty(PropertyName = "maxCount")]
        public long MaxCount { get; set; }
        [JsonProperty(PropertyName = "pageSize")]
        public long PageSize { get; set; }
        [JsonProperty(PropertyName = "maxPageIndex")]
        public long MaxPageIndex { get; set; }
        [JsonProperty(PropertyName = "sortExpression")]
        public string SortExpression { get; set; }
        [JsonProperty(PropertyName = "sortOrder")]
        public string SortOrder { get; set; }


        public bool Equals(PagingHeaderData other)
        {
            if (null == other)
            {
                return false;
            }
            return (Count == other.Count && CurrentPage == other.CurrentPage && PreviousPage == other.PreviousPage && NextPage == other.NextPage
                && MaxCount == other.MaxCount && PageSize == other.PageSize && MaxPageIndex == other.MaxPageIndex && SortExpression == other.SortExpression
                && SortOrder == other.SortOrder);
        }
    }
}

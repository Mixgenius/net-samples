using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusebill.ApiWrapper
{
    public class QueryOptions
    {
        public const int MAX_COUNT = -1;

        public QueryOptions()
        {
            PageNumber = 0;
            PageSize = null;
            DefaultPageSize = 10000;
            Query = String.Empty;
            QuickSearch = String.Empty;
            SortOrder = "Ascending";
            SortExpression = null;
            DefaultSortExpression = "id";
        }

        public int PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int DefaultPageSize { get; set; }
        public string Query { get; set; }
        public string QuickSearch { get; set; }
        public string SortOrder { get; set; }
        public string SortExpression { get; set; }
        public string DefaultSortExpression { get; set; }

        public string SortExpressionForQuery
        {
            get
            {
                return SortExpression ?? DefaultSortExpression;
            }
        }

        public int PageSizeForQuery
        {
            get
            {
                return PageSize ?? DefaultPageSize;
            }
        }

        public int MaxCount
        {
            get { return MAX_COUNT; }
        }

        public override string ToString()
        {
            var queryString = string.Format("?pageNumber={0}&pageSize={1}&sortOrder={2}&sortExpression={3}&query={4}&quickSearch={5}", PageNumber, PageSize, SortOrder, SortExpression, Query, QuickSearch);
            return queryString;
        }

        public string SortingOrder()
        {
            if (ShouldChangeDefaultSortOrder())
                SortOrder = "Descending";

            var currentSortOrder = string.Equals(SortOrder, "Ascending", StringComparison.OrdinalIgnoreCase) ? "OrderBy" : "OrderByDescending";
            return currentSortOrder;
        }

        private bool ShouldChangeDefaultSortOrder()
        {
            return null == SortExpression && (DefaultSortExpression == "id" || DefaultSortExpression == "associatedId");
        }

        public void SetPageSizeIfNull(int pageSize)
        {
            if (!PageSize.HasValue)
                PageSize = pageSize;
        }
    }
}

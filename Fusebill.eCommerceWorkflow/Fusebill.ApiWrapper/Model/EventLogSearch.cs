using System;

namespace Model
{
    public class EventLogSearch
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string SortOrder { get; set; }

        public string SortExpression { get; set; }
        
        public bool HideSuccessful { get; set; }

        public int EventType { get; set; }

        public int ChannelId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public override string ToString()
        {
            var toString = String.Format("fromDate={0}&toDate={1}&sortOrder={2}&sortExpression={3}&hideSuccessful={4}&eventtypeid={5}&channelId={6}&pageNumber={7}&pageSize={8}",
                FromDate, ToDate, SortOrder, SortExpression, HideSuccessful, EventType, ChannelId, PageNumber, PageSize);

            // return a Communication Platform to retrieve all events for all channels
            if (EventType == 0 && ChannelId == 0)
            {
                toString = String.Format("fromDate={0}&toDate={1}&sortOrder={2}&sortExpression={3}&hideSuccessful={4}&pageNumber={5}&pageSize={6}", FromDate, ToDate, SortOrder, SortExpression, HideSuccessful, PageNumber, PageSize);
                return toString;
            }

            // return a Communication Platform to retrieve all events for a specific channel
            if (EventType == 0)
            {
                toString = String.Format("fromDate={0}&toDate={1}&sortOrder={2}&sortExpression={3}&hideSuccessful={4}&channelId={5}&pageNumber={6}&pageSize={7}", FromDate, ToDate, SortOrder, SortExpression, HideSuccessful, ChannelId, PageNumber, PageSize);
                return toString;
            }

            return toString;
        }
    }
}
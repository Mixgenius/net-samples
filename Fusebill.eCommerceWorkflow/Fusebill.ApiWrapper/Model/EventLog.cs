using System;
using System.Collections.Generic;

namespace Model
{
    public class EventLog : Model.Entity
    {
        public int Id { get; set; }

        public long EventStatusId { get; set; }

        public string ChannelName { get; set; }

        public string Status { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime? SentTimestamp { get; set; }

        public string EventType { get; set; }

        public string Body { get; set; }

        public List<string> Errors { get; set; }
    }
}
using System;

namespace Model
{
    public class RequestLog
    {
        public string AccountId { get; set; } 
        public DateTime RequestTimestamp { get; set; }
        public string IpAddress { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        public string AuditSource { get; set; }
    }
}

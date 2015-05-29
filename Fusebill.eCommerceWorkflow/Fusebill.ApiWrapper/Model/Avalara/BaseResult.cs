using System;

namespace Model.Avalara
{
    [Serializable]
    public class BaseResult
    {
        public SeverityLevel ResultCode { get; set; }

        public Message[] Messages { get; set; }
    }
}

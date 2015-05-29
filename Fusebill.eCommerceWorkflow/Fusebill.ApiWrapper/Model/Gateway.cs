using System;

namespace Model
{
    public class Gateway
    {
        public int AccountConfigurationId { get; set; }
        public int AccountId { get; set; }
        public int ProcessorId { get; set; }
        public string AccountConfigurationName { get; set; }
        public string SerializedConfiguration { get; set; }
        public DateTime? Deleted { get; set; }
        public bool IsCreditCardDefault { get; set; }
        public bool IsAchDefault { get; set; }
        public int NumberOfRoutes { get; set; }
        public Processor Processor { get; set; }
    }

    public class Processor
    {
        public bool IsAch { get; set; }
        public bool IsCreditCard { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class PostSubscriptionVM
    {
        public long SubscriptionID { get; set; }
        public string NameOverride { get; set; }

        public string DescriptionOverride { get; set; }

        public string ChargeOverride { get; set; }
        public string SetupOverride { get; set; }

        public string Reference { get; set; }
        public string ContractStartTimestamp { get; set; }
        public string ContractEndTimestamp { get; set; }

        public string ScheduledActivationTimestamp { get; set; }

        public string RemainingInterval { get; set; }

        public List<string> ProductQuantityOverrides { get; set; }
        public List<string> ProductPriceOverrides { get; set; }

        //public List<object> SubscriptionOverrides { get; set; }
        //public List<string> TimeStamps { get; set; }

    }
}
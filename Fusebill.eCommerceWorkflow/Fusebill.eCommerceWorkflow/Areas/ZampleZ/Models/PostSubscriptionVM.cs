using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.ZampleZ.Models
{
    public class PostSubscriptionVM
    {
        public List<Fusebill.ApiWrapper.Dto.Get.Subscription> subscriptions { get; set; }

        public long SubscriptionID { get; set; }
        public string NameOverride{ get; set; }

        public string DescriptionOverride { get; set; }

        public long ChargeOverride { get; set; }
        public long SetupOverride { get; set; }

        public string Reference { get; set; }
        public DateTime ContractStartTimestamp { get; set; }
        public DateTime ContractEndTimestamp { get; set; }

        public DateTime ScheduledActivationTimestamp { get; set; }

        public int RemainingInterval { get; set; }

        public List<int> ProductQuantityOverride { get; set; }

    }
}
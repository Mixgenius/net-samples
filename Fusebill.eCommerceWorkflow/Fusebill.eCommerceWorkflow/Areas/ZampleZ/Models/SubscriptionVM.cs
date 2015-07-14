using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.ZampleZ.Models
{
    public class SubscriptionVM
    {
        public List<Fusebill.ApiWrapper.Dto.Get.Subscription> subscriptions { get; set; }
    }
}
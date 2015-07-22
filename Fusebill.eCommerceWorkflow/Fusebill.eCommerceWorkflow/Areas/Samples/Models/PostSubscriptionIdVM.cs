using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class PostSubscriptionIdVM
    {
        
        public long SubscriptionID { get; set; }

        public List<string> InputValuesForActivationAndProvision { get; set; }
    }
}
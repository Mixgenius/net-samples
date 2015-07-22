using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class CreateSubscriptionVM
    {
        public long CustomerID { get; set; }
        public long PlanFrequencyID { get; set; }

    }
}
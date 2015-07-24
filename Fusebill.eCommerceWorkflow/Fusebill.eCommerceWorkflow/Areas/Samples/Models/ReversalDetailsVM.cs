using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class ReversalDetailsVM
    {
        public string reverseOption { get; set; }
        public decimal specificAmount { get; set; }

        public long invoiceID { get; set; }
        public string reference { get; set; }
    }
}
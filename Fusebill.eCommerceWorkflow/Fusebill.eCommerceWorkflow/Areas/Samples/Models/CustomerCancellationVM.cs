using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class CustomerCancellationVM
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string primaryEmail { get; set; }
        public string primaryPhone { get; set; }
        public long id { get; set; }
        public string cancellationOption { get; set; }

    }
}
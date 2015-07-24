using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class ReversalVM
    {
        public List<ApiWrapper.Dto.Get.Invoice> invoices { get; set; }
    }
}
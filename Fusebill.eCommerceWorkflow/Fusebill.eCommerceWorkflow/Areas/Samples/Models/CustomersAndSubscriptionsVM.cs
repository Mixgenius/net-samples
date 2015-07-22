using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class CustomersAndSubscriptionsVM
    {
        public List<Fusebill.ApiWrapper.Dto.Get.Customer> AvailableCustomers { get; set; }
        public List<Fusebill.ApiWrapper.Dto.Get.Plan> AvailablePlans { get; set; }
    }
}
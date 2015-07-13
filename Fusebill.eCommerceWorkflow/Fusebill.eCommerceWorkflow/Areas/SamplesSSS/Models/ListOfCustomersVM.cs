using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class ListOfCustomersVM
    {
        public List<Fusebill.ApiWrapper.Dto.Get.Customer> Customers { get; set; }
    }
}
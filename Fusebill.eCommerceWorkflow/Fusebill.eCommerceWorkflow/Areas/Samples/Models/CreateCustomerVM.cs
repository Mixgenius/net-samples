using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Areas.Samples.Models
{
    public class CreateCustomerVM
    {
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string companyName {get; set;}
        public string primaryEmail {get; set;}
        public string primaryPhone {get; set;}
        public string reference {get; set;}
        public string customerReference {get; set;}
        public string salesTrackingCode {get; set;}

    }
}
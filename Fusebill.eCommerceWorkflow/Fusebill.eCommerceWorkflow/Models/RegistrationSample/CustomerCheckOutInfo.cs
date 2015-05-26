using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusebill.eCommerceWorkflow.Models.RegistrationSample
{
    class CustomerCheckOutInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string CardNumber { get; set; }

        public int CVV { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }

        public string ZipCode { get; set; }



    }
}

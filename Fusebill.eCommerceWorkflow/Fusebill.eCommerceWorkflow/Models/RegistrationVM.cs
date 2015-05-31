using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class RegistrationVM
    {
        //A list of all plans shown on the start page, currently contains bronze and gold plan
        public List<Plan> AvailablePlans { get; set; }

        //the ID of the plan that the user clicks on
        public long SelectedPlanID { get; set; }

        //A list of the available products in each plan, used in Step2
        public List<PlanProduct> AvailableProducts { get; set; }

        //How many of each product the user decides to have
        public Dictionary<string, decimal> QuantityOfProducts { get; set; }

        //The Customer object contains properties pertaining to the customer, such as FrstName, LastName
        public Customer customerInformation { get; set; }

        //The Address objects contains properties pertaining to the billing address, such as City and PostalZip 
        public Address billingAddress { get; set; }

        //The Subscription objects contains information needed for the customer to create a subscription
        public Subscription subscription { get; set; }
    }
}
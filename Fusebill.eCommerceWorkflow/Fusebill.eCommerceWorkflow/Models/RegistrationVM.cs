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

    }
}
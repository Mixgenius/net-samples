using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusebill.eCommerceWorkflow.Models
{
    public class RegistrationStronglyTypedSessionState
    {
        public string customerInformation;
        public string billingAddress;

        //keeps track of the plan ID to access the plan products page
        public string selectedPlanId;
        ///keeps track of the number of each plan product, key value can be string as well
        public Dictionary<PlanProduct, int> PlanProductQuantities;
        public Dictionary<PlanProduct, bool> PlanProductIncludes;      


        public RegistrationStronglyTypedSessionState()
        {
            customerInformation = "customerInformation";
            billingAddress = "billingAddress";
            PlanProductQuantities = new Dictionary<PlanProduct, int>();
            selectedPlanId = "selectedPlanId";

        }
    }
}
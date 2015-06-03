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
        public string selectedPlanFrequencyID;
        ///keeps track of the number of each plan product, key value can be string as well
        public string planProductQuantities;
        public string planProductIncludes;
        public string sameAsBilling;

        //keeps track of the postCustomer object because we're using it twice
        public string postCustomerActivation;



        public RegistrationStronglyTypedSessionState()
        {
            customerInformation = "customerInformation";
            billingAddress = "billingAddress";
            planProductQuantities = "planProductQuantities";
            planProductIncludes = "planProductIncludes";
            selectedPlanId = "selectedPlanId";
            sameAsBilling = "sameAsBilling";
            selectedPlanFrequencyID = "selectedPlanFrequencyID";
            postCustomerActivation = "postCustomerActivation";
        }
    }
}
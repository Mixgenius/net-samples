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
        public string shippingAddress;

        //keeps track of the plan ID to access the plan products page
        public string selectedPlanId;
        public string selectedPlanFrequencyID;
        public string selectedCountryName;
        public string selectedStateName;
        public string selectedCountryID;
        public string selectedStateID;

        ///keeps track of the number of each plan product, key value can be string as well
        public string planProductQuantities;
        public string planProductIncludes;
        public string planProducts;
        public string sameAsBilling;

        //keeps track of the postCustomer object because we're using it twice
        public string postCustomerActivation;



        public RegistrationStronglyTypedSessionState()
        {
            customerInformation = "customerInformation";
            shippingAddress = "shippingAddress";
            planProductQuantities = "planProductQuantities";
            planProductIncludes = "planProductIncludes";
            selectedPlanId = "selectedPlanId";
            sameAsBilling = "sameAsBilling";
            selectedPlanFrequencyID = "selectedPlanFrequencyID";
            postCustomerActivation = "postCustomerActivation";
            planProducts = "planProducts";
            selectedCountryID = "selectedCountry";
            selectedStateID = "selectedState";
            selectedCountryName = "selectedCountryName";
            selectedStateName = "selectedStateName";
        }
    }
}
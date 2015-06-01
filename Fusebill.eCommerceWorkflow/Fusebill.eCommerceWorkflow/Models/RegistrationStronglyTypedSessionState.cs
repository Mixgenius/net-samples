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
        public Dictionary<PlanProduct, int> PlanProductQuantities;

        public RegistrationStronglyTypedSessionState()
        {
            customerInformation = "customerInformation";
            billingAddress = "billingAddress";
            PlanProductQuantities = new Dictionary<PlanProduct, int>();

        }
    }
}
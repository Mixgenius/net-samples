using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Models
{
    public enum FromStep 
    {
        Step1 = 1, 
        Step2 = 2, 
        Step3 = 3, 
        Step4 = 4, 
        Step5 = 5,
    }
    public class RegistrationVM
    {

        public int Step { get; set; }

        /// <summary>
        /// Update this list with your account's plans
        /// </summary>
        public List<Plan> AvailablePlans { get; set; }
        
        /// <summary>
        /// User selected plan from step 1
        /// </summary>
        public long SelectedPlanId { get; set; }
        
        /// <summary>
        /// A list of the products available from the selected plan
        /// </summary>
        public List<PlanProduct> AvailableProducts { get; set; }

        /// <summary>
        /// User subscriptions are tied to the Plan-Frequency
        /// </summary>
        public List<long> AvailablePlanFrequencyIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long SelectedPlanFrequencyID { get; set; }


        
        public Customer CustomerInformation { get; set; }
        public Address BillingAddress { get; set; }
        public Address CreditAddress { get; set; }

        /// <summary>
        /// Used in our countries dropdown
        /// </summary>
        public List<SelectListItem> ListOfCountriesSLI {get;set;}
        public List<Country> ListOfCountriesCountry { get; set; }


        /// <summary>
        /// Demo credit cards
        /// </summary>
        public List<SelectListItem> ListOfCreditCards { get; set; }
        public List<SelectListItem> ListOfExpirationMonths { get; set; }
        public List<SelectListItem> ListOfExpirationYears { get; set; }

        /// <summary>
        /// Customer after activation will contain invoice details
        /// </summary>
        public Customer ActivatedCustomer { get; set; }
        public Customer ReturnedCustomer { get; set; }


        public string SelectedCountryName { get; set; }
        public string SelectedStateName { get; set; }
        

        public string Cvv { get; set;  }
        public string CreditCardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }


    }
}
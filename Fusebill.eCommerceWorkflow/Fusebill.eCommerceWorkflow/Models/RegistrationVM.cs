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
    public class RegistrationVM
    {
        //A list of all plans shown on the start page, currently contains bronze and gold plan
        public List<Plan> AvailablePlans { get; set; }
        
        //the ID of the plan that the user clicks on
        public long SelectedPlanId { get; set; }

        //A list of the available products in each plan, used in Step2
        public List<PlanProduct> AvailableProducts { get; set; }

        public List<long> AvailablePlanFrequencyIds { get; set; }

        public long SelectedPlanFrequencyID { get; set; }

        //total invoice
        public decimal invoiceTotal { get; set; }


        //The Customer object contains properties pertaining to the customer, such as FrstName, LastName
        public Customer CustomerInformation { get; set; }

        //The Address objects contains properties pertaining to the billing address, such as City and PostalZip 
        public Address BillingAddress { get; set; }

        public Address CreditAddress { get; set; }


        public bool CreditCardSameAsBilling { get; set; }

        public List<SelectListItem> ListOfCountriesSLI {get;set;}

        public List<SelectListItem> ListOfCreditCards { get; set; }
        public List<SelectListItem> listOfExpirationMonths { get; set; }

        public List<SelectListItem> ListOfExpirationYears { get; set; }

        public List<Country> ListOfCountriesCountry { get; set; }


        public Customer ReturnedCustomerActivation { get; set; }

        public Customer ReturnedCustomer { get; set; }

        public string SelectedCountryName { get; set; }

        public string SelectedStateName { get; set; }

        //The Subscription objects contains information needed for the customer to create a subscription
        public Subscription Subscription { get; set; }

        public string Cvv { get; set;  }

        public string CreditCardNumber { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }


    }
}
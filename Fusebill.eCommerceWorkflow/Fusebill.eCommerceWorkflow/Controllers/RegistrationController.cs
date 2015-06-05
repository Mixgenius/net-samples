using Fusebill.ApiWrapper;
using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AutoMapper;
using Fusebill.eCommerceWorkflow.Models;

/*
 * Step3, in state dropdown: select your state for non-Canadian vs select your province for Canada
 * Step3, validation
 *  postBillingAddress.CustomerAddressPreferenceId, and how to find it
 * 
 * */


namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class RegistrationController : FusebillController
    {

        //
        // GET: /Registration/

        public ActionResult Index()
        {
            RegistrationVM step1RegistrationVM = new RegistrationVM();

            var desiredPlanIds = ConfigurationManager.AppSettings["DesiredPlanIds"];

            //string array, each element is the ID of a desired plan
            var desiredPlans = desiredPlanIds.Split(',');

            step1RegistrationVM.AvailablePlans = new List<Plan>();
            step1RegistrationVM.AvailablePlanFrequencyIds = new List<long>();


            foreach (string element in desiredPlans)
            {
                var plan = ApiClient.GetPlan(Convert.ToInt64(element));
                step1RegistrationVM.AvailablePlans.Add(plan);
                step1RegistrationVM.AvailablePlanFrequencyIds.Add(plan.PlanFrequencies[0].Id);

            }

            return View(step1RegistrationVM);
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step2GetPlanProducts")]
        public ActionResult Step2GetPlanProducts(RegistrationVM registrationVM)
        {
            RegistrationVM step2RegistrationVM = new RegistrationVM();
            step2RegistrationVM.AvailableProducts = new List<PlanProduct>();

            if (registrationVM.customerInformation != null)
            {
                Session[new RegistrationStronglyTypedSessionState().customerInformation] = registrationVM.customerInformation;
            }
            if (registrationVM.shippingAddress != null)
            {
                Session[new RegistrationStronglyTypedSessionState().shippingAddress] = registrationVM.shippingAddress;
            }
            if (registrationVM.sameAsBilling != null)
            {
                Session[new RegistrationStronglyTypedSessionState().sameAsBilling] = registrationVM.sameAsBilling;
            }

            //If selectedPlanId is 0, then its value is stored in the session. If it is not 0, its value is not yet stored so let's store it.
            if (registrationVM.SelectedPlanId != 0)
            {
                Session[new RegistrationStronglyTypedSessionState().selectedPlanId] = registrationVM.SelectedPlanId;
            }
            //same reasoning as above
            if (registrationVM.SelectedPlanFrequencyID != 0)
            {
                Session[new RegistrationStronglyTypedSessionState().selectedPlanFrequencyID] = registrationVM.SelectedPlanFrequencyID;
            }

            step2RegistrationVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(((long)Session[new RegistrationStronglyTypedSessionState().selectedPlanId]), new QueryOptions { }).Results;

            //This stores the list of plan products. We actually simply need to store the id of each plan product
            Session[new RegistrationStronglyTypedSessionState().planProducts] = step2RegistrationVM.AvailableProducts;

            //the QuantityOfProducts instance will hold the number of each product the user chooses to obtain
            step2RegistrationVM.QuantityOfProducts = new Dictionary<string, decimal>();

            //if the session state containing the dictionary with the number of products exists, we'll use that to display the number of products.
            //if the session state DOESN'T EXIST, we'll use the original APIClient values

            if (Session[new RegistrationStronglyTypedSessionState().planProductQuantities] == null)
            {
                for (int i = 0; i < step2RegistrationVM.AvailableProducts.Count; i++)
                {
                    step2RegistrationVM.QuantityOfProducts.Add(step2RegistrationVM.AvailableProducts[i].ProductName, step2RegistrationVM.AvailableProducts[i].Quantity);
                }
            }
            else
            {
                step2RegistrationVM.QuantityOfProducts = ((Dictionary<string, decimal>)Session[new RegistrationStronglyTypedSessionState().planProductQuantities]);
            }

            step2RegistrationVM.planProductIncludes = new Dictionary<string, bool>();

            //If the session state containing the dictionary with the checkbox values, we'll use that to display the checkbox values.
            //if the session state DOESN'T EXIST, we'll set the checkbox values to false
            if (Session[new RegistrationStronglyTypedSessionState().planProductIncludes] == null)
            {
                for (int i = 0; i < step2RegistrationVM.AvailableProducts.Count; i++)
                {
                    step2RegistrationVM.planProductIncludes.Add(step2RegistrationVM.AvailableProducts[i].ProductName, !step2RegistrationVM.AvailableProducts[i].IsOptional);
                }
            }
            else
            {
                step2RegistrationVM.planProductIncludes = ((Dictionary<string, bool>)Session[new RegistrationStronglyTypedSessionState().planProductIncludes]);
            }

            return View(step2RegistrationVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step3GetCustomerInformation")]
        public ActionResult Step3GetCustomerInformation(RegistrationVM registrationVM)
        {

            RegistrationVM step3RegistrationVM = new RegistrationVM();
            step3RegistrationVM = registrationVM;

            if (step3RegistrationVM.QuantityOfProducts != null)
            {
                //store previous number of products and included values into session 
                Session[new RegistrationStronglyTypedSessionState().planProductQuantities] = step3RegistrationVM.QuantityOfProducts;
            }
            if (step3RegistrationVM.planProductIncludes != null)
            {

                //store previous value of checked checkboxes into session
                Session[new RegistrationStronglyTypedSessionState().planProductIncludes] = step3RegistrationVM.planProductIncludes;
            }

            step3RegistrationVM.customerInformation = new Customer();
            step3RegistrationVM.shippingAddress = new Address();

            //if the session holding customer information already exists, the TextBoxFors will display it. If not, to avoid a 
            //null exception error, we make the properties in the customer object empty strings
            if (Session[new RegistrationStronglyTypedSessionState().customerInformation] != null)
            {
                step3RegistrationVM.customerInformation = (Customer)Session[new RegistrationStronglyTypedSessionState().customerInformation];
            }
            else
            {
                step3RegistrationVM.customerInformation.FirstName = String.Empty;
                step3RegistrationVM.customerInformation.LastName = String.Empty;
                step3RegistrationVM.customerInformation.PrimaryEmail = String.Empty;
                step3RegistrationVM.customerInformation.PrimaryPhone = String.Empty;
            }

            //if the session holding billing information already exists, the TextBoxFors will display it. If not, to avoid a 
            //null exception error, we make the properties in the billing object empty strings
            if (Session[new RegistrationStronglyTypedSessionState().shippingAddress] != null)
            {
                step3RegistrationVM.shippingAddress = ((Address)Session[new RegistrationStronglyTypedSessionState().shippingAddress]);
            }
            else
            {
                step3RegistrationVM.shippingAddress.CompanyName = String.Empty;
                step3RegistrationVM.shippingAddress.Line1 = String.Empty;
                step3RegistrationVM.shippingAddress.Line2 = String.Empty;
                step3RegistrationVM.shippingAddress.City = String.Empty;
                step3RegistrationVM.shippingAddress.PostalZip = String.Empty;
            }

            //checking session for sameAsBilling checkbox
            if (Session[new RegistrationStronglyTypedSessionState().sameAsBilling] != null)
            {
                step3RegistrationVM.sameAsBilling = ((bool)Session[new RegistrationStronglyTypedSessionState().sameAsBilling]);
            }
            else
            {
                step3RegistrationVM.sameAsBilling = false;
            }


            step3RegistrationVM.listOfCountriesSLI = new List<SelectListItem>();
            step3RegistrationVM.listOfCountriesCountry = new List<Country>();
            step3RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();


            for (int i = 0; i < step3RegistrationVM.listOfCountriesCountry.Count; i++)
            {
                step3RegistrationVM.listOfCountriesSLI.Add(new SelectListItem
                {
                    Text = step3RegistrationVM.listOfCountriesCountry[i].Name,
                    Value = step3RegistrationVM.listOfCountriesCountry[i].Id.ToString(),
                });
            }
            return View(step3RegistrationVM);
        }



        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step4GetInvoice")]
        public ActionResult Step4GetInvoice(RegistrationVM registrationVM)
        {
            Session[new RegistrationStronglyTypedSessionState().customerInformation] = registrationVM.customerInformation;
            Session[new RegistrationStronglyTypedSessionState().shippingAddress] = registrationVM.shippingAddress;
            Session[new RegistrationStronglyTypedSessionState().sameAsBilling] = registrationVM.sameAsBilling;

            RegistrationVM step4RegistrationVM = new RegistrationVM();
            step4RegistrationVM = registrationVM;

            step4RegistrationVM.subscription = new Subscription();


            #region                         Creating a Customer
            Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new Fusebill.ApiWrapper.Dto.Post.Customer();

            postCustomer.FirstName = step4RegistrationVM.customerInformation.FirstName;
            postCustomer.LastName = step4RegistrationVM.customerInformation.LastName;
            postCustomer.PrimaryEmail = step4RegistrationVM.customerInformation.PrimaryEmail;
            postCustomer.PrimaryPhone = step4RegistrationVM.customerInformation.PrimaryPhone;

            var c = ApiClient.PostCustomer(postCustomer);

            Fusebill.ApiWrapper.Dto.Post.CustomerActivation postCustomerActivation = new ApiWrapper.Dto.Post.CustomerActivation();
            postCustomerActivation.ActivateAllSubscriptions = true;
            postCustomerActivation.ActivateAllDraftPurchases = true;
            postCustomerActivation.CustomerId = c.Id;
            #endregion




            #region                         Creating an Address for billing

            Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address();

            postBillingAddress.CompanyName = step4RegistrationVM.shippingAddress.CompanyName;
            postBillingAddress.Line1 = step4RegistrationVM.shippingAddress.Line1;
            postBillingAddress.Line2 = step4RegistrationVM.shippingAddress.Line2;
            postBillingAddress.City = step4RegistrationVM.shippingAddress.City;
            postBillingAddress.PostalZip = step4RegistrationVM.shippingAddress.PostalZip;
            postBillingAddress.CountryId = step4RegistrationVM.shippingAddress.CountryId;
            postBillingAddress.StateId = step4RegistrationVM.shippingAddress.StateId;

            //if the billing address is the same as the shipping address, we shall store the country and state ID for the getPayment page
            Session[new RegistrationStronglyTypedSessionState().selectedCountryID] = step4RegistrationVM.shippingAddress.CountryId;
            Session[new RegistrationStronglyTypedSessionState().selectedStateID] = step4RegistrationVM.shippingAddress.StateId;

            step4RegistrationVM.listOfCountriesCountry = new List<Country>();
            step4RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();

            for (int i = 0; i < step4RegistrationVM.listOfCountriesCountry.Count; i++)
            {
                if (step4RegistrationVM.listOfCountriesCountry[i].Id == step4RegistrationVM.shippingAddress.CountryId)
                {
                    //if country ids are identical, store the country name
                    Session[new RegistrationStronglyTypedSessionState().selectedCountryName] = step4RegistrationVM.listOfCountriesCountry[i].Name;

                    //after finding the country, we find the state
                    for (int j = 0; j < step4RegistrationVM.listOfCountriesCountry[i].States.Count; j++)
                    {
                        //if state ids are identical store the state name
                        if (step4RegistrationVM.listOfCountriesCountry[i].States[j].Id == step4RegistrationVM.shippingAddress.StateId)
                        {
                            Session[new RegistrationStronglyTypedSessionState().selectedStateName] = step4RegistrationVM.listOfCountriesCountry[i].States[j].Name;
                        }
                    }
                }
            }
            //A customer address preference ID must be included to post the address. Here, we arbitrarily set it to the value of 23.
            //postBillingAddress.CustomerAddressPreferenceId = 36;
            postBillingAddress.AddressType = "Shipping";
           
            //      var ba = ApiClient.PostAddress(postBillingAddress);
            #endregion




            #region                             Creating a Subscription
            Fusebill.ApiWrapper.Dto.Post.Subscription postSubscription = new ApiWrapper.Dto.Post.Subscription();


            postSubscription.CustomerId = c.Id;
            postSubscription.PlanFrequencyId = ((long)Session[new RegistrationStronglyTypedSessionState().selectedPlanFrequencyID]);

            var subResult = ApiClient.PostSubscription(postSubscription);

            //putting items into subscription

            step4RegistrationVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(((long)Session[new RegistrationStronglyTypedSessionState().selectedPlanId]), new QueryOptions() { }).Results;
            step4RegistrationVM.QuantityOfProducts = ((Dictionary<string, decimal>)Session[new RegistrationStronglyTypedSessionState().planProductQuantities]);
            step4RegistrationVM.planProductIncludes = ((Dictionary<string, bool>)Session[new RegistrationStronglyTypedSessionState().planProductIncludes]);
            step4RegistrationVM.AvailableProducts = ((List<PlanProduct>)Session[new RegistrationStronglyTypedSessionState().planProducts]);

            for (int i = 0; i < step4RegistrationVM.AvailableProducts.Count; i++)
            {
                var product = step4RegistrationVM.QuantityOfProducts[step4RegistrationVM.AvailableProducts[i].ProductName];
                var include = step4RegistrationVM.planProductIncludes[step4RegistrationVM.AvailableProducts[i].ProductName];
                subResult.SubscriptionProducts[i].Quantity = product;
                subResult.SubscriptionProducts[i].IsIncluded = include;
            }


            Automapping.SetupSubscriptionGetToPutMapping();
            var putSubscription = Mapper.Map<Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(subResult);
            #endregion

            ApiClient.PutSubscription(putSubscription);
            var postedCustomerActivation = ApiClient.PostCustomerActivation(postCustomerActivation, true, true);

            //we make an instance of the returned customer to provide its values to the field
            step4RegistrationVM.postedCustomer = new Customer();
            step4RegistrationVM.postedCustomer = postedCustomerActivation;
            step4RegistrationVM.invoiceTotal = postedCustomerActivation.InvoicePreview.Total;

            Session[new RegistrationStronglyTypedSessionState().postCustomerActivation] = postCustomerActivation;

            return View(step4RegistrationVM);
        }

        [HttpGet]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step5GetPayment")]
        public ActionResult Step5GetPayment()
        {
            RegistrationVM step5RegistrationVM = new RegistrationVM();

            step5RegistrationVM.shippingAddress = new Address();
            step5RegistrationVM.shippingAddress.AddressType = "Shipping";

            step5RegistrationVM.customerInformation = new Customer();

            step5RegistrationVM.sameAsBilling = (bool)Session[new RegistrationStronglyTypedSessionState().sameAsBilling];

            //  if the sameAsBilling checkbox is checked, we will use the same fields, otherwise, new fields
            if (step5RegistrationVM.sameAsBilling)
            {
                var shipping = (Address)Session[new RegistrationStronglyTypedSessionState().shippingAddress];
                step5RegistrationVM.shippingAddress.Line1 = shipping.Line1;
                step5RegistrationVM.shippingAddress.Line2 = shipping.Line2;
                step5RegistrationVM.shippingAddress.PostalZip = shipping.PostalZip;
                step5RegistrationVM.shippingAddress.City = shipping.City;
                step5RegistrationVM.shippingAddress.Country = (string)Session[new RegistrationStronglyTypedSessionState().selectedCountryName];
                step5RegistrationVM.shippingAddress.State = (string) Session[new RegistrationStronglyTypedSessionState().selectedStateName];
                var customer = (Customer)Session[new RegistrationStronglyTypedSessionState().customerInformation];
                step5RegistrationVM.customerInformation.FirstName = customer.FirstName;
                step5RegistrationVM.customerInformation.LastName = customer.LastName;
            } 

            step5RegistrationVM.listOfCountriesSLI = new List<SelectListItem>();
            step5RegistrationVM.listOfCountriesCountry = new List<Country>();
            step5RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();

            for (int i = 0; i < step5RegistrationVM.listOfCountriesCountry.Count; i++)
            {
                step5RegistrationVM.listOfCountriesSLI.Add(new SelectListItem
                {

                    Text = step5RegistrationVM.listOfCountriesCountry[i].Name,
                    Value = step5RegistrationVM.listOfCountriesCountry[i].Id.ToString(),
                    Selected = false
                });
            }

            step5RegistrationVM.listOfCreditCards = new List<SelectListItem>();


            step5RegistrationVM.listOfCreditCards.Add(new SelectListItem
            {
                Text = "4242424242424242",
                Value = "4242424242424242" });
            step5RegistrationVM.listOfCreditCards.Add(new SelectListItem
            {
                Text = "4111111111111111",
                Value = "4111111111111111" });

            return View(step5RegistrationVM);
        }



        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step6GetActivation")]
        public ActionResult Step6GetActivation(RegistrationVM registrationVM)
        {
            //WARNING: IN YOUR WEBSITE, USE PCI COMPLIANT CALLS. AN EXAMPLE IS AVAILABLE ON OUR WEBSITE
            RegistrationVM step6RegistrationVM = new RegistrationVM();

            step6RegistrationVM = registrationVM;

            Fusebill.ApiWrapper.Dto.Post.CreditCard postCreditCard = new ApiWrapper.Dto.Post.CreditCard();

            if ( (bool) Session[new RegistrationStronglyTypedSessionState().sameAsBilling])
            {
                postCreditCard.CardNumber = step6RegistrationVM.creditCardNumber;
                postCreditCard.Cvv = step6RegistrationVM.cvv;

                var customer = (Customer)Session[new RegistrationStronglyTypedSessionState().customerInformation];
                postCreditCard.FirstName = customer.FirstName;
                postCreditCard.LastName = customer.LastName;

                var shipping = (Address)Session[new RegistrationStronglyTypedSessionState().shippingAddress];
                postCreditCard.Address1 = shipping.Line1;
                postCreditCard.Address2 = shipping.Line2;
                postCreditCard.PostalZip = shipping.PostalZip;
                postCreditCard.City = shipping.City;

                postCreditCard.CountryId = (long)Session[new RegistrationStronglyTypedSessionState().selectedCountryID];
                postCreditCard.StateId = (long)Session[new RegistrationStronglyTypedSessionState().selectedStateID];
            }
            else
            {
                postCreditCard.FirstName = step6RegistrationVM.customerInformation.FirstName;
                postCreditCard.LastName = step6RegistrationVM.customerInformation.LastName;

                postCreditCard.Address1 = step6RegistrationVM.shippingAddress.Line1;
                postCreditCard.Address2 = step6RegistrationVM.shippingAddress.Line2;
                postCreditCard.PostalZip = step6RegistrationVM.shippingAddress.PostalZip;
                postCreditCard.City = step6RegistrationVM.shippingAddress.City;

                postCreditCard.CountryId = step6RegistrationVM.shippingAddress.CountryId;
                postCreditCard.StateId = step6RegistrationVM.shippingAddress.StateId;
            }

            try
            {
                ApiClient.PostCreditCard(postCreditCard);
                
                //display success message
                //Activate customer
                ApiClient.PostCustomerActivation((Fusebill.ApiWrapper.Dto.Post.CustomerActivation)Session[new RegistrationStronglyTypedSessionState().postCustomerActivation], false, true);
            }
            catch
            {
                //return to view
            }

            return View();
        }
    }
}


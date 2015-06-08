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
//Whenever possible, we extract information from the session, because it acts as the "global container" and because it is consistent

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class RegistrationController : FusebillController
    {
        //Read step2 for why we include this 
        const string STEP2FIRSTVISIT = "STEP2FIRSTVISIT";

        //session key that stores the viewmodel
        const string REGISTRATIONVM = "REGISTRATIONVM";

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

        /// <summary>
        /// Display the available products and which to include and how many to pruchase
        /// </summary>
        /// <param name="registrationVM"></param>
        /// <returns></returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step2GetPlanProducts")]
        public ActionResult Step2GetPlanProducts(RegistrationVM registrationVM)
        {

            RegistrationVM step2RegistrationVM = new RegistrationVM();
            step2RegistrationVM.AvailableProducts = new List<PlanProduct>();


            /*We will place the "parametered" viewmodel into a session. If this is the first time we visit the page ( step 1 to step 2), the viewmodel's planfrequencyID has a value
            Thus, our session will also contain the planfrequencyID. But if this is our second time visiting the page (step3 to step2), the viewmodel's planfrequencyID is null, and thus
            we do not want to store it.
             * If first visit, store the "parametered" view model, if second visit, don't store it.
            */

            //Session[REGISTRATIONVM] = Session[STEP2FIRSTVISIT] ?? registrationVM;

            //if our session did not already exist
            if (Session[STEP2FIRSTVISIT] == null)
            {
                Session[REGISTRATIONVM] = registrationVM;
                Session[STEP2FIRSTVISIT] = false;   //if we enter from Step 3 to step 2, we will not store the local view model and thus will sustain the planFrequencyID
            }

            //we get the planID from the Session because it contains the planID if we proceeded from step1 or step3 whereas the local viewmodal only contains the planID
            //if we proceeded from step1. We store the available products in the Session
            var sessionRegistration = (RegistrationVM)Session[REGISTRATIONVM];
            ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts = ApiClient.GetPlanProductsByPlanId(sessionRegistration.SelectedPlanId, new QueryOptions { }).Results;

            //we populate the AvailableProducts section of the ViewModal with the AvailableProducts section of the Session
            step2RegistrationVM.AvailableProducts = ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts;

            //now, we set the IsIncluded property of certain items in AvailableProducts to true
            for (int i = 0; i < ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts.Count; i++)
            {
                if (((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].IsOptional == false)
                {
                    step2RegistrationVM.AvailableProducts[i].IsIncluded = true;
                }
            }

            



            /*
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
    
            */

            return View(step2RegistrationVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step3GetCustomerInformation")]
        public ActionResult Step3GetCustomerInformation(RegistrationVM registrationVM)
        {

            RegistrationVM step3RegistrationVM = new RegistrationVM();
            step3RegistrationVM = registrationVM;

            //we instantiate these two objects in order for step3View to transmit information to step4 method
            step3RegistrationVM.customerInformation = new Customer();
            step3RegistrationVM.billingAddress = new Address();

            //if we came from step2, we desire to store the quantity and their inclusion into the appropriate fields in the AvailableProducts section of
            //the Session. Yes, we only want to modify those fields because the other fields in the local VM are null and storing them will replace the original data
            //If we came from step4, we wish to ignore their inclusion. How do we tell if we came from step2 or step4? 
            //If we came from step2, the AvailableProducts section of the ViewModel is not null. If we came from step4, the AvailableProducts section of the
            //ViewModel is null

            if (registrationVM.AvailableProducts != null)
            {
                //we use a for loop instead of a foreach loop to also loop through the session's AvailableProducts. We seek to set its quantities and inclusions
                for (int i = 0; i < registrationVM.AvailableProducts.Count; i++)
                {
                    ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].Quantity = registrationVM.AvailableProducts[i].Quantity;
                    ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].IsIncluded = registrationVM.AvailableProducts[i].IsIncluded;
                }
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

            RegistrationVM step4RegistrationVM = new RegistrationVM();
            step4RegistrationVM = registrationVM;

            step4RegistrationVM.subscription = new Subscription();

            /* We wish to store the customer's personal and shipping and the checked-ness of the "Same as billing" checkbox. These fields are
             * populated in the "parametered" VM only if we came from step 3. If we came from step 5, we do not want to store the "parametered" VM's fields
             * because they are null. We also create a new customer and subscription, etc, if we come from step2. How do we check if we come from step 3 or step 5? If the customerInformation, shippingAddress and 
             * shippingSameAsBilling fields are not null, then we came from step3. If they are null, we came from step5. To display the invoice, we extract information from the session
             * which exists if we came from step3 or step5. */

            //this if statement is equivalement to simply if (registartionVM.customerInformation!= null) because if one is null, all must be null
            if ((registrationVM.customerInformation != null && registrationVM.billingAddress != null && registrationVM.shippingSameAsBilling != null))
            {
                ((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling = registrationVM.shippingSameAsBilling;
                ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation = registrationVM.customerInformation;
                ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress = registrationVM.billingAddress;

                //can we do this with automapper?
                //  *****************************************************************************
                //============================== CREATING CUSTOMER ======================================
                //  *****************************************************************************

                Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new ApiWrapper.Dto.Post.Customer();
                postCustomer.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName;
                postCustomer.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName;
                postCustomer.PrimaryEmail = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.PrimaryEmail;
                postCustomer.PrimaryPhone = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.PrimaryPhone;

                ((RegistrationVM) Session[REGISTRATIONVM]).returnedCustomer = ApiClient.PostCustomer(postCustomer);

      
                //  *****************************************************************************
                //============================== CREATING BILLING ADDRESS ======================================
                //  *****************************************************************************

                Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address();
                postBillingAddress.CompanyName = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CompanyName;
                postBillingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1;
                postBillingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2;
                postBillingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City;
                postBillingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip;
                postBillingAddress.CountryId = step4RegistrationVM.billingAddress.CountryId;
                postBillingAddress.StateId = step4RegistrationVM.billingAddress.StateId;

                postBillingAddress.AddressType = "Billing";
                //postBillingAddress.CustomerAddressPreferenceId = 36;
             //   var returnedBilling = ApiClient.PostAddress(postBillingAddress);
         

                //given the selected state's and country's IDs, we shall find their respective names, which are shown in the invoice and possibly
                //in step5 if ShippingSameAsBilling has been checked. 
            step4RegistrationVM.listOfCountriesCountry = new List<Country>();
            step4RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();

            for (int i = 0; i < step4RegistrationVM.listOfCountriesCountry.Count; i++)
            {
                if (step4RegistrationVM.listOfCountriesCountry[i].Id == step4RegistrationVM.billingAddress.CountryId)
                {
                    //if country ids are identical, store the country name
                  ( (RegistrationVM) Session[REGISTRATIONVM]).selectedCountryName = step4RegistrationVM.listOfCountriesCountry[i].Name;

                    //after finding the country, we find the state
                    for (int j = 0; j < step4RegistrationVM.listOfCountriesCountry[i].States.Count; j++)
                    {
                        //if state ids are identical store the state name
                        if (step4RegistrationVM.listOfCountriesCountry[i].States[j].Id == step4RegistrationVM.billingAddress.StateId)
                        {
                            ((RegistrationVM)Session[REGISTRATIONVM]).selectedStateyName = step4RegistrationVM.listOfCountriesCountry[i].States[j].Name;
                        }
                    }
                }
            }
     
         //  *****************************************************************************
         //============================== CREATING SUBSCRIPTION ======================================
         //  *****************************************************************************
    
            Fusebill.ApiWrapper.Dto.Post.Subscription postSubscription = new ApiWrapper.Dto.Post.Subscription();
            postSubscription.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id;
            postSubscription.PlanFrequencyId = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedPlanFrequencyID;

            var returnedSubscription = ApiClient.PostSubscription(postSubscription);


          //  *****************************************************************************
          //============================== PUTTING UPDATED SUBSCRIPTION ======================================
         //  *****************************************************************************

                for (int i = 0; i < ((RegistrationVM) Session[REGISTRATIONVM]).AvailableProducts.Count; i++)
                {
                    var quantity = ((RegistrationVM) Session[REGISTRATIONVM]).AvailableProducts[i].Quantity;
                    var inclusion = ((RegistrationVM) Session[REGISTRATIONVM]).AvailableProducts[i].IsIncluded;
                    returnedSubscription.SubscriptionProducts[i].Quantity = quantity;
                    returnedSubscription.SubscriptionProducts[i].IsIncluded = inclusion;
                }

            Automapping.SetupSubscriptionGetToPutMapping();
            var putSubscription = Mapper.Map<Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(returnedSubscription);

            ApiClient.PutSubscription(putSubscription);
            }

            Fusebill.ApiWrapper.Dto.Post.CustomerActivation postCustomerActivation = new ApiWrapper.Dto.Post.CustomerActivation();
            postCustomerActivation.ActivateAllSubscriptions = true;
            postCustomerActivation.ActivateAllDraftPurchases = true;
            postCustomerActivation.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id;
            ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation = ApiClient.PostCustomerActivation(postCustomerActivation, true, true);


            var asdf = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation;
            
            //now, we set the properties that will be displayed on the step4 view with the properties from the Session. We use postedCustomer because the view is set to that. But we could
            //also change the view to take customerInfo and use customerInfo here.
            step4RegistrationVM.returnedCustomerActivation = new Customer();

            step4RegistrationVM.returnedCustomerActivation.FirstName = ( (RegistrationVM) Session[REGISTRATIONVM]).returnedCustomerActivation.FirstName ;
            step4RegistrationVM.returnedCustomerActivation.LastName = ( (RegistrationVM) Session[REGISTRATIONVM]).returnedCustomerActivation.LastName ;

             step4RegistrationVM.returnedCustomerActivation.PrimaryEmail = ( (RegistrationVM) Session[REGISTRATIONVM]).returnedCustomerActivation.PrimaryEmail ;
            step4RegistrationVM.returnedCustomerActivation.PrimaryPhone = ( (RegistrationVM) Session[REGISTRATIONVM]).returnedCustomerActivation.PrimaryPhone ;


            step4RegistrationVM.returnedCustomerActivation.InvoicePreview = new InvoicePreview();

            step4RegistrationVM.returnedCustomerActivation.InvoicePreview.Subtotal = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.Subtotal;
            step4RegistrationVM.returnedCustomerActivation.InvoicePreview.TotalTaxes = ( (RegistrationVM) Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.TotalTaxes;
            step4RegistrationVM.returnedCustomerActivation.InvoicePreview.Total = ( (RegistrationVM) Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.Total;


            return View(step4RegistrationVM);
        }


    /*
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step5GetPayment")]
        public ActionResult Step5GetPayment()
        {

            RegistrationVM step5RegistrationVM = new RegistrationVM();

            step5RegistrationVM.billingAddress = new Address();
            step5RegistrationVM.billingAddress.AddressType = "Shipping";

            step5RegistrationVM.customerInformation = new Customer();

            step5RegistrationVM.shippingSameAsBilling = (bool)Session[new RegistrationStronglyTypedSessionState().shippingSameAsBilling];

            //  if the shippingSameAsBilling checkbox is checked, we will use the same fields, otherwise, new fields
            if (step5RegistrationVM.shippingSameAsBilling)
            {
                var shipping = (Address)Session[new RegistrationStronglyTypedSessionState().shippingAddress];
                step5RegistrationVM.billingAddress.Line1 = shipping.Line1;
                step5RegistrationVM.billingAddress.Line2 = shipping.Line2;
                step5RegistrationVM.billingAddress.PostalZip = shipping.PostalZip;
                step5RegistrationVM.billingAddress.City = shipping.City;
                step5RegistrationVM.billingAddress.Country = (string)Session[new RegistrationStronglyTypedSessionState().selectedCountryName];
                step5RegistrationVM.billingAddress.State = (string)Session[new RegistrationStronglyTypedSessionState().selectedStateName];
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
                Value = "4242424242424242"
            });
            step5RegistrationVM.listOfCreditCards.Add(new SelectListItem
            {
                Text = "4111111111111111",
                Value = "4111111111111111"
            });

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

            if ((bool)Session[new RegistrationStronglyTypedSessionState().shippingSameAsBilling])
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

                postCreditCard.Address1 = step6RegistrationVM.billingAddress.Line1;
                postCreditCard.Address2 = step6RegistrationVM.billingAddress.Line2;
                postCreditCard.PostalZip = step6RegistrationVM.billingAddress.PostalZip;
                postCreditCard.City = step6RegistrationVM.billingAddress.City;

                postCreditCard.CountryId = step6RegistrationVM.billingAddress.CountryId;
                postCreditCard.StateId = step6RegistrationVM.billingAddress.StateId;
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
                step6RegistrationVM.billingAddress = new Address();
                step6RegistrationVM.billingAddress.AddressType = "Shipping";

                step6RegistrationVM.customerInformation = new Customer();


                if ((bool)Session[new RegistrationStronglyTypedSessionState().shippingSameAsBilling])
                {
                    var shipping = (Address)Session[new RegistrationStronglyTypedSessionState().shippingAddress];
                    step6RegistrationVM.billingAddress.Line1 = shipping.Line1;
                    step6RegistrationVM.billingAddress.Line2 = shipping.Line2;
                    step6RegistrationVM.billingAddress.PostalZip = shipping.PostalZip;
                    step6RegistrationVM.billingAddress.City = shipping.City;
                    step6RegistrationVM.billingAddress.Country = (string)Session[new RegistrationStronglyTypedSessionState().selectedCountryName];
                    step6RegistrationVM.billingAddress.State = (string)Session[new RegistrationStronglyTypedSessionState().selectedStateName];
                    var customer = (Customer)Session[new RegistrationStronglyTypedSessionState().customerInformation];
                    step6RegistrationVM.customerInformation.FirstName = customer.FirstName;
                    step6RegistrationVM.customerInformation.LastName = customer.LastName;
                }

                step6RegistrationVM.listOfCountriesSLI = new List<SelectListItem>();
                step6RegistrationVM.listOfCountriesCountry = new List<Country>();
                step6RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();

                for (int i = 0; i < step6RegistrationVM.listOfCountriesCountry.Count; i++)
                {
                    step6RegistrationVM.listOfCountriesSLI.Add(new SelectListItem
                    {

                        Text = step6RegistrationVM.listOfCountriesCountry[i].Name,
                        Value = step6RegistrationVM.listOfCountriesCountry[i].Id.ToString(),
                        Selected = false
                    });
                }


                step6RegistrationVM.listOfCreditCards = new List<SelectListItem>();


                step6RegistrationVM.listOfCreditCards.Add(new SelectListItem
                {
                    Text = "4242424242424242",
                    Value = "4242424242424242"
                });
                step6RegistrationVM.listOfCreditCards.Add(new SelectListItem
                {
                    Text = "4111111111111111",
                    Value = "4111111111111111"
                });

                step6RegistrationVM.shippingSameAsBilling = (bool)Session[new RegistrationStronglyTypedSessionState().shippingSameAsBilling];
                return View("Step5GetPayment", step6RegistrationVM);
            }
            return View();
        }
       */
         
    }
 }



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
            //This Session will store relevant information pertaining to each plan. When we come to step1, we set its value to null, essentially resetting it
            Session[REGISTRATIONVM] = null;

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
            step2RegistrationVM = registrationVM;
            step2RegistrationVM.AvailableProducts = new List<PlanProduct>();


            /*We will place the "parametered" viewmodel into a session. If this is the first time we visit the page ( step 1 to step 2), the viewmodel's planfrequencyID has a value
            Thus, our session will also contain the planfrequencyID. But if this is our second time visiting the page (step3 to step2), the viewmodel's planfrequencyID is null, and thus
            we do not want to store it.
             * If first visit, store the "parametered" view model, if second visit, don't store it.
            */

            //if our session did not already exist, aka, we came from step1 to step2, we'll make the registration
            if (Session[REGISTRATIONVM] == null)
            {
                Session[REGISTRATIONVM] = registrationVM;
                //we get the planID from the Session because it contains the planID if we proceeded from step1 or step3 whereas the local viewmodal only contains the planID
                //if we proceeded from step1. We store the available products in the Session
                ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts = ApiClient.GetPlanProductsByPlanId(((RegistrationVM)Session[REGISTRATIONVM]).SelectedPlanId, new QueryOptions { }).Results;
            
                //we will be displaying the billing and customer information in the step3 view, on our first visit to step2, we set them to null, on our second visit, we set them equal to
                //the fields in step 3

                ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress = new Address
                {
                   CompanyName = "Hello",
                   Line1 = String.Empty,
                   Line2 = String.Empty,
                   City = String.Empty,
                   PostalZip = String.Empty,

                };

                ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation = new Customer
                {
                    FirstName = String.Empty,
                    LastName = String.Empty,
                    PrimaryEmail = String.Empty,
                    PrimaryPhone = String.Empty
                };

                ((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling = false;

            }
            //if we came from step3, we wish to store the customer information, billing address, and shipping checkbox fields from step 3 into the session
            else
            {
                ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress = step2RegistrationVM.billingAddress;
                ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation = step2RegistrationVM.customerInformation;
                ((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling = step2RegistrationVM.shippingSameAsBilling;
            }

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
            var s = ((RegistrationVM)Session[REGISTRATIONVM]);

                step3RegistrationVM.billingAddress = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress;
                step3RegistrationVM.customerInformation = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation;
                step3RegistrationVM.shippingSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling;
 


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
            if ((registrationVM.customerInformation != null && registrationVM.billingAddress != null))
            {
                ((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling = registrationVM.shippingSameAsBilling;
                ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation = registrationVM.customerInformation;
                ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress = registrationVM.billingAddress;

                //can we do this with automapper?
                //  *****************************************************************************
                //============================== CREATING CUSTOMER ======================================
                //  *****************************************************************************



                Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new ApiWrapper.Dto.Post.Customer
                {
                    FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName,
                    LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName,
                    PrimaryEmail = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.PrimaryEmail,
                    PrimaryPhone = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.PrimaryPhone
                };
                ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer = ApiClient.PostCustomer(postCustomer);


                //  *****************************************************************************
                //============================== CREATING BILLING ADDRESS ======================================
                //  *****************************************************************************

                Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address
                {
                    CompanyName = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CompanyName,
                    Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1,
                    Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2,
                    City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City,
                    PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip,
                    CountryId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CountryId,
                    StateId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.StateId,
                    AddressType = "Billing",
                    CustomerAddressPreferenceId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id
                };
                var returnedBilling = ApiClient.PostAddress(postBillingAddress);

                //given the selected state's and country's IDs, we shall find their respective names, which are shown in the invoice and possibly
                //in step5 if ShippingSameAsBilling has been checked. 
                step4RegistrationVM.listOfCountriesCountry = new List<Country>();
                step4RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();

                for (int i = 0; i < step4RegistrationVM.listOfCountriesCountry.Count; i++)
                {
                    if (step4RegistrationVM.listOfCountriesCountry[i].Id == step4RegistrationVM.billingAddress.CountryId)
                    {
                        //if country ids are identical, store the country name
                        ((RegistrationVM)Session[REGISTRATIONVM]).selectedCountryName = step4RegistrationVM.listOfCountriesCountry[i].Name;

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

                for (int i = 0; i < ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts.Count; i++)
                {
                    var quantity = ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].Quantity;
                    var inclusion = ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].IsIncluded;
                    returnedSubscription.SubscriptionProducts[i].Quantity = quantity;
                    returnedSubscription.SubscriptionProducts[i].IsIncluded = inclusion;
                }

                Automapping.SetupSubscriptionGetToPutMapping();
                var putSubscription = Mapper.Map<Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(returnedSubscription);

                ApiClient.PutSubscription(putSubscription);
            }

            Fusebill.ApiWrapper.Dto.Post.CustomerActivation postCustomerActivation = new ApiWrapper.Dto.Post.CustomerActivation
            {

                ActivateAllSubscriptions = true,
                ActivateAllDraftPurchases = true,
                CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id
            };
            ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation = ApiClient.PostCustomerActivation(postCustomerActivation, true, true);

            //now, we set the properties that will be displayed on the step4 view with the properties from the Session. We use postedCustomer because the view is set to that. But we could
            //also change the view to take customerInfo and use customerInfo here.
            step4RegistrationVM.returnedCustomerActivation = new Customer()
            {
                FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.FirstName,
                LastName = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.LastName,
                PrimaryEmail = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.PrimaryEmail,
                PrimaryPhone = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.PrimaryPhone

            };

            step4RegistrationVM.returnedCustomerActivation.InvoicePreview = new InvoicePreview
            {
                Subtotal = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.Subtotal,
                TotalTaxes = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.TotalTaxes,
                Total = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.Total
            };

            return View(step4RegistrationVM);
        }


        /// <summary>
        /// We obtain credit card and payment information
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step5GetPayment")]
        public ActionResult Step5GetPayment()
        {
            RegistrationVM step5RegistrationVM = new RegistrationVM
            {
                billingAddress = new Address(),
                customerInformation = new Customer()
            };

            step5RegistrationVM.billingAddress.AddressType = "Shipping";


            //step5RegistrationVM.shippingSameAsBilling will be used by the view to determine whether to show text or textboxes
            step5RegistrationVM.shippingSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling;


            //if the user checked the "shipping is same as billing" checkbox in step3, we supply the view's fields with previously entered fields
            //which we had placed in the session
            if (((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling == true)
            {
                step5RegistrationVM.billingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1;
                step5RegistrationVM.billingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2;
                step5RegistrationVM.billingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip;
                step5RegistrationVM.billingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City;
                step5RegistrationVM.billingAddress.Country = ((RegistrationVM)Session[REGISTRATIONVM]).selectedCountryName;
                step5RegistrationVM.billingAddress.State = ((RegistrationVM)Session[REGISTRATIONVM]).selectedStateyName;

                step5RegistrationVM.customerInformation.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName;
                step5RegistrationVM.customerInformation.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName;
            }
            else
            {
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


        /// <summary>
        /// Please note that this method is for demonstration purposes only and is NOT PCI Compliant. For PCI Compliant calls, please view an example on our website
        /// We need the customerId, the cardnumber, the cvv, first and last names, primary and secondary address, postal code, country, state, and city. In our example
        /// because we're using invalid credit cards, we did not include expiration dates.
        /// LIST WEBSITE...............AHHHHHHHHHHHHHHHHHHHHHHHHHHHH!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="registrationVM"></param>
        /// <returns></returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step6GetActivation")]
        public ActionResult Step6GetActivation(RegistrationVM registrationVM)
        {
            //WARNING: IN YOUR WEBSITE, USE PCI COMPLIANT CALLS. AN EXAMPLE IS AVAILABLE ON OUR WEBSITE
            RegistrationVM step6RegistrationVM = new RegistrationVM();
            step6RegistrationVM = registrationVM;

            Fusebill.ApiWrapper.Dto.Post.CreditCard postCreditCard = new ApiWrapper.Dto.Post.CreditCard();

            postCreditCard.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id;
            postCreditCard.CardNumber = step6RegistrationVM.creditCardNumber;
            postCreditCard.Cvv = step6RegistrationVM.cvv;
            postCreditCard.ExpirationMonth = step6RegistrationVM.expirationMonth;
            postCreditCard.ExpirationYear = step6RegistrationVM.expirationYear;

            postCreditCard.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName;
            postCreditCard.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName;

            postCreditCard.Address1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1;
            postCreditCard.Address2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2;
            postCreditCard.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip;
            postCreditCard.City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City;

            postCreditCard.CountryId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CountryId;
            postCreditCard.StateId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.StateId;


            Fusebill.ApiWrapper.Dto.Post.Payment postPayment = new Fusebill.ApiWrapper.Dto.Post.Payment();
            postPayment.Amount = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.Total;
            postPayment.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.Id;



            //if the shipping information is different from the billing information, we  post the shipping information with the fields from the local modal
            //otherwise, the fields come from the session

            Fusebill.ApiWrapper.Dto.Post.Address postShippingAddress = new ApiWrapper.Dto.Post.Address();

            postShippingAddress.AddressType = "Shipping";
            postShippingAddress.CustomerAddressPreferenceId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id;

            if (!((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling)
            {
                postShippingAddress.Line1 = step6RegistrationVM.shippingAddress.Line1;
                postShippingAddress.Line2 = step6RegistrationVM.shippingAddress.Line2;
                postShippingAddress.PostalZip = step6RegistrationVM.shippingAddress.PostalZip;
                postShippingAddress.City = step6RegistrationVM.shippingAddress.City;
                postShippingAddress.CountryId = step6RegistrationVM.shippingAddress.CountryId;
                postShippingAddress.StateId = step6RegistrationVM.shippingAddress.StateId;
            }
            else
            {
                postShippingAddress.CompanyName = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CompanyName;
                postShippingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1;
                postShippingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2;
                postShippingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City;
                postShippingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip;
                postShippingAddress.CountryId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CountryId;
                postShippingAddress.StateId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.StateId;
            }

            ApiClient.PostAddress(postShippingAddress);

            try
            {
                //we try to post the credit card
                ApiClient.PostCreditCard(postCreditCard);

                //after posting the credit card, we post the payment method, which contains the amount to charge the credit card
                ApiClient.PostPayment(postPayment);

                //Activate customer by setting the preview to false

                Fusebill.ApiWrapper.Dto.Post.CustomerActivation postCustomerActivationPreviewFalse = new ApiWrapper.Dto.Post.CustomerActivation();
                postCustomerActivationPreviewFalse.ActivateAllDraftPurchases = true;
                postCustomerActivationPreviewFalse.ActivateAllSubscriptions = true;
                postCustomerActivationPreviewFalse.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id;

                ApiClient.PostCustomerActivation(postCustomerActivationPreviewFalse, false, true);

                //display new view that containa success message
                return View();
            }
            catch
            {
                step6RegistrationVM.billingAddress = new Address();
                step6RegistrationVM.billingAddress.AddressType = "Shipping";

                step6RegistrationVM.customerInformation = new Customer();


                if (((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling == true)
                {
                    step6RegistrationVM.billingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1;
                    step6RegistrationVM.billingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2;
                    step6RegistrationVM.billingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip;
                    step6RegistrationVM.billingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City;
                    step6RegistrationVM.billingAddress.Country = ((RegistrationVM)Session[REGISTRATIONVM]).selectedCountryName;
                    step6RegistrationVM.billingAddress.State = ((RegistrationVM)Session[REGISTRATIONVM]).selectedStateyName;

                    step6RegistrationVM.customerInformation.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName;
                    step6RegistrationVM.customerInformation.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName;
                }
                else
                {

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


                TempData["InvalidCreditCard"] = "Invalid credit card. Please re-enter your credit card information";
                step6RegistrationVM.shippingSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).shippingSameAsBilling;
                return View("Step5GetPayment", step6RegistrationVM);
            }
        }
    }
}



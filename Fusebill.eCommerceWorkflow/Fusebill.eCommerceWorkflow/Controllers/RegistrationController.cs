﻿using AutoMapper;
using Fusebill.ApiWrapper;
using Fusebill.ApiWrapper.Dto.Get;
using Fusebill.eCommerceWorkflow.Common;
using Fusebill.eCommerceWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class RegistrationController : FusebillBaseController
    {
        //session key that stores the viewmodel
        const string RegistrationKey = "RegistrationKey";
        
        //
        // GET: /Registration/
        // First Step in Registration process
        public ActionResult Index()
        {
         
            // Reset session
            Session[RegistrationKey] = null;

            var registrationVm = new RegistrationVM();

            var desiredPlanIds = ConfigurationManager.AppSettings["DesiredPlanIds"];
            var desiredPlans = desiredPlanIds.Split(',');

            registrationVm.AvailablePlans = new List<Plan>();
            registrationVm.AvailablePlanFrequencyIds = new List<long>();

            foreach (string element in desiredPlans)
            {
                var plan = ApiClient.GetPlan(Convert.ToInt64(element));
                registrationVm.AvailablePlans.Add(plan);
                registrationVm.AvailablePlanFrequencyIds.Add(plan.PlanFrequencies[0].Id);
            }
            return View(registrationVm);
        }

        /// <summary>
        /// Display the available products. Editable fields include the checkboxes that represent a product's inclusion
        /// and TextBoxFors that display the number of products to purchase
        /// </summary>
        /// <param name="registrationVM"></param>
        /// <returns></returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step2GetPlanProducts")]
        public ActionResult Step2GetPlanProducts(RegistrationVM registration)
        {
            //KJH: Manilpulate the RegistrationVM (this includes pulling data out of session when we hit back button
            // KJH: Last step is to stuff the session with the registrationVM object
            // KJH: Then return View(registrationVM)
            // KJH: Stuff all, then slot in 

            var session = (RegistrationVM)Session[RegistrationKey];

            switch (registration.Step)
            {
                // We came from Step1
                case (int)FromStep.Step1:

                    //combine into one line?
                    registration.AvailableProducts = new List<PlanProduct>();
                    registration.AvailableProducts = ApiClient.GetPlanProductsByPlanId(registration.SelectedPlanId, new QueryOptions { }).Results;

                    // We don't want to enter the textbox fields every time we demo
                    // registration = RegistrationHelper.SetCustomerAndBillingInformationToDefaults(registration);

                    //instantiate these variables so that Step3's view can place information in the next view model's CustomerInformation and BillingAddress fields
                    registration.CustomerInformation = new Customer();
                    registration.BillingAddress = new Address();
                    break;

                // We came from Step3 repopulate viewmodel
                case (int)FromStep.Step3:

                    registration.AvailableProducts = session.AvailableProducts;
                    registration.SelectedPlanId = session.SelectedPlanId;
                    registration.SelectedPlanFrequencyID = session.SelectedPlanFrequencyID;
                    registration.SelectedCountryName = session.SelectedCountryName;
                    registration.SelectedStateName = session.SelectedStateName;
                    break;

                default:
                    throw new ArgumentException("Invalid Step");
            }

            //If a product's IsOptional value is false, it must be included in the plan and so we set its IsIncluded property to true
            foreach (PlanProduct planProduct in registration.AvailableProducts)
            {
                if (!planProduct.IsOptional)
                {
                    planProduct.IsIncluded = true;
                }
            }

            Session[RegistrationKey] = registration;

            RegistrationVM registrationVM = registration;

            return View(registrationVM);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step3GetCustomerInformation")]
        public ActionResult Step3GetCustomerInformation(RegistrationVM registration)
        {
            var session = (RegistrationVM)Session[RegistrationKey];

            /* If we came from Step2, we will store the values of the Quantity and IsIncluded properties into their respective fields in our Session's AvailableProducts
             * List. (Yes, we seek to modify only the Quantity and IsIncluded properties because the other properties in the local VM are null if we come from Step4. We do not wish to include them.
             * In what ways can we determine ifwe came from Step2 or Step4? 
             * If we came from Step2, the AvailableProducts list of the local VM is not null. If we came from Step4, it is null.
            */

            if (registration.Step == (int)FromStep.Step2)
            {
                for (int i = 0; i < registration.AvailableProducts.Count; i++)
                {
                    session.AvailableProducts[i].Quantity = registration.AvailableProducts[i].Quantity;
                    session.AvailableProducts[i].IsIncluded = registration.AvailableProducts[i].IsIncluded;
                }
            }

            //stuffing everything
            registration.BillingAddress = session.BillingAddress;
            registration.CustomerInformation = session.CustomerInformation;
            registration.SelectedPlanId = session.SelectedPlanId;
            registration.SelectedPlanFrequencyID = session.SelectedPlanFrequencyID;
            registration.AvailableProducts = session.AvailableProducts;
            registration.SelectedCountryName = session.SelectedCountryName;
            registration.SelectedStateName = session.SelectedStateName;

            //Add a list of country for the dropdown menu in Step3's view
            PopulateCountryDropdown(ref registration);

            Session[RegistrationKey] = registration;

            //we instantiate these two objects in order for Step3's view to pass information to Step4's action method
            //    registrationVm.CustomerInformation = new Customer();
            //    registrationVm.BillingAddress = new Address();

            RegistrationVM registrationVM = registration;

            return View(registrationVM);
        }

        /// <summary>
        /// STEP4 TO REFACTOR
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step4GetInvoice")]
        public ActionResult Step4GetInvoice(RegistrationVM registration)
        {

            var session = (RegistrationVM)Session[RegistrationKey];

            /* We wish to store the customer's contact, billing, and value of the "Same as billing" checkbox to our Session. 
             * These fields are populated in the "parametered" VM if we came from Step3 and are null if we came from Step5, in which we seek not to store their values.
             * We also create a new customer and subscription, etc, if we come from Step3. 
             * How do we determine if we come from Step3 or Step5?
             * We came from Step3 if the customerInformation and billingAddress fields and are not null; we came from Step5 if they are null. 
             */

            if (registration.Step == (int)FromStep.Step3)
            {
                session.CustomerInformation = new Customer();
                session.CustomerInformation = registration.CustomerInformation;
                session.BillingAddress = new Address(); 
                session.BillingAddress = registration.BillingAddress;

                CreateCustomer(ref session);
                CreateBillingAddress( session);
                var returnedSubscription = CreateSubscription(session);
                PutSubscription(returnedSubscription, session);

                #region Find customer's country and state name
                //We determine the name of the user's selected state and country based on the ID of the selected state and country, which are shown
                //in Step4's view and Step5's view if "creditCardSameAsBilling" has been checked. 

                registration.ListOfCountriesCountry = new List<Country>();
                registration.ListOfCountriesCountry = ApiClient.GetCountries();

                //We post the customer's activation, which requireds the customerId, which has been stored in our Session
                PostCustomerActivation(preview: true, session: ref session);

                session.SelectedCountryName = (from country in registration.ListOfCountriesCountry
                                                                                  where country.Id == registration.BillingAddress.CountryId
                                                                                  select country.Name).First();

                if (registration.BillingAddress.StateId != null)
                {

                    session.SelectedStateName = (from country in registration.ListOfCountriesCountry
                                                                                    where country.Id == registration.BillingAddress.CountryId
                                                                                    from state in country.States
                                                                                    where state.Id == registration.BillingAddress.StateId
                                                                                    select state.Name).First();
                }
                #endregion
            }


                //stuffing everything
                registration.BillingAddress = session.BillingAddress;
                registration.CustomerInformation = session.CustomerInformation;
                registration.SelectedPlanId = session.SelectedPlanId;
                registration.SelectedPlanFrequencyID = session.SelectedPlanFrequencyID;
                registration.AvailableProducts = session.AvailableProducts;
                registration.SelectedCountryName = session.SelectedCountryName;
                registration.SelectedStateName = session.SelectedStateName;
                registration.ReturnedCustomer = session.ReturnedCustomer;
                registration.ActivatedCustomer = session.ActivatedCustomer;

            //We set the properties that will be displayed in Step4's view with the appropriate properties from the Session. 
            PrepareStep4View(ref registration, session);

            Session[RegistrationKey] = session;

            RegistrationVM registrationVM = registration;

            return View(registrationVM);

        }



        /// <summary>
        /// We obtain credit card and payment information
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step5GetPayment")]
        public ActionResult Step5GetPayment()
        {

            //var a = (RegistrationVM)Session[RegistrationKey];

            var session = ((RegistrationVM)Session[RegistrationKey]);

            var registrationVM = new RegistrationVM
            {
                BillingAddress = new Address(),
                CustomerInformation = new Customer()
            };


        
                PopulateCountryDropdown(ref registrationVM);
                registrationVM.CreditAddress = new Address();
        

            ListOfCreditCards(ref registrationVM);
            ListOfExpirationMonths(ref registrationVM);
            ListOfExpirationYears(ref registrationVM);            

            return View(registrationVM);
        }



        /// <summary>
        /// WARNING: Please note that this method is for demonstration purposes only and is NOT PCI Compliant. For PCI Compliant calls, please view an example on our website:
        /// http://help.fusebill.com/ajax-transparent-redirect-add-credit-card-payment-method
        /// We need the customerId, the cardnumber, the cvv, first and last names, primary and secondary address, postal code, country, state, and city. In our example
        /// because we're using invalid credit cards, we did not include expiration dates.
        /// </summary>
        /// <param name="registrationVM"></param>
        /// <returns></returns>
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step6GetActivation")]
        public ActionResult Step6GetActivation(RegistrationVM registrationVM)
        {
            var session = (RegistrationVM)Session[RegistrationKey];

            try
            {
                var postCreditCard = PostCreditCardAndPayment(registrationVM, session);

                //we try to post the credit card
                var returnedCreditCard = ApiClient.PostCreditCard(postCreditCard);

                var postPayment = PostPayment(returnedCreditCard, session);

                //after posting the credit card, we post the payment method, which contains the amount to charge the credit card
                ApiClient.PostPayment(postPayment);

                //Activate customer by setting the preview to false
                PostCustomerActivation(preview: false, session: ref session );

                //Display the next view, which indicates that the user's transaction was successful
                return View(); 
            }
            catch
            {

             
              
                registrationVM.BillingAddress = new Address();
                registrationVM.CustomerInformation = new Customer();
              

                    PopulateCountryDropdown(ref registrationVM);
                    registrationVM.CreditAddress = new Address();


                ListOfCreditCards(ref registrationVM);
                ListOfExpirationMonths(ref registrationVM);
                ListOfExpirationYears(ref registrationVM);

                TempData["InvalidCreditCard"] = "Invalid credit card. Please re-enter your credit card information";

                return View("Step5GetPayment", registrationVM);
            }
        }



        // ***********************************************************************************
        // ================================ HELPER METHODS ===================================
        // ***********************************************************************************



        private void PrepareStep4View(ref Models.RegistrationVM step4RegistrationVM, RegistrationVM session)
        {
            step4RegistrationVM.ActivatedCustomer = new Customer()
            {
                FirstName = session.ActivatedCustomer.FirstName,
                LastName = session.ActivatedCustomer.LastName,
                PrimaryEmail = session.ActivatedCustomer.PrimaryEmail,
                PrimaryPhone = session.ActivatedCustomer.PrimaryPhone

            };

            step4RegistrationVM.ActivatedCustomer.InvoicePreview = new InvoicePreview
            {
                Subtotal = session.ActivatedCustomer.InvoicePreview.Subtotal,
                TotalTaxes = session.ActivatedCustomer.InvoicePreview.TotalTaxes,
                Total = session.ActivatedCustomer.InvoicePreview.Total
            };
          
            step4RegistrationVM.BillingAddress = new Address
            {
                Line1 = session.BillingAddress.Line1,
                Line2 = session.BillingAddress.Line2,
                PostalZip = session.BillingAddress.PostalZip,
                City = session.BillingAddress.City,
                Country = session.SelectedCountryName,
                State = session.SelectedStateName
            };

        }






        private void CreateCustomer(ref RegistrationVM session)
        {
            Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new ApiWrapper.Dto.Post.Customer
            {
                FirstName = session.CustomerInformation.FirstName,
                LastName = session.CustomerInformation.LastName,
                PrimaryEmail = session.CustomerInformation.PrimaryEmail,
                PrimaryPhone = session.CustomerInformation.PrimaryPhone
            };

            session.ReturnedCustomer = ApiClient.PostCustomer(postCustomer);
        }

        private void CreateBillingAddress(RegistrationVM session)
        {
            Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address
            {
                CompanyName = session.BillingAddress.CompanyName,
                Line1 = session.BillingAddress.Line1,
                Line2 = session.BillingAddress.Line2,
                City = session.BillingAddress.City,
                PostalZip = session.BillingAddress.PostalZip,
                CountryId = session.BillingAddress.CountryId,
                AddressType = "Billing",
                CustomerAddressPreferenceId = session.ReturnedCustomer.Id
            };

            //some countries do not have states
            if (session.BillingAddress.StateId != null)
            {
                postBillingAddress.StateId = session.BillingAddress.StateId;
            }

            ApiClient.PostAddress(postBillingAddress);
        }


        private Subscription CreateSubscription(RegistrationVM session)
        {
            var postSubscription = new ApiWrapper.Dto.Post.Subscription
            {
                CustomerId = session.ReturnedCustomer.Id,
                PlanFrequencyId = session.SelectedPlanFrequencyID
            };

            return ApiClient.PostSubscription(postSubscription);
        }


        private void PutSubscription(Subscription newlyReturnedSubscription, RegistrationVM session)
        {
            for (int i = 0; i < session.AvailableProducts.Count; i++)
            {
                var quantity = session.AvailableProducts[i].Quantity;
                var inclusion = session.AvailableProducts[i].IsIncluded;
                newlyReturnedSubscription.SubscriptionProducts[i].Quantity = quantity;
                newlyReturnedSubscription.SubscriptionProducts[i].IsIncluded = inclusion;
            }

            Automapping.SetupSubscriptionGetToPutMapping();
            var putSubscription = Mapper.Map<Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(newlyReturnedSubscription);

            ApiClient.PutSubscription(putSubscription);
        }



        private void PostCustomerActivation(bool preview, ref RegistrationVM session)
        {
            var postCustomerActivation = new ApiWrapper.Dto.Post.CustomerActivation
            {
                ActivateAllSubscriptions = true,
                ActivateAllDraftPurchases = true,
                CustomerId = session.ReturnedCustomer.Id
            };

            session.ActivatedCustomer = ApiClient.PostCustomerActivation(postCustomerActivation, preview, true);

        }



        private void PopulateCountryDropdown(ref Models.RegistrationVM registrationVM)
        {
            registrationVM.ListOfCountriesSLI = new List<SelectListItem>();
            registrationVM.ListOfCountriesCountry = new List<Country>();
            registrationVM.ListOfCountriesCountry = ApiClient.GetCountries();

            for (int i = 0; i < registrationVM.ListOfCountriesCountry.Count; i++)
            {
                registrationVM.ListOfCountriesSLI.Add(new SelectListItem
                {
                    Text = registrationVM.ListOfCountriesCountry[i].Name,
                    Value = registrationVM.ListOfCountriesCountry[i].Id.ToString(),
                });
            }
        }


        private Fusebill.ApiWrapper.Dto.Post.Payment PostPayment(CreditCard credit, RegistrationVM session)
        {
            var postPayment = new Fusebill.ApiWrapper.Dto.Post.Payment();
            postPayment.Amount = session.ActivatedCustomer.InvoicePreview.Total;
            postPayment.CustomerId = session.ActivatedCustomer.Id;
            postPayment.Source = "Manual";
            postPayment.PaymentMethodType = "CreditCard";
            postPayment.PaymentMethodTypeId = credit.Id;

            return postPayment;
        }

        private Fusebill.ApiWrapper.Dto.Post.CreditCard PostCreditCardAndPayment(Models.RegistrationVM step6RegistrationVM, RegistrationVM session)
        {
            var postCreditCard = new ApiWrapper.Dto.Post.CreditCard();

            postCreditCard.CustomerId = session.ReturnedCustomer.Id;
            postCreditCard.CardNumber = step6RegistrationVM.CreditCardNumber;
            postCreditCard.Cvv = step6RegistrationVM.Cvv;
            postCreditCard.ExpirationMonth = step6RegistrationVM.ExpirationMonth;
            postCreditCard.ExpirationYear = step6RegistrationVM.ExpirationYear;


            postCreditCard.FirstName = session.CustomerInformation.FirstName;
            postCreditCard.LastName = session.CustomerInformation.LastName;


                postCreditCard.Address1 = step6RegistrationVM.CreditAddress.Line1;
                postCreditCard.Address2 = step6RegistrationVM.CreditAddress.Line2;
                postCreditCard.PostalZip = step6RegistrationVM.CreditAddress.PostalZip;
                postCreditCard.City = step6RegistrationVM.CreditAddress.City;

                postCreditCard.CountryId = step6RegistrationVM.CreditAddress.CountryId;
                postCreditCard.StateId = step6RegistrationVM.CreditAddress.StateId;
   

            return postCreditCard;
        }

        private void ListOfCreditCards(ref Models.RegistrationVM registrationVM)
        {
            registrationVM.ListOfCreditCards = new List<SelectListItem>();

            registrationVM.ListOfCreditCards.Add(new SelectListItem
            {
                Text = "4242424242424242",
                Value = "4242424242424242"
            });
            registrationVM.ListOfCreditCards.Add(new SelectListItem
            {
                Text = "4111111111111111",
                Value = "4111111111111111"
            });
        }

        private void ListOfExpirationMonths(ref Models.RegistrationVM registrationVM)
        {
            registrationVM.ListOfExpirationMonths = new List<SelectListItem>();

            for (int i = 1; i < 10; i++)
            {
                registrationVM.ListOfExpirationMonths.Add(new SelectListItem
                {
                    Text = "0" + i.ToString(),
                    Value = "0" + i.ToString(),
                });
            }

            for (int i = 10; i <= 12; i++)
            {
                registrationVM.ListOfExpirationMonths.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                });
            }


        }

        private void ListOfExpirationYears(ref Models.RegistrationVM registrationVM)
        {
            registrationVM.ListOfExpirationYears = new List<SelectListItem>();

            for (int i = 15; i <= 21; i++)
            {
                registrationVM.ListOfExpirationYears.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                });
            }
        }
    }
}






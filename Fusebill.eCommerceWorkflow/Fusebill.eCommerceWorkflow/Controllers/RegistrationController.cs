using AutoMapper;
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

            var registrationVm = new RegistrationVM {CurrentStep = CurrentStep.Step1};

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
        public ActionResult Step2GetPlanProducts(RegistrationVM registrationVM)
        {
            //KJH: Manilpulate the RegistrationVM (this includes pulling data out of session when we hit back button
            // KJH: Last step is to stuff the session with the registrationVM object
            // KJH: Then return View(registrationVM)
            // KJH: Stuff all, then slot in 
            var session = (RegistrationVM) Session[RegistrationKey];

            // We came from Step1
            if (registrationVM.CurrentStep == CurrentStep.Step1) 
            {
                registrationVM.AvailableProducts = ApiClient.GetPlanProductsByPlanId(registrationVM.SelectedPlanId, new QueryOptions { }).Results;

                // We don't want to enter this every time for the demo
                RegistrationHelper.SetCustomerAndBillingInformationToDefaults(registrationVM);
            }

            // We came from Step3 repopulate viewmodel
            else if (registrationVM.CurrentStep == CurrentStep.Step3)
            {
                registrationVM.BillingAddress = session.BillingAddress;
                registrationVM.CustomerInformation = session.CustomerInformation;
                registrationVM.CCAddressSameAsBilling = session.CCAddressSameAsBilling;
                registrationVM.AvailableProducts = session.AvailableProducts;
            }
            else
            {
                throw new ArgumentException("Invalid Step");
            }

            //If a product's IsOptional value is false, it must be included in the plan and so we set its IsIncluded property to true
            foreach (PlanProduct planProduct in registrationVM.AvailableProducts)
            {
                if (!planProduct.IsOptional)
                {
                    planProduct.IsIncluded = true;
                }
            }

            registrationVM.CurrentStep = CurrentStep.Step2;
            Session[RegistrationKey] = registrationVM;

            return View(registrationVM);
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step3GetCustomerInformation")]
        public ActionResult Step3GetCustomerInformation(RegistrationVM registrationVM)
        {

            RegistrationVM registrationVm = new RegistrationVM();
            registrationVm = registrationVM;

            //we instantiate these two objects in order for Step3's view to pass information to Step4's action method
            registrationVm.CustomerInformation = new Customer();
            registrationVm.BillingAddress = new Address();

            /* If we came from Step2, we will store the values of the Quantity and IsIncluded properties into their respective fields in our Session's AvailableProducts
             * List. (Yes, we seek to modify only the Quantity and IsIncluded properties because the other properties in the local VM are null if we come from Step4. We do not wish to include them.
             * In what ways can we determine ifwe came from Step2 or Step4? 
             * If we came from Step2, the AvailableProducts list of the local VM is not null. If we came from Step4, it is null.
            */
            if (registrationVM.AvailableProducts != null)
            {
                //We use a for loop instead of a foreach loop to also loop through the Session's AvailableProducts. We seek to set its Quantity and IsIncluded fields.
                for (int i = 0; i < registrationVM.AvailableProducts.Count; i++)
                {
                    ((RegistrationVM)Session[RegistrationKey]).AvailableProducts[i].Quantity = registrationVM.AvailableProducts[i].Quantity;
                    ((RegistrationVM)Session[RegistrationKey]).AvailableProducts[i].IsIncluded = registrationVM.AvailableProducts[i].IsIncluded;
                }
            }

            registrationVm.BillingAddress = ((RegistrationVM)Session[RegistrationKey]).BillingAddress;
            registrationVm.CustomerInformation = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation;
            registrationVm.CCAddressSameAsBilling = ((RegistrationVM)Session[RegistrationKey]).CCAddressSameAsBilling;

            //Add a list of country for the dropdown menu in Step3's view
            PopulateCountryDropdown(ref registrationVm);

            registrationVM.CurrentStep = CurrentStep.Step3;
            Session[RegistrationKey] = registrationVM;

            return View(registrationVM);
        }



        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step4GetInvoice")]
        public ActionResult Step4GetInvoice(RegistrationVM registrationVM)
        {

            RegistrationVM step4RegistrationVM = new RegistrationVM();
            step4RegistrationVM = registrationVM;

            /* We wish to store the customer's contact, billing, and value of the "Same as billing" checkbox to our Session. 
             * These fields are populated in the "parametered" VM if we came from Step3 and are null if we came from Step5, in which we seek not to store their values.
             * We also create a new customer and subscription, etc, if we come from Step3. 
             * How do we determine if we come from Step3 or Step5?
             * We came from Step3 if the customerInformation and billingAddress fields and are not null; we came from Step5 if they are null. 
             */

            //This if statement is equivalement to  "if (registartionVM.customerInformation!= null){...}" because if registartionVM.customerInformation is null, registrationVM.billingAddress must also be null
            if ((registrationVM.CustomerInformation != null && registrationVM.BillingAddress != null))
            {
                ((RegistrationVM)Session[RegistrationKey]).CCAddressSameAsBilling = registrationVM.CCAddressSameAsBilling;
                ((RegistrationVM)Session[RegistrationKey]).CustomerInformation = registrationVM.CustomerInformation;
                ((RegistrationVM)Session[RegistrationKey]).BillingAddress = registrationVM.BillingAddress;

                CreateCustomer();
                CreateBillingAddress();
                var returnedSubscription = CreateSubscription((RegistrationVM)Session[RegistrationKey]);
                PutSubscription(returnedSubscription);

                #region Find customer's country and state name
                //We determine the name of the user's selected state and country based on the ID of the selected state and country, which are shown
                //in Step4's view and Step5's view if "creditCardSameAsBilling" has been checked. 

                step4RegistrationVM.ListOfCountriesCountry = new List<Country>();
                step4RegistrationVM.ListOfCountriesCountry = ApiClient.GetCountries();

                ((RegistrationVM)Session[RegistrationKey]).SelectedCountryName = (from country in step4RegistrationVM.ListOfCountriesCountry
                                                                                 where country.Id == step4RegistrationVM.BillingAddress.CountryId
                                                                                 select country.Name).First();

                if (step4RegistrationVM.BillingAddress.StateId != null)
                {

                    ((RegistrationVM)Session[RegistrationKey]).SelectedStateName = (from country in step4RegistrationVM.ListOfCountriesCountry
                                                                                   where country.Id == step4RegistrationVM.BillingAddress.CountryId
                                                                                   from state in country.States
                                                                                   where state.Id == step4RegistrationVM.BillingAddress.StateId
                                                                                   select state.Name).First();
                }
                #endregion
            }

            //We post the customer's activation, which requireds the customerId, which has been stored in our Session
            PostCustomerActivation(preview: true);

            //We set the properties that will be displayed in Step4's view with the appropriate properties from the Session. 
            PrepareStep4View(ref step4RegistrationVM);

            registrationVM.CurrentStep = CurrentStep.Step4;
            Session[RegistrationKey] = registrationVM;

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
            var registrationVM = new RegistrationVM
            {
                BillingAddress = new Address(),
                CustomerInformation = new Customer()
            };


            //step5RegistrationVM.creditCardSameAsBilling will be used by the view to determine whether to show text or textboxes
            registrationVM.CCAddressSameAsBilling = ((RegistrationVM)Session[RegistrationKey]).CCAddressSameAsBilling;


            //if the user checked the "shipping is same as billing" checkbox in step3, we supply the view's fields with previously entered fields
            //which we had placed in the session
            if (((RegistrationVM)Session[RegistrationKey]).CCAddressSameAsBilling)
            {
                registrationVM.BillingAddress.Line1 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line1;
                registrationVM.BillingAddress.Line2 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line2;
                registrationVM.BillingAddress.PostalZip = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.PostalZip;
                registrationVM.BillingAddress.City = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.City;
                registrationVM.BillingAddress.Country = ((RegistrationVM)Session[RegistrationKey]).SelectedCountryName;
                registrationVM.BillingAddress.State = ((RegistrationVM)Session[RegistrationKey]).SelectedStateName;

                registrationVM.CustomerInformation.FirstName = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.FirstName;
                registrationVM.CustomerInformation.LastName = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.LastName;
            }
            else
            {
                PopulateCountryDropdown(ref registrationVM);
                registrationVM.CreditAddress = new Address();
            }

            ListOfCreditCards(ref registrationVM);
            ListOfExpirationMonths(ref registrationVM);
            ListOfExpirationYears(ref registrationVM);

            registrationVM.CurrentStep = CurrentStep.Step5;
            Session[RegistrationKey] = registrationVM;

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

            try
            {
                var postCreditCard = PostCreditCardAndPayment(registrationVM);

                //we try to post the credit card
                var returnedCreditCard = ApiClient.PostCreditCard(postCreditCard);

                var postPayment = PostPayment(returnedCreditCard);

                //after posting the credit card, we post the payment method, which contains the amount to charge the credit card
                ApiClient.PostPayment(postPayment);

                //Activate customer by setting the preview to false
                PostCustomerActivation(preview: false);

                registrationVM.CurrentStep = CurrentStep.Completed;
                Session[RegistrationKey] = registrationVM;

                //Display the next view, which indicates that the user's transaction was successful
                return View();
            }
            catch
            {
                registrationVM.BillingAddress = new Address();
                registrationVM.CustomerInformation = new Customer();
                var session = (RegistrationVM)Session[RegistrationKey];

                if (session.CCAddressSameAsBilling)
                {
                    registrationVM.BillingAddress.Line1 = session.BillingAddress.Line1;
                    registrationVM.BillingAddress.Line2 = session.BillingAddress.Line2;
                    registrationVM.BillingAddress.PostalZip = session.BillingAddress.PostalZip;
                    registrationVM.BillingAddress.City = session.BillingAddress.City;
                    registrationVM.BillingAddress.Country = session.SelectedCountryName;
                    registrationVM.BillingAddress.State = session.SelectedStateName;

                    registrationVM.CustomerInformation.FirstName = session.CustomerInformation.FirstName;
                    registrationVM.CustomerInformation.LastName = session.CustomerInformation.LastName;
                }
                else
                {
                    PopulateCountryDropdown(ref registrationVM);
                    registrationVM.CreditAddress = new Address();
                }

                ListOfCreditCards(ref registrationVM);
                ListOfExpirationMonths(ref registrationVM);
                ListOfExpirationYears(ref registrationVM);

                TempData["InvalidCreditCard"] = "Invalid credit card. Please re-enter your credit card information";
                registrationVM.CCAddressSameAsBilling = session.CCAddressSameAsBilling;

                return View("Step5GetPayment", registrationVM);
            }
        }



        // ***********************************************************************************
        // ================================ HELPER METHODS ===================================
        // ***********************************************************************************
        


        private void PrepareStep4View(ref Models.RegistrationVM step4RegistrationVM)
        {
            step4RegistrationVM.ActivatedCustomer = new Customer()
            {
                FirstName = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.FirstName,
                LastName = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.LastName,
                PrimaryEmail = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.PrimaryEmail,
                PrimaryPhone = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.PrimaryPhone

            };

            step4RegistrationVM.ActivatedCustomer.InvoicePreview = new InvoicePreview
            {
                Subtotal = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.InvoicePreview.Subtotal,
                TotalTaxes = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.InvoicePreview.TotalTaxes,
                Total = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.InvoicePreview.Total
            };

            step4RegistrationVM.BillingAddress = new Address
            {
                Line1 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line1,
                Line2 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line2,
                PostalZip = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.PostalZip,
                City = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.City,
                Country = ((RegistrationVM)Session[RegistrationKey]).SelectedCountryName,
                State = ((RegistrationVM)Session[RegistrationKey]).SelectedStateName
            };

        }






        private void CreateCustomer()
        {
            Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new ApiWrapper.Dto.Post.Customer
            {
                FirstName = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.FirstName,
                LastName = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.LastName,
                PrimaryEmail = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.PrimaryEmail,
                PrimaryPhone = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.PrimaryPhone
            };

            ((RegistrationVM)Session[RegistrationKey]).ReturnedCustomer = ApiClient.PostCustomer(postCustomer);
        }

        private void CreateBillingAddress()
        {
            Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address
            {
                CompanyName = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.CompanyName,
                Line1 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line1,
                Line2 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line2,
                City = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.City,
                PostalZip = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.PostalZip,
                CountryId = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.CountryId,
                AddressType = "Billing",
                CustomerAddressPreferenceId = ((RegistrationVM)Session[RegistrationKey]).ReturnedCustomer.Id
            };

            //some countries do not have states
            if (((RegistrationVM)Session[RegistrationKey]).BillingAddress.StateId != null)
            {
                postBillingAddress.StateId = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.StateId;
            }

            ApiClient.PostAddress(postBillingAddress);
        }


        private Subscription CreateSubscription(RegistrationVM registration)
        {
            var postSubscription = new ApiWrapper.Dto.Post.Subscription
            {
                CustomerId = registration.ReturnedCustomer.Id,
                PlanFrequencyId = registration.SelectedPlanFrequencyID
            };

            return ApiClient.PostSubscription(postSubscription);
        }


        private void PutSubscription(Subscription newlyReturnedSubscription)
        {
            for (int i = 0; i < ((RegistrationVM)Session[RegistrationKey]).AvailableProducts.Count; i++)
            {
                var quantity = ((RegistrationVM)Session[RegistrationKey]).AvailableProducts[i].Quantity;
                var inclusion = ((RegistrationVM)Session[RegistrationKey]).AvailableProducts[i].IsIncluded;
                newlyReturnedSubscription.SubscriptionProducts[i].Quantity = quantity;
                newlyReturnedSubscription.SubscriptionProducts[i].IsIncluded = inclusion;
            }

            Automapping.SetupSubscriptionGetToPutMapping();
            var putSubscription = Mapper.Map<Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(newlyReturnedSubscription);

            ApiClient.PutSubscription(putSubscription);
        }



        private void PostCustomerActivation(bool preview)
        {
            var postCustomerActivation = new ApiWrapper.Dto.Post.CustomerActivation
            {
                ActivateAllSubscriptions = true,
                ActivateAllDraftPurchases = true,
                CustomerId = ((RegistrationVM)Session[RegistrationKey]).ReturnedCustomer.Id
            };

            ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer = ApiClient.PostCustomerActivation(postCustomerActivation, preview, true);
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


        private Fusebill.ApiWrapper.Dto.Post.Payment PostPayment(CreditCard credit)
        {
            var postPayment = new Fusebill.ApiWrapper.Dto.Post.Payment();
            postPayment.Amount = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.InvoicePreview.Total;
            postPayment.CustomerId = ((RegistrationVM)Session[RegistrationKey]).ActivatedCustomer.Id;
            postPayment.Source = "Manual";
            postPayment.PaymentMethodType = "CreditCard";
            postPayment.PaymentMethodTypeId = credit.Id;

            return postPayment;
        }

        private Fusebill.ApiWrapper.Dto.Post.CreditCard PostCreditCardAndPayment(Models.RegistrationVM step6RegistrationVM)
        {
            var postCreditCard = new ApiWrapper.Dto.Post.CreditCard();

            postCreditCard.CustomerId = ((RegistrationVM)Session[RegistrationKey]).ReturnedCustomer.Id;
            postCreditCard.CardNumber = step6RegistrationVM.CreditCardNumber;
            postCreditCard.Cvv = step6RegistrationVM.Cvv;
            postCreditCard.ExpirationMonth = step6RegistrationVM.ExpirationMonth;
            postCreditCard.ExpirationYear = step6RegistrationVM.ExpirationYear;


            postCreditCard.FirstName = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.FirstName;
            postCreditCard.LastName = ((RegistrationVM)Session[RegistrationKey]).CustomerInformation.LastName;


            if (((RegistrationVM)Session[RegistrationKey]).CCAddressSameAsBilling == true)
            {
                postCreditCard.Address1 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line1;
                postCreditCard.Address2 = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.Line2;
                postCreditCard.PostalZip = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.PostalZip;
                postCreditCard.City = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.City;

                postCreditCard.CountryId = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.CountryId;
                postCreditCard.StateId = ((RegistrationVM)Session[RegistrationKey]).BillingAddress.StateId;


            }
            else
            {
                postCreditCard.Address1 = step6RegistrationVM.CreditAddress.Line1;
                postCreditCard.Address2 = step6RegistrationVM.CreditAddress.Line2;
                postCreditCard.PostalZip = step6RegistrationVM.CreditAddress.PostalZip;
                postCreditCard.City = step6RegistrationVM.CreditAddress.City;

                postCreditCard.CountryId = step6RegistrationVM.CreditAddress.CountryId;
                postCreditCard.StateId = step6RegistrationVM.CreditAddress.StateId;
            }

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






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
 * NOTE: We use a Session object in this sample to store the user's information. Because a Session times out after 20 minutes, if you project your customers to take
 * more than 20 miunutes to purchase products, we recommend extending the Session's timeout time.
 * 
 * NOTE: Whenever possible, we extract information from the session, because it acts as the "global container" and because it is consistent
 * 
 * */

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class RegistrationController : FusebillController
    {


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
        /// Display the available products. Editable fields include the checkboxes that represent a product's inclusion
        /// and TextBoxFors that display the number of products to pruchase
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


            /*We will place the "parametered" viewmodel into a Session. If we cam from Step1, the viewmodel has a string planfrequencyID, which we store into our Session
             * If we came from Step3  the viewmodel has a null planfrequencyID, which we do not store.
             * we do not want to store it.
            */

            //If came from Step1, our Session is null. If we came from Step3, our Session is not null. 
            if (Session[REGISTRATIONVM] == null)
            {
                Session[REGISTRATIONVM] = registrationVM;

                //We get the planID from the Session because it always contains the PlanID whereas the local viewmodel contains the PlanID only if we came from Step1
                ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts = ApiClient.GetPlanProductsByPlanId(((RegistrationVM)Session[REGISTRATIONVM]).SelectedPlanId, new QueryOptions { }).Results;

                //Since we will be displaying the billing and customer information in the Step3 view (either with strings or empty strings), we initialize
                //their fields here
                SetCustomerAndBillingInformationToEmptyStrings();
            }

            //If we came from Step3, we wish to store the customer information, billing address, and shipping checkbox fields into the Session
            else
            {
                ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress = step2RegistrationVM.billingAddress;
                ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation = step2RegistrationVM.customerInformation;
                ((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling = step2RegistrationVM.creditCardSameAsBilling;
            }

            //we populate the AvailableProducts section of the ViewModal with the AvailableProducts section of the Session
            step2RegistrationVM.AvailableProducts = ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts;


            //If a product's IsOptional value is false, it must be included in the plan and so we set its IsIncluded property to true
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

            //we instantiate these two objects in order for Step3's view to pass information to Step4's action method
            step3RegistrationVM.customerInformation = new Customer();
            step3RegistrationVM.billingAddress = new Address();

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
                    ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].Quantity = registrationVM.AvailableProducts[i].Quantity;
                    ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].IsIncluded = registrationVM.AvailableProducts[i].IsIncluded;
                }
            }

            step3RegistrationVM.billingAddress = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress;
            step3RegistrationVM.customerInformation = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation;
            step3RegistrationVM.creditCardSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling;

            //Add a list of country for the dropdown menu in Step3's view
            PopulateCountryDropdown(ref step3RegistrationVM);

            return View(step3RegistrationVM);
        }



        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Step4GetInvoice")]
        public ActionResult Step4GetInvoice(RegistrationVM registrationVM)
        {

            RegistrationVM step4RegistrationVM = new RegistrationVM();
            step4RegistrationVM = registrationVM;

            step4RegistrationVM.subscription = new Subscription();

            /* We wish to store the customer's contact, billing, and value of the "Same as billing" checkbox to our Session. 
             * These fields are populated in the "parametered" VM if we came from Step3 and are null if we came from Step5, in which we seek not to store their values.
             * We also create a new customer and subscription, etc, if we come from Step3. 
             * How do we determine if we come from Step3 or Step5?
             * We came from Step3 if the customerInformation and billingAddress fields and are not null; we came from Step5 if they are null. 
             */

            //This if statement is equivalement to  "if (registartionVM.customerInformation!= null){...}" because if registartionVM.customerInformation is null, registrationVM.billingAddress must also be null
            if ((registrationVM.customerInformation != null && registrationVM.billingAddress != null))
            {
                ((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling = registrationVM.creditCardSameAsBilling;
                ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation = registrationVM.customerInformation;
                ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress = registrationVM.billingAddress;

                CreateCustomer();
                CreateBillingAddress();
                var returnedSubscription = CreateSubscription();
                PutSubscription(returnedSubscription);

                #region Find customer's country and state name
                //We determine the name of the user's selected state and country based on the ID of the selected state and country, which are shown
                //in Step4's view and Step5's view if "creditCardSameAsBilling" has been checked. 

                step4RegistrationVM.listOfCountriesCountry = new List<Country>();
                step4RegistrationVM.listOfCountriesCountry = ApiClient.GetCountries();

                ((RegistrationVM)Session[REGISTRATIONVM]).selectedCountryName = (from country in step4RegistrationVM.listOfCountriesCountry
                                                                                 where country.Id == step4RegistrationVM.billingAddress.CountryId
                                                                                 select country.Name).First();

                if (step4RegistrationVM.billingAddress.StateId != null)
                {

                    ((RegistrationVM)Session[REGISTRATIONVM]).selectedStateName = (from country in step4RegistrationVM.listOfCountriesCountry
                                                                                   where country.Id == step4RegistrationVM.billingAddress.CountryId
                                                                                   from state in country.States
                                                                                   where state.Id == step4RegistrationVM.billingAddress.StateId
                                                                                   select state.Name).First();
                }
                #endregion
            }

            //We post the customer's activation, which requireds the customerId, which has been stored in our Session
            PostCustomerActivation(preview: true);

            //We set the properties that will be displayed in Step4's view with the appropriate properties from the Session. 
            PrepareStep4View(ref step4RegistrationVM);

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


            //step5RegistrationVM.creditCardSameAsBilling will be used by the view to determine whether to show text or textboxes
            step5RegistrationVM.creditCardSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling;


            //if the user checked the "shipping is same as billing" checkbox in step3, we supply the view's fields with previously entered fields
            //which we had placed in the session
            if (((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling == true)
            {
                step5RegistrationVM.billingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1;
                step5RegistrationVM.billingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2;
                step5RegistrationVM.billingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip;
                step5RegistrationVM.billingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City;
                step5RegistrationVM.billingAddress.Country = ((RegistrationVM)Session[REGISTRATIONVM]).selectedCountryName;
                step5RegistrationVM.billingAddress.State = ((RegistrationVM)Session[REGISTRATIONVM]).selectedStateName;

                step5RegistrationVM.customerInformation.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName;
                step5RegistrationVM.customerInformation.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName;
            }
            else
            {
                PopulateCountryDropdown(ref step5RegistrationVM);
            }

            ListOfCreditCards(ref step5RegistrationVM);
            ListOfExpirationMonths(ref step5RegistrationVM);
            ListOfExpirationYears(ref step5RegistrationVM);

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

            try
            {
                var postCreditCard = PostCreditCardAndPayment(step6RegistrationVM);

                //we try to post the credit card
                var returnedCreditCard = ApiClient.PostCreditCard(postCreditCard);

                var postPayment = PostPayment(returnedCreditCard);

                //after posting the credit card, we post the payment method, which contains the amount to charge the credit card
                ApiClient.PostPayment(postPayment);

                //Activate customer by setting the preview to false
                PostCustomerActivation(preview: false);

                //Display the next view, which indicates that the user's transaction was successful
                return View();
            }
            catch
            {
                step6RegistrationVM.billingAddress = new Address();
                step6RegistrationVM.customerInformation = new Customer();


                if (((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling == true)
                {
                    step6RegistrationVM.billingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1;
                    step6RegistrationVM.billingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2;
                    step6RegistrationVM.billingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip;
                    step6RegistrationVM.billingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City;
                    step6RegistrationVM.billingAddress.Country = ((RegistrationVM)Session[REGISTRATIONVM]).selectedCountryName;
                    step6RegistrationVM.billingAddress.State = ((RegistrationVM)Session[REGISTRATIONVM]).selectedStateName;

                    step6RegistrationVM.customerInformation.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName;
                    step6RegistrationVM.customerInformation.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName;
                }
                else
                {
                    PopulateCountryDropdown(ref step6RegistrationVM);
                }

                ListOfCreditCards(ref step6RegistrationVM);
                ListOfExpirationMonths(ref step6RegistrationVM);
                ListOfExpirationYears(ref step6RegistrationVM);

                TempData["InvalidCreditCard"] = "Invalid credit card. Please re-enter your credit card information";
                step6RegistrationVM.creditCardSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling;

                return View("Step5GetPayment", step6RegistrationVM);
            }
        }



        // ***********************************************************************************
        // ================================ HELPER METHODS ===================================
        // ***********************************************************************************


        //helper method for Step1
        private void SetCustomerAndBillingInformationToEmptyStrings()
        {
            ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress = new Address
            {
                CompanyName = "Hello",
                Line1 = "Hello",
                Line2 = "Hello",
                City = "Hello",
                PostalZip = "Hello",

            };

            ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation = new Customer
            {
                FirstName = "Hello",
                LastName = "Hello",
                PrimaryEmail = "Hello",
                PrimaryPhone = "Hello",
            };

            ((RegistrationVM)Session[REGISTRATIONVM]).creditCardSameAsBilling = false;
        }




        private void PrepareStep4View(ref Models.RegistrationVM step4RegistrationVM)
        {
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

            step4RegistrationVM.billingAddress = new Address
            {
                Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1,
                Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2,
                PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip,
                City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City,
                Country = ((RegistrationVM)Session[REGISTRATIONVM]).selectedCountryName,
                State = ((RegistrationVM)Session[REGISTRATIONVM]).selectedStateName
            };

        }






        private void CreateCustomer()
        {
            Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new ApiWrapper.Dto.Post.Customer
            {
                FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.FirstName,
                LastName = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.LastName,
                PrimaryEmail = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.PrimaryEmail,
                PrimaryPhone = ((RegistrationVM)Session[REGISTRATIONVM]).customerInformation.PrimaryPhone
            };

            ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer = ApiClient.PostCustomer(postCustomer);
        }

        private void CreateBillingAddress()
        {
            Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address
            {
                CompanyName = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CompanyName,
                Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line1,
                Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.Line2,
                City = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.City,
                PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.PostalZip,
                CountryId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.CountryId,
                AddressType = "Billing",
                CustomerAddressPreferenceId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id
            };

            //some countries do not have states
            if (((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.StateId != null)
            {
                postBillingAddress.StateId = ((RegistrationVM)Session[REGISTRATIONVM]).billingAddress.StateId;
            }

            ApiClient.PostAddress(postBillingAddress);
        }



        private Subscription CreateSubscription()
        {
            Fusebill.ApiWrapper.Dto.Post.Subscription postSubscription = new ApiWrapper.Dto.Post.Subscription();
            postSubscription.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id;
            postSubscription.PlanFrequencyId = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedPlanFrequencyID;

            return ApiClient.PostSubscription(postSubscription);
        }


        private void PutSubscription(Subscription newlyReturnedSubscription)
        {
            for (int i = 0; i < ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts.Count; i++)
            {
                var quantity = ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].Quantity;
                var inclusion = ((RegistrationVM)Session[REGISTRATIONVM]).AvailableProducts[i].IsIncluded;
                newlyReturnedSubscription.SubscriptionProducts[i].Quantity = quantity;
                newlyReturnedSubscription.SubscriptionProducts[i].IsIncluded = inclusion;
            }

            Automapping.SetupSubscriptionGetToPutMapping();
            var putSubscription = Mapper.Map<Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>(newlyReturnedSubscription);

            ApiClient.PutSubscription(putSubscription);
        }



        private void PostCustomerActivation(bool preview)
        {
            Fusebill.ApiWrapper.Dto.Post.CustomerActivation postCustomerActivation = new ApiWrapper.Dto.Post.CustomerActivation
            {
                ActivateAllSubscriptions = true,
                ActivateAllDraftPurchases = true,
                CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomer.Id
            };

            ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation = ApiClient.PostCustomerActivation(postCustomerActivation, preview, true);
        }



        private void PopulateCountryDropdown(ref Models.RegistrationVM registrationVM)
        {
            registrationVM.listOfCountriesSLI = new List<SelectListItem>();
            registrationVM.listOfCountriesCountry = new List<Country>();
            registrationVM.listOfCountriesCountry = ApiClient.GetCountries();


            for (int i = 0; i < registrationVM.listOfCountriesCountry.Count; i++)
            {
                registrationVM.listOfCountriesSLI.Add(new SelectListItem
                {
                    Text = registrationVM.listOfCountriesCountry[i].Name,
                    Value = registrationVM.listOfCountriesCountry[i].Id.ToString(),
                });
            }
        }


        private Fusebill.ApiWrapper.Dto.Post.Payment PostPayment(CreditCard credit)
        {
            Fusebill.ApiWrapper.Dto.Post.Payment postPayment = new Fusebill.ApiWrapper.Dto.Post.Payment();
            postPayment.Amount = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.InvoicePreview.Total;
            postPayment.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).returnedCustomerActivation.Id;
            postPayment.Source = "Manual";
            postPayment.PaymentMethodType = "CreditCard";
            postPayment.PaymentMethodTypeId = credit.Id;

            return postPayment;
        }

        private Fusebill.ApiWrapper.Dto.Post.CreditCard PostCreditCardAndPayment(Models.RegistrationVM step6RegistrationVM)
        {
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

            return postCreditCard;
        }

        private void ListOfCreditCards(ref Models.RegistrationVM registrationVM)
        {
            registrationVM.listOfCreditCards = new List<SelectListItem>();

            registrationVM.listOfCreditCards.Add(new SelectListItem
            {
                Text = "4242424242424242",
                Value = "4242424242424242"
            });
            registrationVM.listOfCreditCards.Add(new SelectListItem
            {
                Text = "4111111111111111",
                Value = "4111111111111111"
            });
        }

        private void ListOfExpirationMonths(ref Models.RegistrationVM registrationVM)
        {
            registrationVM.listOfExpirationMonths = new List<SelectListItem>();

            for (int i = 1; i < 10; i++)
            {
                registrationVM.listOfExpirationMonths.Add(new SelectListItem
                {
                    Text = "0" + i.ToString(),
                    Value = "0" + i.ToString(),
                });
            }

            for (int i = 10; i <= 12; i++)
            {
                registrationVM.listOfExpirationMonths.Add(new SelectListItem
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






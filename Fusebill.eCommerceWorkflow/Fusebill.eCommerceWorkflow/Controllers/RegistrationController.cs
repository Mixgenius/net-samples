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
                ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress = step2RegistrationVM.BillingAddress;
                ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation = step2RegistrationVM.CustomerInformation;
                ((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling = step2RegistrationVM.CreditCardSameAsBilling;
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
            step3RegistrationVM.CustomerInformation = new Customer();
            step3RegistrationVM.BillingAddress = new Address();

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

            step3RegistrationVM.BillingAddress = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress;
            step3RegistrationVM.CustomerInformation = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation;
            step3RegistrationVM.CreditCardSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling;

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

            step4RegistrationVM.Subscription = new Subscription();

            /* We wish to store the customer's contact, billing, and value of the "Same as billing" checkbox to our Session. 
             * These fields are populated in the "parametered" VM if we came from Step3 and are null if we came from Step5, in which we seek not to store their values.
             * We also create a new customer and subscription, etc, if we come from Step3. 
             * How do we determine if we come from Step3 or Step5?
             * We came from Step3 if the customerInformation and billingAddress fields and are not null; we came from Step5 if they are null. 
             */

            //This if statement is equivalement to  "if (registartionVM.customerInformation!= null){...}" because if registartionVM.customerInformation is null, registrationVM.billingAddress must also be null
            if ((registrationVM.CustomerInformation != null && registrationVM.BillingAddress != null))
            {
                ((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling = registrationVM.CreditCardSameAsBilling;
                ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation = registrationVM.CustomerInformation;
                ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress = registrationVM.BillingAddress;

                CreateCustomer();
                CreateBillingAddress();
                var returnedSubscription = CreateSubscription();
                PutSubscription(returnedSubscription);

                #region Find customer's country and state name
                //We determine the name of the user's selected state and country based on the ID of the selected state and country, which are shown
                //in Step4's view and Step5's view if "creditCardSameAsBilling" has been checked. 

                step4RegistrationVM.ListOfCountriesCountry = new List<Country>();
                step4RegistrationVM.ListOfCountriesCountry = ApiClient.GetCountries();

                ((RegistrationVM)Session[REGISTRATIONVM]).SelectedCountryName = (from country in step4RegistrationVM.ListOfCountriesCountry
                                                                                 where country.Id == step4RegistrationVM.BillingAddress.CountryId
                                                                                 select country.Name).First();

                if (step4RegistrationVM.BillingAddress.StateId != null)
                {

                    ((RegistrationVM)Session[REGISTRATIONVM]).SelectedStateName = (from country in step4RegistrationVM.ListOfCountriesCountry
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
                BillingAddress = new Address(),
                CustomerInformation = new Customer()
            };


            //step5RegistrationVM.creditCardSameAsBilling will be used by the view to determine whether to show text or textboxes
            step5RegistrationVM.CreditCardSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling;


            //if the user checked the "shipping is same as billing" checkbox in step3, we supply the view's fields with previously entered fields
            //which we had placed in the session
            if (((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling == true)
            {
                step5RegistrationVM.BillingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line1;
                step5RegistrationVM.BillingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line2;
                step5RegistrationVM.BillingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.PostalZip;
                step5RegistrationVM.BillingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.City;
                step5RegistrationVM.BillingAddress.Country = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedCountryName;
                step5RegistrationVM.BillingAddress.State = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedStateName;

                step5RegistrationVM.CustomerInformation.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.FirstName;
                step5RegistrationVM.CustomerInformation.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.LastName;
            }
            else
            {
                PopulateCountryDropdown(ref step5RegistrationVM);
                step5RegistrationVM.CreditAddress = new Address();
            }

            ListOfCreditCards(ref step5RegistrationVM);
            ListOfExpirationMonths(ref step5RegistrationVM);
            ListOfExpirationYears(ref step5RegistrationVM);

            return View(step5RegistrationVM);
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
                step6RegistrationVM.BillingAddress = new Address();
                step6RegistrationVM.CustomerInformation = new Customer();


                if (((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling == true)
                {
                    step6RegistrationVM.BillingAddress.Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line1;
                    step6RegistrationVM.BillingAddress.Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line2;
                    step6RegistrationVM.BillingAddress.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.PostalZip;
                    step6RegistrationVM.BillingAddress.City = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.City;
                    step6RegistrationVM.BillingAddress.Country = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedCountryName;
                    step6RegistrationVM.BillingAddress.State = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedStateName;

                    step6RegistrationVM.CustomerInformation.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.FirstName;
                    step6RegistrationVM.CustomerInformation.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.LastName;
                }
                else
                {
                    PopulateCountryDropdown(ref step6RegistrationVM);
                    step6RegistrationVM.CreditAddress = new Address();
                }

                ListOfCreditCards(ref step6RegistrationVM);
                ListOfExpirationMonths(ref step6RegistrationVM);
                ListOfExpirationYears(ref step6RegistrationVM);

                TempData["InvalidCreditCard"] = "Invalid credit card. Please re-enter your credit card information";
                step6RegistrationVM.CreditCardSameAsBilling = ((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling;

                return View("Step5GetPayment", step6RegistrationVM);
            }
        }



        // ***********************************************************************************
        // ================================ HELPER METHODS ===================================
        // ***********************************************************************************


        //helper method for Step1
        private void SetCustomerAndBillingInformationToEmptyStrings()
        {
            ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress = new Address
            {
                CompanyName = "Hello",
                Line1 = "Hello",
                Line2 = "Hello",
                City = "Hello",
                PostalZip = "Hello",

            };

            ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation = new Customer
            {
                FirstName = "Hello",
                LastName = "Hello",
                PrimaryEmail = "Hello",
                PrimaryPhone = "Hello",
            };

            ((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling = false;
        }




        private void PrepareStep4View(ref Models.RegistrationVM step4RegistrationVM)
        {
            step4RegistrationVM.ReturnedCustomerActivation = new Customer()
            {
                FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.FirstName,
                LastName = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.LastName,
                PrimaryEmail = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.PrimaryEmail,
                PrimaryPhone = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.PrimaryPhone

            };

            step4RegistrationVM.ReturnedCustomerActivation.InvoicePreview = new InvoicePreview
            {
                Subtotal = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.InvoicePreview.Subtotal,
                TotalTaxes = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.InvoicePreview.TotalTaxes,
                Total = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.InvoicePreview.Total
            };

            step4RegistrationVM.BillingAddress = new Address
            {
                Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line1,
                Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line2,
                PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.PostalZip,
                City = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.City,
                Country = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedCountryName,
                State = ((RegistrationVM)Session[REGISTRATIONVM]).SelectedStateName
            };

        }






        private void CreateCustomer()
        {
            Fusebill.ApiWrapper.Dto.Post.Customer postCustomer = new ApiWrapper.Dto.Post.Customer
            {
                FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.FirstName,
                LastName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.LastName,
                PrimaryEmail = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.PrimaryEmail,
                PrimaryPhone = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.PrimaryPhone
            };

            ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomer = ApiClient.PostCustomer(postCustomer);
        }

        private void CreateBillingAddress()
        {
            Fusebill.ApiWrapper.Dto.Post.Address postBillingAddress = new Fusebill.ApiWrapper.Dto.Post.Address
            {
                CompanyName = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.CompanyName,
                Line1 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line1,
                Line2 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line2,
                City = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.City,
                PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.PostalZip,
                CountryId = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.CountryId,
                AddressType = "Billing",
                CustomerAddressPreferenceId = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomer.Id
            };

            //some countries do not have states
            if (((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.StateId != null)
            {
                postBillingAddress.StateId = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.StateId;
            }

            ApiClient.PostAddress(postBillingAddress);
        }



        private Subscription CreateSubscription()
        {
            Fusebill.ApiWrapper.Dto.Post.Subscription postSubscription = new ApiWrapper.Dto.Post.Subscription();
            postSubscription.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomer.Id;
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
                CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomer.Id
            };

            ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation = ApiClient.PostCustomerActivation(postCustomerActivation, preview, true);
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
            Fusebill.ApiWrapper.Dto.Post.Payment postPayment = new Fusebill.ApiWrapper.Dto.Post.Payment();
            postPayment.Amount = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.InvoicePreview.Total;
            postPayment.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomerActivation.Id;
            postPayment.Source = "Manual";
            postPayment.PaymentMethodType = "CreditCard";
            postPayment.PaymentMethodTypeId = credit.Id;

            return postPayment;
        }

        private Fusebill.ApiWrapper.Dto.Post.CreditCard PostCreditCardAndPayment(Models.RegistrationVM step6RegistrationVM)
        {
            Fusebill.ApiWrapper.Dto.Post.CreditCard postCreditCard = new ApiWrapper.Dto.Post.CreditCard();

            postCreditCard.CustomerId = ((RegistrationVM)Session[REGISTRATIONVM]).ReturnedCustomer.Id;
            postCreditCard.CardNumber = step6RegistrationVM.CreditCardNumber;
            postCreditCard.Cvv = step6RegistrationVM.Cvv;
            postCreditCard.ExpirationMonth = step6RegistrationVM.ExpirationMonth;
            postCreditCard.ExpirationYear = step6RegistrationVM.ExpirationYear;


            postCreditCard.FirstName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.FirstName;
            postCreditCard.LastName = ((RegistrationVM)Session[REGISTRATIONVM]).CustomerInformation.LastName;


            if (((RegistrationVM)Session[REGISTRATIONVM]).CreditCardSameAsBilling == true)
            {
                postCreditCard.Address1 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line1;
                postCreditCard.Address2 = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.Line2;
                postCreditCard.PostalZip = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.PostalZip;
                postCreditCard.City = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.City;

                postCreditCard.CountryId = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.CountryId;
                postCreditCard.StateId = ((RegistrationVM)Session[REGISTRATIONVM]).BillingAddress.StateId;

                
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






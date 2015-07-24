using Fusebill.ApiWrapper.Contracts;
using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;

namespace Fusebill.ApiWrapper
{
    public class Client : IClient
    {
        #region Members
        protected readonly IExecuteHttpRequest ExecuteHttpRequest;
        protected readonly IParseHttpResponse ParseHttpResponse;
        protected readonly IRestUriBuilder RestUriBuilder;
        #endregion

        #region Properties

        private bool AllowForError { get; set; }

        public string SystemSource
        {
            set { ExecuteHttpRequest.SystemSource = value; }

        }

        public long LoggedInUserId
        {
            set
            {
                ExecuteHttpRequest.LoggedInUserId = value;
            }
        }

        public string ApiKey
        {
            protected get
            {
                return ExecuteHttpRequest.ApiKey;
            }
            set
            {
                ExecuteHttpRequest.ApiKey = value;
            }
        }

        public DateTime? DateForTesting
        {
            protected get
            {
                return ExecuteHttpRequest.DateForTesting;
            }
            set
            {
                ExecuteHttpRequest.DateForTesting = value;
            }
        }

        #endregion

        #region Constructors
        public Client(IExecuteHttpRequest executeHttpRequest, IParseHttpResponse parseHttpResponse, IRestUriBuilder restUriBuilder)
        {
            ExecuteHttpRequest = executeHttpRequest;
            ParseHttpResponse = parseHttpResponse;
            RestUriBuilder = restUriBuilder;

            ExecuteHttpRequest.ApiKey = string.Empty;
        }

        #endregion

        #region Protected Methods
        protected ResultList<T> GetEntities<T>(string url, string acceptType = "application/json")
        {
            var resultList = new ResultList<T>();

            var response = ExecuteHttpRequest.ExecuteHttpGet(url, acceptType);

            resultList.Results = ParseHttpResponse.GetEntities<T>(response.Content.ReadAsStringAsync().Result);
            resultList.PagingHeaderData = ParseHttpResponse.GetHeaderData(response.Headers);

            return resultList;
        }

        protected List<T> GetIds<T>(string url)
        {
            var response = ExecuteHttpRequest.ExecuteHttpGet(url);

            var resultList = ParseHttpResponse.GetEntities<T>(response.Content.ReadAsStringAsync().Result);

            return resultList;
        }

        protected List<T> GetEntityList<T>(string url, string acceptType = "application/json")
        {
            var response = ExecuteHttpRequest.ExecuteHttpGet(url, acceptType);
            return ParseHttpResponse.GetEntity<List<T>>(response.Content.ReadAsStringAsync().Result);
        }

        protected T GetEntity<T>(string url, string acceptType = "application/json")
        {
            var response = ExecuteHttpRequest.ExecuteHttpGet(url, acceptType);
            return ParseHttpResponse.GetEntity<T>(response.Content.ReadAsStringAsync().Result);
        }

        protected string GetString(string url, string acceptType = "application/json", int timeout = 60)
        {
            var response = ExecuteHttpRequest.ExecuteHttpGet(url, acceptType, timeout);
            return response.Content.ReadAsStringAsync().Result;
        }


        public Subscription PutSubscription(Fusebill.ApiWrapper.Dto.Put.Subscription subscription)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return PutEntity<Fusebill.ApiWrapper.Dto.Put.Subscription, Subscription>(url, subscription);
        }

        protected TU PutEntity<T, TU>(string url, T entity)
        {
            var response = ExecuteHttpRequest.ExecuteHttpPut(url, entity);
            return ParseHttpResponse.GetEntity<TU>(response.Content.ReadAsStringAsync().Result);
        }


        public Customer PostCustomer(Fusebill.ApiWrapper.Dto.Post.Customer customer)
        {
            var url = RestUriBuilder.BuildUri("customers");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Customer, Customer>(url, customer);
        }

        public Address PostAddress(Fusebill.ApiWrapper.Dto.Post.Address address)
        {
            var url = RestUriBuilder.BuildUri("addresses");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Address, Address>(url, address);
        }

        public Subscription PostSubscription(Fusebill.ApiWrapper.Dto.Post.Subscription subscription)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Subscription, Subscription>(url, subscription);
        }

        public Subscription GetSubscription(long id)
        {
            var url = RestUriBuilder.BuildUri("subscriptions", id);
            return GetEntity<Subscription>(url);
        }

        public Customer PostCustomerActivation(Fusebill.ApiWrapper.Dto.Post.CustomerActivation customerActivation, bool preview, bool showZeroDollarCharges)
        {
            var url = RestUriBuilder.BuildUri("CustomerActivation") + "?preview=" + preview + "&showZeroDollarCharges=" + showZeroDollarCharges;
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.CustomerActivation, Customer>(url, customerActivation);
        }

        public void DeleteSubscription(long id, bool allowForError = false)
        {
            AllowForError = allowForError;

            var url = RestUriBuilder.BuildUri("subscriptions", id);
            DeleteEntity(url);
        }

        public Payment PostPayment(Fusebill.ApiWrapper.Dto.Post.Payment payment)
        {
            var url = RestUriBuilder.BuildUri("payments");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Payment, Payment>(url, payment);
        }

        public CreditCard PostCreditCard(Fusebill.ApiWrapper.Dto.Post.CreditCard paymentMethod)
        {
            var url = RestUriBuilder.BuildUri("paymentmethods");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.CreditCard, CreditCard>(url, paymentMethod);
        }
      
        protected TU PostEntity<T, TU>(string url, T entity, string acceptType = "application/json", int timeout = 60)
        {
            var response = ExecuteHttpRequest.ExecuteHttpPost(url, entity, acceptType, timeout);

            return ParseHttpResponse.GetEntity<TU>(response.Content.ReadAsStringAsync().Result);
        }

        protected byte[] GetBytes(string url)
        {
            var response = ExecuteHttpRequest.ExecuteHttpGet(url);
            return response.Content.ReadAsByteArrayAsync().Result;
        }

        protected void DeleteEntity(string url, long id)
        {
            DeleteEntity(url + "/" + id);
        }

        protected void DeleteEntity(string url)
        {
            ExecuteHttpRequest.ExecuteHttpDelete(url);
        }

        #endregion

        #region Public Methods

        public List<Country> GetCountries()
        {
            var url = RestUriBuilder.BuildUri("countries");
            return GetEntityList<Country>(url);
        }

        public ResultList<Plan> GetPlans(QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("Plans", queryOptions);
            return GetEntities<Plan>(url);
        }

        public ResultList<Plan> GetActivePlans(QueryOptions queryOptions)
        {
            queryOptions.Query = "Status:Active";
            var url = RestUriBuilder.BuildUri("Plans", queryOptions);
            return GetEntities<Plan>(url);
        }

        public Plan GetPlan(long id)
        {
            var url = RestUriBuilder.BuildUri("plans", id);
            return GetEntity<Plan>(url);
        }

        public ResultList<CustomerSummary> GetCustomers(QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("customerSummary", queryOptions);
            return GetEntities<CustomerSummary>(url);
        }

        public Customer GetCustomer(long id)
        {
            var url = RestUriBuilder.BuildUri("customers", id);
            return GetEntity<Customer>(url);
        }

        public Subscription PostSubscriptionActivation(Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation subscriptionActivation, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false)
        {
            var url = RestUriBuilder.BuildUri("subscriptionActivation", subscriptionActivation.Id) + string.Format("?preview={0}&showZeroDollarCharges={1}&temporarilyDisableAutoPost={2}", preview, showZeroDollarCharges, temporarilyDisableAutoPost);
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation, Subscription>(url, subscriptionActivation);
        }

        public Subscription PostSubscriptionProvision(Fusebill.ApiWrapper.Dto.Post.SubscriptionProvision subscriptionProvision, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false)
        {
            var url = RestUriBuilder.BuildUri("subscriptionProvision", subscriptionProvision.Id) + string.Format("?preview={0}&showZeroDollarCharges={1}&temporarilyDisableAutoPost={2}", preview, showZeroDollarCharges, temporarilyDisableAutoPost);
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.SubscriptionProvision, Subscription>(url, subscriptionProvision);
        }

        public ResultList<PlanProduct> GetPlanProductsByPlanId(long id, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("plans", id, "planProducts", queryOptions);
            return GetEntities<PlanProduct>(url);

        }

        public void DeleteSubscription(long id)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            DeleteEntity(url, id);
        }



        public Subscription PostSubscriptionCancel(Fusebill.ApiWrapper.Dto.Post.SubscriptionCancel subscriptionCancel)
        {
            var url = RestUriBuilder.BuildUri("subscriptionCancellation", subscriptionCancel.SubscriptionId);
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.SubscriptionCancel, Subscription>(url, subscriptionCancel);
        }

        public ResultList<Subscription> GetSubscriptions(long customerId, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("customers", customerId, "subscriptions", queryOptions);
            return GetEntities<Subscription>(url);
        }

        public Customer PostCustomerCancel(Fusebill.ApiWrapper.Dto.Post.CustomerCancel customer)
        {
            var url = RestUriBuilder.BuildUri("customerCancellation");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.CustomerCancel, Customer>(url, customer);
        }

        public ReverseCharge PostReverseCharge(Fusebill.ApiWrapper.Dto.Post.ReverseCharge reverseCharge)
        {
            var url = RestUriBuilder.BuildUri("reverseCharges");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.ReverseCharge, ReverseCharge>(url, reverseCharge);
        }

        public ResultList<Invoice> GetInvoicesByCustomerId(long customerId, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("customers", customerId, "invoices", queryOptions);
            return GetEntities<Invoice>(url);
        }

        public Payment PostRefund(Fusebill.ApiWrapper.Dto.Post.Refund model)
        {
            var url = RestUriBuilder.BuildUri("refunds");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Refund, Payment>(url, model);
        }


        #endregion
    }
}

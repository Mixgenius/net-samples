using Fusebill.ApiWrapper.Contracts;
using Fusebill.ApiWrapper.Dto.Get;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        protected async Task<ResultList<T>> GetEntities<T>(string url, string acceptType = "application/json")
        {
            var resultList = new ResultList<T>();

            var response = await ExecuteHttpRequest.ExecuteHttpGet(url, acceptType);

            resultList.Results = ParseHttpResponse.GetEntities<T>(await response.Content.ReadAsStringAsync());
            resultList.PagingHeaderData = ParseHttpResponse.GetHeaderData(response.Headers);

            return resultList;
        }

        protected async Task<List<T>> GetIds<T>(string url)
        {
            var response = await ExecuteHttpRequest.ExecuteHttpGet(url);

            var resultList = ParseHttpResponse.GetEntities<T>(await response.Content.ReadAsStringAsync());

            return resultList;
        }

        protected async Task<List<T>> GetEntityList<T>(string url, string acceptType = "application/json")
        {
            var response = await ExecuteHttpRequest.ExecuteHttpGet(url, acceptType);
            return ParseHttpResponse.GetEntity<List<T>>(await response.Content.ReadAsStringAsync());
        }

        protected async Task<T> GetEntity<T>(string url, string acceptType = "application/json")
        {
            var response = await ExecuteHttpRequest.ExecuteHttpGet(url, acceptType);
            return ParseHttpResponse.GetEntity<T>(await response.Content.ReadAsStringAsync());
        }

        protected async Task<string> GetString(string url, string acceptType = "application/json", int timeout = 60)
        {
            var response = await ExecuteHttpRequest.ExecuteHttpGet(url, acceptType, timeout);
            return await response.Content.ReadAsStringAsync();
        }


        public Task<Subscription> PutSubscription(Fusebill.ApiWrapper.Dto.Put.Subscription subscription)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return PutEntity<Fusebill.ApiWrapper.Dto.Put.Subscription, Subscription>(url, subscription);
        }

        protected async Task<TU> PutEntity<T, TU>(string url, T entity)
        {
            var response = await ExecuteHttpRequest.ExecuteHttpPut(url, entity);
            return ParseHttpResponse.GetEntity<TU>(await response.Content.ReadAsStringAsync());
        }

        public Task<Customer> PostCustomer(Fusebill.ApiWrapper.Dto.Post.Customer customer)
        {
            var url = RestUriBuilder.BuildUri("customers");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Customer, Customer>(url, customer);
        }

        public Task<Customer> PutCustomer(Fusebill.ApiWrapper.Dto.Put.Customer customer)
        {
            var url = RestUriBuilder.BuildUri("customers");
            return PutEntity<Fusebill.ApiWrapper.Dto.Put.Customer, Customer>(url, customer);
        }

        public Task<Address> PostAddress(Fusebill.ApiWrapper.Dto.Post.Address address)
        {
            var url = RestUriBuilder.BuildUri("addresses");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Address, Address>(url, address);
        }

        public Task<Subscription> PostSubscription(Fusebill.ApiWrapper.Dto.Post.Subscription subscription)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Subscription, Subscription>(url, subscription);
        }

        public Task<Subscription> GetSubscription(long id)
        {
            var url = RestUriBuilder.BuildUri("subscriptions", id);
            return GetEntity<Subscription>(url);
        }

        public Task<Customer> PostCustomerActivation(Fusebill.ApiWrapper.Dto.Post.CustomerActivation customerActivation, bool preview, bool showZeroDollarCharges)
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

        public Task<Payment> PostPayment(Fusebill.ApiWrapper.Dto.Post.Payment payment)
        {
            var url = RestUriBuilder.BuildUri("payments");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Payment, Payment>(url, payment);
        }

        public Task<CreditCard> PostCreditCard(Fusebill.ApiWrapper.Dto.Post.CreditCard paymentMethod)
        {
            var url = RestUriBuilder.BuildUri("paymentmethods");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.CreditCard, CreditCard>(url, paymentMethod);
        }
      
        protected async Task<TU> PostEntity<T, TU>(string url, T entity, string acceptType = "application/json", int timeout = 60)
        {
            var response = await ExecuteHttpRequest.ExecuteHttpPost(url, entity, acceptType, timeout);

            return ParseHttpResponse.GetEntity<TU>(await response.Content.ReadAsStringAsync());
        }

        protected async Task<TU> PatchEntity<T, TU>(string url, T entity, string acceptType = "application/json")
        {
            var response = await ExecuteHttpRequest.ExecuteHttpPatch(url, entity, acceptType);

            return ParseHttpResponse.GetEntity<TU>(await response.Content.ReadAsStringAsync());
        }

        protected async Task<byte[]> GetBytes(string url)
        {
            var response = await ExecuteHttpRequest.ExecuteHttpGet(url);
            return await response.Content.ReadAsByteArrayAsync();
        }

        protected Task DeleteEntity(string url, long id)
        {
            return DeleteEntity(url + "/" + id);
        }

        protected Task DeleteEntity(string url)
        {
            return ExecuteHttpRequest.ExecuteHttpDelete(url);
        }

        #endregion

        #region Public Methods

        public Task<List<Country>> GetCountries()
        {
            var url = RestUriBuilder.BuildUri("countries");
            return GetEntityList<Country>(url);
        }

        public Task<ResultList<Plan>> GetPlans(QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("Plans", queryOptions);
            return GetEntities<Plan>(url);
        }

        public Task<ResultList<Plan>> GetActivePlans(QueryOptions queryOptions)
        {
            queryOptions.Query = "Status:Active";
            var url = RestUriBuilder.BuildUri("Plans", queryOptions);
            return GetEntities<Plan>(url);
        }

        public Task<Plan> GetPlan(long id)
        {
            var url = RestUriBuilder.BuildUri("plans", id);
            return GetEntity<Plan>(url);
        }

        public Task<ResultList<CustomerSummary>> GetCustomers(QueryOptions queryOptions)
        {
            //var url = RestUriBuilder.BuildUri("customerSummary", queryOptions);
            var url = RestUriBuilder.BuildUri("customers", queryOptions);
            return GetEntities<CustomerSummary>(url);
        }

        public Task<Customer> GetCustomer(long id)
        {
            var url = RestUriBuilder.BuildUri("customers", id);
            return GetEntity<Customer>(url);
        }

        public Task<Subscription> PostSubscriptionActivation(Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation subscriptionActivation, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false)
        {
            var url = RestUriBuilder.BuildUri("subscriptionActivation", subscriptionActivation.Id) + string.Format("?preview={0}&showZeroDollarCharges={1}&temporarilyDisableAutoPost={2}", preview, showZeroDollarCharges, temporarilyDisableAutoPost);
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation, Subscription>(url, subscriptionActivation);
        }

        public Task<Subscription> PostSubscriptionProvision(Fusebill.ApiWrapper.Dto.Post.SubscriptionProvision subscriptionProvision, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false)
        {
            var url = RestUriBuilder.BuildUri("subscriptionProvision", subscriptionProvision.Id) + string.Format("?preview={0}&showZeroDollarCharges={1}&temporarilyDisableAutoPost={2}", preview, showZeroDollarCharges, temporarilyDisableAutoPost);
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.SubscriptionProvision, Subscription>(url, subscriptionProvision);
        }

        public Task<ResultList<PlanProduct>> GetPlanProductsByPlanId(long id, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("plans", id, "planProducts", queryOptions);
            return GetEntities<PlanProduct>(url);

        }

        public Task DeleteSubscription(long id)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return DeleteEntity(url, id);
        }



        public Task<Subscription> PostSubscriptionCancel(Fusebill.ApiWrapper.Dto.Post.SubscriptionCancel subscriptionCancel)
        {
            var url = RestUriBuilder.BuildUri("subscriptionCancellation", subscriptionCancel.SubscriptionId);
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.SubscriptionCancel, Subscription>(url, subscriptionCancel);
        }

        public Task<ResultList<Subscription>> GetSubscriptions(long customerId, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("customers", customerId, "subscriptions", queryOptions);
            return GetEntities<Subscription>(url);
        }

        public Task<Customer> PostCustomerCancel(Fusebill.ApiWrapper.Dto.Post.CustomerCancel customer)
        {
            var url = RestUriBuilder.BuildUri("customerCancellation");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.CustomerCancel, Customer>(url, customer);
        }

        public Task<ReverseCharge> PostReverseCharge(Fusebill.ApiWrapper.Dto.Post.ReverseCharge reverseCharge)
        {
            var url = RestUriBuilder.BuildUri("reverseCharges");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.ReverseCharge, ReverseCharge>(url, reverseCharge);
        }

        public Task<ResultList<Invoice>> GetInvoicesByCustomerId(long customerId, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("customers", customerId, "invoices", queryOptions);
            return GetEntities<Invoice>(url);
        }

        public Task<Payment> PostRefund(Fusebill.ApiWrapper.Dto.Post.Refund model)
        {
            var url = RestUriBuilder.BuildUri("refunds");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Refund, Payment>(url, model);
        }


        public Task<ResultList<Product>> GetProducts(QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("Products", queryOptions);
            return GetEntities<Product>(url);
        }
        public Task<Product> GetProduct(long id)
        {
            var url = RestUriBuilder.BuildUri("Products", id);
            return GetEntity<Product>(url);
        }

        public Task<Dto.Get.Purchase> PostPurchase(Dto.Post.Purchase purchase)
        {
            var url = RestUriBuilder.BuildUri("purchases");
            return PostEntity<Fusebill.ApiWrapper.Dto.Post.Purchase, Purchase>(url, purchase);
        }

        public Task<Dto.Patch.SubscriptionProductItems> PatchSubscriptionProductItems(Dto.Patch.SubscriptionProductItems items)
        {
            var url = RestUriBuilder.BuildUri("subscriptionProductItems");
            return PatchEntity<Fusebill.ApiWrapper.Dto.Patch.SubscriptionProductItems, Dto.Patch.SubscriptionProductItems>(url, items);
        }
        #endregion
    }
}

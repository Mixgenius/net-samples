using Fusebill.ApiWrapper.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Get = Fusebill.ApiWrapper.Dto.Get;
using Patch = Fusebill.ApiWrapper.Dto.Patch;
using Post = Fusebill.ApiWrapper.Dto.Post;
using Put = Fusebill.ApiWrapper.Dto.Put;

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


        public Task<Get.Subscription> PutSubscription(Fusebill.ApiWrapper.Dto.Put.Subscription subscription)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return PutEntity<Put.Subscription, Get.Subscription>(url, subscription);
        }

        protected async Task<TU> PutEntity<T, TU>(string url, T entity)
        {
            var response = await ExecuteHttpRequest.ExecuteHttpPut(url, entity);
            return ParseHttpResponse.GetEntity<TU>(await response.Content.ReadAsStringAsync());
        }

        public Task<Get.Customer> PostCustomer(Post.Customer customer)
        {
            var url = RestUriBuilder.BuildUri("customers");
            return PostEntity<Post.Customer, Get.Customer>(url, customer);
        }

        public Task<Get.Customer> PutCustomer(Fusebill.ApiWrapper.Dto.Put.Customer customer)
        {
            var url = RestUriBuilder.BuildUri("customers");
            return PutEntity<Put.Customer, Get.Customer>(url, customer);
        }

        public Task<Get.Address> PostAddress(Post.Address address)
        {
            var url = RestUriBuilder.BuildUri("addresses");
            return PostEntity<Post.Address, Get.Address>(url, address);
        }

        public Task<Get.Subscription> PostSubscription(Post.Subscription subscription)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return PostEntity<Post.Subscription, Get.Subscription>(url, subscription);
        }

        public Task<Get.Subscription> GetSubscription(long id)
        {
            var url = RestUriBuilder.BuildUri("subscriptions", id);
            return GetEntity<Get.Subscription>(url);
        }

        public Task<Get.Customer> PostCustomerActivation(Post.CustomerActivation customerActivation, bool preview, bool showZeroDollarCharges)
        {
            var url = RestUriBuilder.BuildUri("CustomerActivation") + "?preview=" + preview + "&showZeroDollarCharges=" + showZeroDollarCharges;
            return PostEntity<Post.CustomerActivation, Get.Customer>(url, customerActivation);
        }

        public void DeleteSubscription(long id, bool allowForError = false)
        {
            AllowForError = allowForError;

            var url = RestUriBuilder.BuildUri("subscriptions", id);
            DeleteEntity(url);
        }

        public Task<Get.Payment> PostPayment(Post.Payment payment)
        {
            var url = RestUriBuilder.BuildUri("payments");
            return PostEntity<Post.Payment, Get.Payment>(url, payment);
        }

        public Task<Get.CreditCard> PostCreditCard(Post.CreditCard paymentMethod)
        {
            var url = RestUriBuilder.BuildUri("paymentmethods");
            return PostEntity<Post.CreditCard, Get.CreditCard>(url, paymentMethod);
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

        public Task<List<Get.Country>> GetCountries()
        {
            var url = RestUriBuilder.BuildUri("countries");
            return GetEntityList<Get.Country>(url);
        }

        public Task<ResultList<Get.Plan>> GetPlans(QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("Plans", queryOptions);
            return GetEntities<Get.Plan>(url);
        }

        public Task<ResultList<Get.Plan>> GetActivePlans(QueryOptions queryOptions)
        {
            queryOptions.Query = "Status:Active";
            var url = RestUriBuilder.BuildUri("Plans", queryOptions);
            return GetEntities<Get.Plan>(url);
        }

        public Task<Get.Plan> GetPlan(long id)
        {
            var url = RestUriBuilder.BuildUri("plans", id);
            return GetEntity<Get.Plan>(url);
        }

        public Task<ResultList<Get.CustomerSummary>> GetCustomers(QueryOptions queryOptions)
        {
            //var url = RestUriBuilder.BuildUri("customerSummary", queryOptions);
            var url = RestUriBuilder.BuildUri("customers", queryOptions);
            return GetEntities<Get.CustomerSummary>(url);
        }

        public Task<Get.Customer> GetCustomer(long id)
        {
            var url = RestUriBuilder.BuildUri("customers", id);
            return GetEntity<Get.Customer>(url);
        }

        public Task<Get.Subscription> PostSubscriptionActivation(Post.SubscriptionActivation subscriptionActivation, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false)
        {
            var url = RestUriBuilder.BuildUri("subscriptionActivation", subscriptionActivation.Id) + string.Format("?preview={0}&showZeroDollarCharges={1}&temporarilyDisableAutoPost={2}", preview, showZeroDollarCharges, temporarilyDisableAutoPost);
            return PostEntity<Post.SubscriptionActivation, Get.Subscription>(url, subscriptionActivation);
        }

        public Task<Get.Subscription> PostSubscriptionProvision(Post.SubscriptionProvision subscriptionProvision, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false)
        {
            var url = RestUriBuilder.BuildUri("subscriptionProvision", subscriptionProvision.Id) + string.Format("?preview={0}&showZeroDollarCharges={1}&temporarilyDisableAutoPost={2}", preview, showZeroDollarCharges, temporarilyDisableAutoPost);
            return PostEntity<Post.SubscriptionProvision, Get.Subscription>(url, subscriptionProvision);
        }

        public Task<ResultList<Get.PlanProduct>> GetPlanProductsByPlanId(long id, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("plans", id, "planProducts", queryOptions);
            return GetEntities<Get.PlanProduct>(url);

        }

        public Task DeleteSubscription(long id)
        {
            var url = RestUriBuilder.BuildUri("subscriptions");
            return DeleteEntity(url, id);
        }



        public Task<Get.Subscription> PostSubscriptionCancel(Post.SubscriptionCancel subscriptionCancel)
        {
            var url = RestUriBuilder.BuildUri("subscriptionCancellation", subscriptionCancel.SubscriptionId);
            return PostEntity<Post.SubscriptionCancel, Get.Subscription>(url, subscriptionCancel);
        }

        public Task<ResultList<Get.Subscription>> GetSubscriptions(long customerId, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("customers", customerId, "subscriptions", queryOptions);
            return GetEntities<Get.Subscription>(url);
        }

        public Task<Get.Customer> PostCustomerCancel(Post.CustomerCancel customer)
        {
            var url = RestUriBuilder.BuildUri("customerCancellation");
            return PostEntity<Post.CustomerCancel, Get.Customer>(url, customer);
        }

        public Task<Get.ReverseCharge> PostReverseCharge(Post.ReverseCharge reverseCharge)
        {
            var url = RestUriBuilder.BuildUri("reverseCharges");
            return PostEntity<Post.ReverseCharge, Get.ReverseCharge>(url, reverseCharge);
        }

        public Task<ResultList<Get.Invoice>> GetInvoicesByCustomerId(long customerId, QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("customers", customerId, "invoices", queryOptions);
            return GetEntities<Get.Invoice>(url);
        }

        public Task<Get.Payment> PostRefund(Post.Refund model)
        {
            var url = RestUriBuilder.BuildUri("refunds");
            return PostEntity<Post.Refund, Get.Payment>(url, model);
        }


        public Task<ResultList<Get.Product>> GetProducts(QueryOptions queryOptions)
        {
            var url = RestUriBuilder.BuildUri("Products", queryOptions);
            return GetEntities<Get.Product>(url);
        }
        public Task<Get.Product> GetProduct(long id)
        {
            var url = RestUriBuilder.BuildUri("Products", id);
            return GetEntity<Get.Product>(url);
        }

        public Task<Get.Purchase> PostPurchase(Dto.Post.Purchase purchase)
        {
            var url = RestUriBuilder.BuildUri("purchases");
            return PostEntity<Post.Purchase, Get.Purchase>(url, purchase);
        }

        public Task<Patch.SubscriptionProductItems> PatchSubscriptionProductItems(Dto.Patch.SubscriptionProductItems items)
        {
            var url = RestUriBuilder.BuildUri("subscriptionProductItems");
            return PatchEntity<Patch.SubscriptionProductItems, Dto.Patch.SubscriptionProductItems>(url, items);
        }
        #endregion
    }
}

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

        protected byte[] GetBytes(string url)
        {
            var response = ExecuteHttpRequest.ExecuteHttpGet(url);
            return response.Content.ReadAsByteArrayAsync().Result;
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

        //public ResultList<CustomerSummary> GetCustomers(QueryOptions queryOptions, CustomerAdvancedSearch advancedSearch)
        //{
        //    var url = RestUriBuilder.BuildUri("customerSummary", 0, "search", queryOptions, advancedSearch);
        //    return GetEntities<CustomerSummary>(url);
        //}

        #endregion
    }
}

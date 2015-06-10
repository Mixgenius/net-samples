using Fusebill.ApiWrapper;
using System.Configuration;
using System.Web.Mvc;
using Fusebill.ApiWrapper.Contracts;
using Fusebill.eCommerceWorkflow.Common;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    /// <summary>
    /// Derive your controller from this class to inherit the API client construction
    /// </summary>
    public class FusebillBaseController : Controller
    {
        protected IClient ApiClient;

        public FusebillBaseController()
        {
            IExecuteHttpRequest executeHttpRequest = new ExecuteHttpRequest(new NoLog(), "eCommerceWorkflow");
            IParseHttpResponse parseHttpResponse = new ParseHttpResponse(new JsonDeserializer());
            IRestUriBuilder restUriBuilder = new RestUriBuilder(ConfigurationManager.AppSettings["SecureUri"]);
            ApiClient = new Client(executeHttpRequest, parseHttpResponse, restUriBuilder);
            // Contact Fusebill to get your API Key
            ApiClient.ApiKey = string.Format("{0}", ConfigurationManager.AppSettings["AccountKey"]);
        }

    }
}

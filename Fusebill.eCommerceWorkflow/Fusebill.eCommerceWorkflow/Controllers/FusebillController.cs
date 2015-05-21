using Fusebill.ApiWrapper;
using System.Configuration;
using System.Web.Mvc;
using Fusebill.eCommerceWorkflow.Common;

namespace Fusebill.eCommerceWorkflow.Controllers
{
    public class FusebillController : Controller
    {
        protected IClient ApiClient;


        public FusebillController()
        {
            IExecuteHttpRequest executeHttpRequest = new ExecuteHttpRequest(new NoLog(), "eCommerceWorkflow");
            IParseHttpResponse parseHttpResponse = new ParseHttpResponse(new JsonDeserializer());
            IRestUriBuilder restUriBuilder = new RestUriBuilder(ConfigurationManager.AppSettings["SecureUri"]);
            ApiClient = new Client(executeHttpRequest, parseHttpResponse, restUriBuilder);
            ApiClient.ApiKey = string.Format("{0}", ConfigurationManager.AppSettings["AccountKey"]);
        }

    }
}

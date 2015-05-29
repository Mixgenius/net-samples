using System;
using System.Configuration;

namespace Model
{
    public class HostedPageDomainToUrl
    {
        public static string ToUrl(HostedPageDomain hostedPageDomain)
        {
            string url = "";

            switch (hostedPageDomain)
            {
                case HostedPageDomain.MyBillSystem:
                    url = "mybillsystem.com";
                    break;
                
                default:
                    throw new NotImplementedException(string.Format("No URL provided for HostedPageDomain: {0}", hostedPageDomain));
            }

            return ConfigurationManager.AppSettings["EnvironmentPrefix"] + url;
        }
    }
}

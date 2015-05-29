using System;
using System.Collections.Generic;
using Fusebill.ApiWrapper.Dto.Get;

namespace Fusebill.ApiWrapper
{
    public interface IClient
    {
        string SystemSource { set; }

        long LoggedInUserId { set; }

        string ApiKey { set; }

        DateTime? DateForTesting { set; }

        ResultList<Plan> GetPlans(QueryOptions queryOptions);
        ResultList<Plan> GetActivePlans(QueryOptions queryOptions);


     //   Customer PostCustomer(Customer customer);

        ResultList<PlanProduct> GetPlanProductsByPlanId(long id, QueryOptions queryOptions);


        Plan GetPlan(long id);
        List<Country> GetCountries();
        ResultList<CustomerSummary> GetCustomers(QueryOptions queryOptions);
        Customer GetCustomer(long id);
    }
}

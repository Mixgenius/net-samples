using System;
using System.Collections.Generic;
using Fusebill.ApiWrapper.Dto.Get;

namespace Fusebill.ApiWrapper.Contracts
{
    public interface IClient
    {
        string SystemSource { set; }

        long LoggedInUserId { set; }

        string ApiKey { set; }

        DateTime? DateForTesting { set; }
        ResultList<Subscription> GetSubscriptions(long customerId, QueryOptions queryOptions);

        ResultList<Plan> GetPlans(QueryOptions queryOptions);
        ResultList<Plan> GetActivePlans(QueryOptions queryOptions);
        CreditCard PostCreditCard(Fusebill.ApiWrapper.Dto.Post.CreditCard paymentMethod);

        Subscription PutSubscription(Fusebill.ApiWrapper.Dto.Put.Subscription model);
        Subscription PostSubscriptionActivation(Fusebill.ApiWrapper.Dto.Post.SubscriptionActivation subscriptionActivation, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false);

        Customer PostCustomer(Fusebill.ApiWrapper.Dto.Post.Customer customer);
        Address PostAddress(Fusebill.ApiWrapper.Dto.Post.Address model);
        Subscription PostSubscription(Fusebill.ApiWrapper.Dto.Post.Subscription subscription);
        Customer PostCustomerActivation(Fusebill.ApiWrapper.Dto.Post.CustomerActivation customerActivation, bool preview, bool showZeroDollarCharges);

        Payment PostPayment(Fusebill.ApiWrapper.Dto.Post.Payment payment);

        ResultList<PlanProduct> GetPlanProductsByPlanId(long id, QueryOptions queryOptions);
        Subscription GetSubscription(long id);

        Subscription PostSubscriptionProvision(Fusebill.ApiWrapper.Dto.Post.SubscriptionProvision subscriptionProvision, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false);

        Plan GetPlan(long id);
        List<Country> GetCountries();
        ResultList<CustomerSummary> GetCustomers(QueryOptions queryOptions);
        Customer GetCustomer(long id);
        void DeleteSubscription(long id);
        Subscription PostSubscriptionCancel(Fusebill.ApiWrapper.Dto.Post.SubscriptionCancel subscription);


    }
}

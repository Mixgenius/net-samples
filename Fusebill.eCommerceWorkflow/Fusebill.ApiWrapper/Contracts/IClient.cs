using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Get = Fusebill.ApiWrapper.Dto.Get;
using Patch = Fusebill.ApiWrapper.Dto.Patch;
using Post = Fusebill.ApiWrapper.Dto.Post;
using Put = Fusebill.ApiWrapper.Dto.Put;

namespace Fusebill.ApiWrapper.Contracts
{
    public interface IClient
    {
        string SystemSource { set; }

        long LoggedInUserId { set; }

        string ApiKey { set; }

        DateTime? DateForTesting { set; }
        Task<ResultList<Get.Subscription>> GetSubscriptions(long customerId, QueryOptions queryOptions);
        Task<Get.Customer> PostCustomerCancel(Post.CustomerCancel customer);
        Task<ResultList<Get.Plan>> GetPlans(QueryOptions queryOptions);
        Task<ResultList<Get.Plan>> GetActivePlans(QueryOptions queryOptions);
        Task<Get.CreditCard> PostCreditCard(Post.CreditCard paymentMethod);
        Task<Get.CreditCard> GetCreditCard(long paymentMethodId);
        Task<Get.Subscription> PutSubscription(Put.Subscription model);
        Task<Get.Subscription> PostSubscriptionActivation(Post.SubscriptionActivation subscriptionActivation, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false);
        Task<Get.Customer> PostCustomer(Post.Customer customer);
        Task<Get.Customer> PutCustomer(Dto.Put.Customer putCustomer);
        Task<Get.Address> PostAddress(Post.Address model);
        Task<Get.Subscription> PostSubscription(Post.Subscription subscription);
        Task<Get.Customer> PostCustomerActivation(Post.CustomerActivation customerActivation, bool preview, bool showZeroDollarCharges);
        Task<Get.Payment> PostPayment(Post.Payment payment);
        Task<ResultList<Get.PlanProduct>> GetPlanProductsByPlanId(long id, QueryOptions queryOptions);
        Task<Get.Subscription> GetSubscription(long id);
        Task<Get.Subscription> PostSubscriptionProvision(Post.SubscriptionProvision subscriptionProvision, bool preview = false, bool showZeroDollarCharges = true, bool temporarilyDisableAutoPost = false);
        Task<Get.Plan> GetPlan(long id);
        Task<List<Get.Country>> GetCountries();
        Task<ResultList<Get.CustomerSummary>> GetCustomers(QueryOptions queryOptions);
        Task<Get.Customer> GetCustomer(long id);
        Task<Get.Subscription> PostSubscriptionCancel(Post.SubscriptionCancel subscription);
        Task<Get.ReverseCharge> PostReverseCharge(Post.ReverseCharge reverseCharge);
        Task<ResultList<Get.Invoice>> GetInvoicesByCustomerId(long customerId, QueryOptions queryOptions);
        Task DeleteSubscription(long id);
        Task<Get.Payment> PostRefund(Post.Refund model);

        Task<ResultList<Get.Product>> GetProducts(QueryOptions queryOptions);
        Task<Get.Product> GetProduct(long id);

        Task<Get.Purchase> PostPurchase(Post.Purchase purchase);

        Task<Patch.SubscriptionProductItems> PatchSubscriptionProductItems(Dto.Patch.SubscriptionProductItems productItems);
    }
}

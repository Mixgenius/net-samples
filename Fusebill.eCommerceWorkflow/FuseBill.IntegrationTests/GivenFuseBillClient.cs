using AutoMapper;
using FluentAssertions;
using Fusebill.ApiWrapper;
using Fusebill.ApiWrapper.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Patch = Fusebill.ApiWrapper.Dto.Patch;
using Post = Fusebill.ApiWrapper.Dto.Post;
using Put = Fusebill.ApiWrapper.Dto.Put;

namespace FuseBillCore.IntegrationTests
{
    public class GivenFusebillClient
    {
        private const long ALC_WAV = 54803;
        private const long ADVANCED = 24489;
        private const long ADVANCED_MONTHLY = 70632;
        private const long PRO_MONTHLY = 70634;
        private const long CUSTOMER1 = 3427018;
        private const long CUSTOMER2 = 3428642; //has invoices
        private const long SUBSCRIPTION1 = 792678;
        private const long INVALID_ID = 2427018;

        #region Helpers
        private static IClient GetClient()
        {
            var executeHttpRequest = new ExecuteHttpRequest(Mock.Of<ILogger<ExecuteHttpRequest>>(), "eCommerceWorkflow");
            var parseHttpResponse = new ParseHttpResponse(new JsonDeserializer());
            var restUriBuilder = new RestUriBuilder("https://secure.fusebill.com/v1");
            var client = new Client(executeHttpRequest, parseHttpResponse, restUriBuilder)
            {
                ApiKey = "MDpOMjc5UWhGYTY3MTBqYXJpMzI4WlF5YU1WRXUyenNQNmVQV05QRzZTb002UHl0STZnNGxTR0VlMlNVcjlCNlVV"
            };

            return client;
        }

        private static IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                Automapping.SetupSubscriptionGetToPutMapping(cfg);
            });

            var mapper = mappingConfig.CreateMapper();
            return mapper;
        }
        #endregion

        [Fact]
        public async Task WhenGettingCountries_exists()
        {
            var client = GetClient();
            var countries = await client.GetCountries();
            countries.Should().NotBeEmpty();
        }

        #region Plans
        [Fact]
        public async Task WhenGettingPlans_exists()
        {
            var client = GetClient();
            var plans = await client.GetPlans(new QueryOptions());
            plans.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WhenGettingActivePlans_exists()
        {
            var client = GetClient();
            var plans = await client.GetActivePlans(new QueryOptions());
            plans.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WhenGettingPlan_exists()
        {
            var client = GetClient();
            var plans = await client.GetPlan(ADVANCED);
            plans.Should().NotBeNull();
        }

        [Fact]
        public void WhenGettingPlan_throwNotFound()
        {
            var client = GetClient();
            client
                .Awaiting(c => c.GetPlan(INVALID_ID))
                .ShouldThrow<ApiClientException>().WithMessage($"Plan with id {INVALID_ID} not found.");
        }

        [Fact]
        public async Task WhenGettingPlanProducts_exists()
        {
            var client = GetClient();
            var products = await client.GetPlanProductsByPlanId(ADVANCED, new QueryOptions());
            products.Results.Should().NotBeEmpty();
        }
        #endregion

        #region Products
        [Fact]
        public async Task WhenGettingProducts_exists()
        {
            var client = GetClient();
            var products = await client.GetProducts(new QueryOptions());
            products.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WhenGettingProduct_exists()
        {
            var client = GetClient();
            var products = await client.GetProduct(ALC_WAV);
            products.Should().NotBeNull();
        }

        [Fact]
        public void WhenGettingProduct_throwNotFound()
        {
            var client = GetClient();
            client
                .Awaiting(c => c.GetProduct(INVALID_ID))
                .ShouldThrow<ApiClientException>().WithMessage($"Product with id {INVALID_ID} not found.");
        }
        #endregion

        #region Customers
        [Fact]
        public async Task WhenGettingCustomers_exists()
        {
            var client = GetClient();
            var customers = await client.GetCustomers(new QueryOptions());
            customers.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WhenGettingCustomer_exists()
        {
            var client = GetClient();
            var customer = await client.GetCustomer(CUSTOMER1);
            customer.Should().NotBeNull();
        }

        [Fact]
        public void WhenGettingCustomer_throwNotFound()
        {
            var client = GetClient();
            client
                .Awaiting(c => c.GetCustomer(INVALID_ID))
                .ShouldThrow<ApiClientException>().WithMessage($"Customer with id {INVALID_ID} not found.");
        }

        [Fact]
        public async Task WhenGettingCustomerSubscriptions_exists()
        {
            var client = GetClient();
            var subscriptions = await client.GetSubscriptions(CUSTOMER1, new QueryOptions());
            subscriptions.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WhenGettingInvoicesByCustomer_exists()
        {
            var client = GetClient();
            var invoices = await client.GetInvoicesByCustomerId(CUSTOMER1, new QueryOptions());
            invoices.Results.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WhenPostingCustomer_isCreated()
        {
            var client = GetClient();
            var customer = await client.PostCustomer(new Post.Customer
            {
                FirstName = "Integration",
                LastName = "Test",
            });
            customer.Should().NotBeNull();
        }

        [Fact]
        public async Task WhenPuttingCustomer_isUpdated()
        {
            var client = GetClient();
            var newCustomer = await client.PostCustomer(new Post.Customer
            {
                FirstName = "Integration",
                LastName = "Test"
            });

            newCustomer.Status.Should().Be("Draft");

            var mapper = GetMapper();

            var putCustomer = mapper.Map<Put.Customer>(newCustomer);

            putCustomer.Status = "Active";
            putCustomer.LastName = "Updated";

            var updatedCustomer = await client.PutCustomer(putCustomer);

            updatedCustomer.Should().NotBeNull();
            updatedCustomer.Status.Should().Be("Active");
            updatedCustomer.LastName.Should().Be("Updated");
        }
        #endregion

        #region Subscriptions
        [Fact]
        public async Task WhenGettingSubscriptions_exists()
        {
            var client = GetClient();
            var subscription = await client.GetSubscription(SUBSCRIPTION1);
            subscription.Should().NotBeNull();
        }

        [Fact]
        public void WhenGettingSubscriptions_throwNotFound()
        {
            var client = GetClient();
            client
                .Awaiting(c => c.GetSubscription(INVALID_ID))
                .ShouldThrow<ApiClientException>().WithMessage($"Subscription with id {INVALID_ID} not found.");
        }

        [Fact]
        public async Task WhenPostingSubscription_isCreated()
        {
            var client = GetClient();
            var subscription = await client.PostSubscription(new Post.Subscription
            {
                CustomerId = CUSTOMER1,
                PlanFrequencyId = ADVANCED_MONTHLY
            });
            subscription.Should().NotBeNull();

        }

        [Fact]
        public async Task WhenPuttingSubscription_isUpdated()
        {
            var client = GetClient();
            var newSubscription = await client.PostSubscription(new Post.Subscription
            {
                CustomerId = CUSTOMER1,
                PlanFrequencyId = ADVANCED_MONTHLY
            });

            newSubscription.Status.Should().Be("Draft");

            var mapper = GetMapper();

            var putSubscription = mapper.Map<Put.Subscription>(newSubscription);

            putSubscription.Status = "Active";
            var updatedSubscription = await client.PutSubscription(putSubscription);

            updatedSubscription.Should().NotBeNull();
            updatedSubscription.Status.Should().Be("Active");
        }

        [Fact]
        public async Task WhenUpgradingSubscription_is()
        {
            var client = GetClient();

            //new customer
            var newCustomer = await client.PostCustomer(new Post.Customer
            {
                FirstName = "Integration",
                LastName = "Test",
            });

            //activate
            var updatedCustomer = await client.PostCustomerActivation(new Post.CustomerActivation
            {
                 CustomerId = newCustomer.Id
            }, false, false);

            //subscribe advanced
            var newAdvancedSubscription = await client.PostSubscription(new Post.Subscription
            {
                CustomerId = newCustomer.Id,
                PlanFrequencyId = ADVANCED_MONTHLY,
            });

            var ccard = await client.PostCreditCard(new Post.CreditCard
            {
                CustomerId = newCustomer.Id,
                Address1 = "test",
                CardNumber = "4111111111111111",
                Cvv = "123",
                ExpirationMonth = 11,
                ExpirationYear = 20,
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName
            });

            await client.PostPayment(new Post.Payment
            {
                CustomerId = newCustomer.Id,
                Amount = 14,
                Source = "Import",
                PaymentMethodType = "CreditCard",
                PaymentMethodTypeId = ccard.Id
            });

            var updatedSubscription = await client.PostSubscriptionActivation(new Post.SubscriptionActivation
            {
                Id = newAdvancedSubscription.Id
            });

            //cancel before upgrade.
            await client.PostSubscriptionCancel(new Post.SubscriptionCancel
            {
                SubscriptionId = updatedSubscription.Id,
                CancellationOption = "Unearned" // prorated
            });

            //upgrade to pro
            var newProSubscription = await client.PostSubscription(new Post.Subscription
            {
                CustomerId = newCustomer.Id,
                PlanFrequencyId = PRO_MONTHLY
            });

            updatedSubscription = await client.PostSubscriptionActivation(new Post.SubscriptionActivation
            {
                Id = newProSubscription.Id
            });

            updatedSubscription.Status.Should().Be("Active");
        }        

        [Fact]
        public async Task WhenPatchingSubscriptionProductItems_isUpdated()
        {
            var client = GetClient();
            var subscription = await client.GetSubscription(SUBSCRIPTION1);

            var subscriptionProduct = subscription.SubscriptionProducts.Single(p => p.PlanProduct.ProductId == ALC_WAV);
            var index = subscriptionProduct.Quantity + 1;
            var update = await client.PatchSubscriptionProductItems(new Patch.SubscriptionProductItems
            {
                SubscriptionId = subscription.Id,
                SubscriptionProducts = new System.Collections.Generic.List<Patch.SubscriptionProduct>
                {
                     new Patch.SubscriptionProduct
                     {
                         SubscriptionProductId = subscriptionProduct.Id,
                         SubscriptionProductItems = new System.Collections.Generic.List<Patch.SubscriptionProductItem>
                         {
                             new Patch.SubscriptionProductItem
                             {
                                 Operation = "Insert",
                                 Reference = $"test{index}",
                                 Description = $"test{index}",
                                 Name = $"test{index}"
                             }
                         }
                     }
                }
            });

            update.Should().NotBeNull();

            subscription = await client.GetSubscription(SUBSCRIPTION1);
            subscriptionProduct = subscription.SubscriptionProducts.Single(p => p.PlanProduct.ProductId == ALC_WAV);
            subscriptionProduct.Quantity.Should().Be(index);
            subscriptionProduct.PlanProduct.OrderToCashCycles.Single().PricingModel.QuantityRanges.Single().Prices.Single().Amount.Should().Be(6.99m); //because is advanced
        }
        #endregion

        #region Purchases
        [Fact]
        public async Task WhenPostingPurchase_isCreated()
        {
            var client = GetClient();
            var purchase = await client.PostPurchase(new Post.Purchase
            {
                CustomerId = CUSTOMER1,
                ProductId = ALC_WAV,
                Name = "test"
            });
            purchase.Should().NotBeNull();
            purchase.PriceRanges.Single().Amount.Should().Be(9.99m); //because purchase is outside of plan
        }
        #endregion

        //
        [Fact]
        public async Task WhenGettingCustomerInvoices_exists()
        {
            var client = GetClient();
            var invoices = await client.GetInvoicesByCustomerId(CUSTOMER2, new QueryOptions());
            invoices.Results.Should().NotBeEmpty();
        }
    }
}

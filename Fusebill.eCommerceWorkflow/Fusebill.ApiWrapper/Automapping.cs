namespace Fusebill.ApiWrapper
{
    public static class Automapping
    {
        /*
        public static void DefineGetToPutAutomapping()
        {
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.AccountBillingStatementPreference, Fusebill.ApiWrapper.Dto.Put.AccountBillingStatementPreference>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.Address, Fusebill.ApiWrapper.Dto.Put.Address>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.AvalaraConfiguration, Fusebill.ApiWrapper.Dto.Put.AvalaraConfiguration>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.AvalaraAddress, Fusebill.ApiWrapper.Dto.Put.AvalaraAddress>();

            AutoMapper.Mapper.CreateMap<Common.Dto.Get.BillingPeriodConfiguration, Fusebill.ApiWrapper.Dto.Put.BillingPeriodConfiguration>();

            AutoMapper.Mapper.CreateMap<Common.Dto.Get.Customer, Fusebill.ApiWrapper.Dto.Put.Customer>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.CustomerAcquisition, Fusebill.ApiWrapper.Dto.Put.CustomerAcquisition>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.CustomerAddressPreference, Fusebill.ApiWrapper.Dto.Put.CustomerAddressPreference>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.CustomerBillingSetting, Fusebill.ApiWrapper.Dto.Put.CustomerBillingSetting>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.CustomerBillingStatementSetting, Fusebill.ApiWrapper.Dto.Put.CustomerBillingStatementSetting>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.CustomerReference, Fusebill.ApiWrapper.Dto.Put.CustomerReference>();
            AutoMapper.Mapper.CreateMap<Common.Dto.Get.CustomerSalesTrackingCode, Fusebill.ApiWrapper.Dto.Put.CustomerSalesTrackingCode>();

            AutoMapper.Mapper.CreateMap<Common.Dto.Get.TrackedItemDisplay, Fusebill.ApiWrapper.Dto.Put.TrackedItemDisplay>();
        }

         * */
        public static void SetupSubscriptionGetToPutMapping()
        {
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProduct, Fusebill.ApiWrapper.Dto.Put.SubscriptionProduct>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductDiscount, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductDiscount>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionOverride>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductOverride>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductPriceOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductPriceOverride>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductEarningTiming, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductEarningTiming>();

      //      AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.PlanProduct, Fusebill.ApiWrapper.Dto.Put.PlanProduct>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.PlanOrderToCashCycle, Fusebill.ApiWrapper.Dto.Put.PlanOrderToCashCycle>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.PlanFrequency, Fusebill.ApiWrapper.Dto.Put.PlanFrequency>();
        }
    }
}
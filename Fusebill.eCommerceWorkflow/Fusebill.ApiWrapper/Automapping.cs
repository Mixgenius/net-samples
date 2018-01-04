using AutoMapper;

namespace Fusebill.ApiWrapper
{
    public static class Automapping
    {
        public static void SetupSubscriptionGetToPutMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>();
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProduct, Fusebill.ApiWrapper.Dto.Put.SubscriptionProduct>();
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductDiscount, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductDiscount>();
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionOverride>();
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductOverride>();
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductPriceOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductPriceOverride>();
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductEarningTiming, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductEarningTiming>();
            
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.PlanOrderToCashCycle, Fusebill.ApiWrapper.Dto.Put.PlanOrderToCashCycle>();
            cfg.CreateMap<Fusebill.ApiWrapper.Dto.Get.PlanFrequency, Fusebill.ApiWrapper.Dto.Put.PlanFrequency>();
        }
    }
}
namespace Fusebill.ApiWrapper
{
    public static class Automapping
    {
        public static void SetupSubscriptionGetToPutMapping()
        {
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.Subscription, Fusebill.ApiWrapper.Dto.Put.Subscription>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProduct, Fusebill.ApiWrapper.Dto.Put.SubscriptionProduct>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductDiscount, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductDiscount>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionOverride>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductOverride>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductPriceOverride, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductPriceOverride>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.SubscriptionProductEarningTiming, Fusebill.ApiWrapper.Dto.Put.SubscriptionProductEarningTiming>();

            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.PlanOrderToCashCycle, Fusebill.ApiWrapper.Dto.Put.PlanOrderToCashCycle>();
            AutoMapper.Mapper.CreateMap<Fusebill.ApiWrapper.Dto.Get.PlanFrequency, Fusebill.ApiWrapper.Dto.Put.PlanFrequency>();
        }
    }
}
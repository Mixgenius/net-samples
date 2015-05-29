using System;
using System.Collections.Generic;
using Model.Builders;
using Model.Builders.Implementation;

namespace Model.Factories.Implementation
{
    public class ProductTypeBuilders
    {
        private static Dictionary<ProductType, ProductTypeBuilder> builders;

        public static Dictionary<ProductType, ProductTypeBuilder> Builders()
        {
            if (null == builders)
            {
                builders = new Dictionary<ProductType, ProductTypeBuilder>();
                builders.Add(ProductType.PhysicalGood, new PhysicalGoodBuilder());
                builders.Add(ProductType.PlanSetupFee, new PhysicalGoodBuilder());
                builders.Add(ProductType.RecurringService, new RecurringServiceBuilder());
                builders.Add(ProductType.PlanCharge, new RecurringServiceBuilder());
                builders.Add(ProductType.OneTimeCharge, new OneTimeChargeBuilder());
            }

            return builders;
        }
    }
}

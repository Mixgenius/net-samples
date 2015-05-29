namespace Model.FactoryMethods
{
    public class CustomFieldFactoryMethods
    {
        public static void AddSubscriptionCustomFieldTo(Subscription subscription,
            PlanFrequencyCustomField planFrequencyCustomField)
        {
            var subscriptionCustomField = new SubscriptionCustomField
            {
                CustomFieldId = planFrequencyCustomField.CustomFieldId,
                SubscriptionId = subscription.Id,
                DateValue = planFrequencyCustomField.DefaultDateValue,
                NumericValue = planFrequencyCustomField.DefaultNumericValue,
                StringValue = planFrequencyCustomField.DefaultStringValue
            };

            subscription.SubscriptionCustomFields.Add(subscriptionCustomField);
        }

        public static void AddSubscriptionProductCustomFieldTo(SubscriptionProduct subscriptionProduct, PlanProductFrequencyCustomField planProductFrequencyCustomField)
        {
            var subscriptionProductCustomField = new SubscriptionProductCustomField
            {
                CustomFieldId = planProductFrequencyCustomField.CustomFieldId,
                DateValue = planProductFrequencyCustomField.DefaultDateValue,
                NumericValue = planProductFrequencyCustomField.DefaultNumericValue,
                StringValue = planProductFrequencyCustomField.DefaultStringValue
            };

            subscriptionProduct.SubscriptionProductCustomFields.Add(subscriptionProductCustomField);
        }
    }
}

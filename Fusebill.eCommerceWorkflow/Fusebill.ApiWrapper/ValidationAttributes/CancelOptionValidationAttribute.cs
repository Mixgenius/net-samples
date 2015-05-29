using System;
using System.ComponentModel.DataAnnotations;
using Model;

namespace Fusebill.ApiWrapper.ValidationAttributes
{
    public class CancelOptionValidationAttribute : ValidationAttribute 
    {
        public CancelOptionValidationAttribute()
        {
            ErrorMessage = "Allowable Cancel Options are: " + String.Join(", ", Enum.GetNames(typeof(SubscriptionCancellationReversalOptions)));
        }

        public override bool IsValid(object value)
        {
            SubscriptionCancellationReversalOptions enumValue;

            return Enum.TryParse(value as string, out enumValue);
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Put
{
    public class PricingModel : BaseDto, IValidatableObject
    {
        [JsonProperty(PropertyName = "pricingModelType")]
        public string PricingModelType { get; set; }

        [JsonProperty(PropertyName = "quantityRanges")]
        public List<QuantityRange> QuantityRanges { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (QuantityRanges != null && QuantityRanges.Any())
            {
                if (QuantityRanges.Count(qr => qr.Max == null) != 1)
                    yield return new ValidationResult("Please ensure all quantity ranges are set.");
            }
        }
    }
}

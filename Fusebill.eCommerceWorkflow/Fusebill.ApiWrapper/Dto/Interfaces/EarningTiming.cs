using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Fusebill.ApiWrapper.Dto.Interfaces
{
    public class EarningTiming : IValidatableObject
    {
        [JsonProperty(PropertyName = "earningInterval")]
        public string EarningInterval { get; set; }

        [JsonProperty(PropertyName = "earningNumberOfIntervals")]
        public int? EarningNumberOfIntervals { get; set; }

        [JsonProperty(PropertyName = "earningTimingInterval")]
        [Required]
        public string EarningTimingInterval { get; set; }

        [JsonProperty(PropertyName = "earningTimingType")]
        [Required]
        public string EarningTimingType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((string.IsNullOrEmpty(EarningInterval) && EarningNumberOfIntervals.HasValue)
                || (!string.IsNullOrEmpty(EarningInterval) && !EarningNumberOfIntervals.HasValue))
                yield return new ValidationResult("Please select both an Interval and Number of Intervals when earning over a period.", new[] { "EarningInterval", "EarningNumberOfIntervals" });

            if (string.IsNullOrEmpty(EarningInterval) && !EarningNumberOfIntervals.HasValue)
            {
                if (EarningTimingInterval != "EarnImmediately")
                    yield return
                        new ValidationResult(
                            "The timing interval must be EarnImmediately for products that do not earn over time",
                            new[] { "EarningTimingInterval" });

                if (EarningTimingType != "StartOfInterval")
                    yield return
                        new ValidationResult(
                            "The timing type must be StartOfInterval for products that earn immediately",
                            new[] { "EarningTimingType" });
            }
            else
            {
                if (EarningTimingInterval != "Daily")
                    yield return
                        new ValidationResult(
                            "The timing interval must be Daily for products that earn over time",
                            new[] { "EarningTimingInterval" });

                if (EarningTimingType != "StartOfInterval" && EarningTimingType != "EndOfInterval")
                    yield return
                        new ValidationResult(
                            "The timing type must be StartOfInterval or EndOfInterval",
                            new[] { "EarningTimingType" });
            }
        }
    }
}

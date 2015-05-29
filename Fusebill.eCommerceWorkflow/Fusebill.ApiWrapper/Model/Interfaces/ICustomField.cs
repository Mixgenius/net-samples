using System;

namespace Model.Internal
{
    public interface ICustomField
    {
        string StringValue { get; set; }
        DateTime? DateValue { get; set; }
        decimal? NumericValue { get; set; }
    }
}

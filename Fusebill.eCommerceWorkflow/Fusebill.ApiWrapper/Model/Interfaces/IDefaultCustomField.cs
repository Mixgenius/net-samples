using System;

namespace Model.Internal
{
    public interface IDefaultCustomField
    {
        string DefaultStringValue { get; set; }
        DateTime? DefaultDateValue { get; set; }
        decimal? DefaultNumericValue { get; set; }
    }
}

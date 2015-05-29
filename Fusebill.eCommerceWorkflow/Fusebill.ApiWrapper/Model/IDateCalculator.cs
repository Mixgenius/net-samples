using System;

namespace Model
{
    public interface IDateCalculator
    {
        DateTime CalculateDate(Interval interval, int numberOfIntervals, DateTime startDateTime);
        DateTime CalculateDate(Interval interval, int numberOfIntervals, DateTime startDateTime, int targetDay, Timezone accountTimezone);
        DateTime ConvertUtcDateTimeToAccountTimezoneDateTime(DateTime utcTransactionTime, Timezone accountTimezone);
        DateTime CalculateDateAndLineup(Interval interval, int numberOfIntervals, DateTime dateTime, int lineupDay, Timezone timezone, int? lineupMonth = null);
    }
}

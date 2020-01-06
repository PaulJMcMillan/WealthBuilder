//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;

namespace WealthBuilder
{
    public static class Date
    {
        public static DateTime Validate(string dateString)
        {
            DateTime dt;
            if (DateTime.TryParse(dateString, out dt)) return dt;
            return DateTime.MinValue;
        }

        public static DateTime IncrementDate(DateTime date, int frequencyId)
        {
            string frequencyCode = FrequencyCode.GetById(frequencyId);

            switch (frequencyCode)
            {
                case FrequencyCode.Daily:
                    return date.AddDays(1).Date;
                case FrequencyCode.Weekly:
                    return date.AddDays(7).Date;
                case FrequencyCode.BiWeekly:
                    return date.AddDays(14).Date;
                case FrequencyCode.Monthly:
                    return date.AddMonths(1).Date;
                case FrequencyCode.Quarterly:
                    return date.AddMonths(3).Date;
                case FrequencyCode.SemiAnnually:
                    return date.AddMonths(6).Date;
                case FrequencyCode.Annual:
                    return date.AddYears(1).Date;
                default:
                    throw new ArgumentException("Invalid frequency in Increment Date function.");
            }
        }
    }
}
